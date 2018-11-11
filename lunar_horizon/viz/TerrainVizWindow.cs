using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using lunar_horizon.patches;
using System.Linq;
using lunar_horizon.terrain;

namespace lunar_horizon.viz
{
    public partial class TerrainVizWindow : Form
    {
        public const double Meters = 0.000001d;
        public const double Kilometers = 0.001d;

        public InMemoryTerrainManager Terrain;

        public OpenGLControlWrapper OpenGLWindow;

        private bool _dragging;

        public ShaderProgram EarthShader;
        public ShaderProgram MoonShader;
        public PhongRejection1 MoonShaderPhongRejection1;
        public ShaderProgram MoonShaderTexturedPhong;

        public List<TerrainPatch> Patches = new List<TerrainPatch>();

        protected DateTime _currentTime = DateTime.UtcNow;
        protected TimeSpan _timeSpan = new TimeSpan(3 * 28, 0, 0);

        public TerrainVizWindow()
        {
            InitializeComponent();
        }

        private void TerrainVizWindow_Load(object sender, EventArgs e)
        {
            Console.WriteLine(@"in load");
            Toolkit.Init();
            OpenGLWindow = new OpenGLControlWrapper { Dock = DockStyle.Fill };

            OpenGLWindow.MouseDown += OnMouseDownHandler;
            OpenGLWindow.MouseMove += OnMouseMoveHandler;
            OpenGLWindow.MouseUp += OnMouseUpHandler;
            OpenGLWindow.Paint += ogl_Paint;

            panel1.Controls.Add(OpenGLWindow);
            OpenGLWindow.Loaded = true;

            CreateShaders();

            OpenGLWindow.TheWorld = new World();

            LoadObjects(Patches);
            var world = OpenGLWindow.TheWorld;

            GL.ClearColor(Color.Black);
            //SetupViewport();

            GL.Enable(EnableCap.Lighting);   // Turn off lighting to get color
            GL.Enable(EnableCap.Light0);

            GL.Hint(HintTarget.PerspectiveCorrectionHint, HintMode.Nicest);  //??
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

            GL.ShadeModel(ShadingModel.Smooth);

            // Enable Light 0 and set its parameters.
            GL.Light(LightName.Light0, LightParameter.Position, new Vector4(world.Sun.Position.ToFloat(), 1f));

            const float ambient = 0.4f;
            const float diffuse = 1f;

            GL.Light(LightName.Light0, LightParameter.Ambient, new[] { ambient, ambient, ambient, 1.0f });
            //GL.Light(LightName.Light0, LightParameter.Ambient, new[] { 0.6f, 0.6f, 0.6f, 1.0f });
            GL.Light(LightName.Light0, LightParameter.Diffuse, new[] { diffuse, diffuse, diffuse, 1.0f });
            GL.Light(LightName.Light0, LightParameter.Specular, new[] { 1f, 1f, 1f, 1.0f });
            GL.Light(LightName.Light0, LightParameter.SpotExponent, new[] { 1.0f, 1.0f, 1.0f, 1.0f });
            GL.LightModel(LightModelParameter.LightModelAmbient, new[] { 0f, 0f, 0f, 1.0f });
            GL.LightModel(LightModelParameter.LightModelLocalViewer, 0);
            GL.LightModel(LightModelParameter.LightModelTwoSide, 0);

            //GL.Material(MaterialFace.Front, MaterialParameter.Ambient, new float[] { 0.3f, 0.3f, 0.3f, 1.0f });
            //GL.Material(MaterialFace.Front, MaterialParameter.Diffuse, new float[] { 1.0f, 1.0f, 1.0f, 1.0f });
            //GL.Material(MaterialFace.Front, MaterialParameter.Specular, new float[] { 0.5f, 0.5f, 0.5f, 1.0f });
            //GL.Material(MaterialFace.Front, MaterialParameter.Emission, new float[] { 0.0f, 0.0f, 0.0f, 1.0f });

            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.ColorMaterial);  // lets me use colors rather than changing materials
            //GL.Enable(EnableCap.CullFace);
            GL.Enable(EnableCap.Normalize);      // Do I need this?  (this make a difference, although I don't know why)

            GL.PointSize(5f);
            GL.Enable(EnableCap.PointSmooth);
            GL.Hint(HintTarget.PointSmoothHint, HintMode.Nicest);

            //world.Fetcher.Frame = LadeeStateFetcher.StateFrame.MoonFixed;

            //var t = TimeUtilities.DateTimeToTime42(new DateTime(2014, 1, 1));
            //UpdateToTime(t);

            OpenGLWindow.CameraMode = new ArcBall(OpenGLWindow, OpenGLWindow.TheWorld.ViewTarget) { RelativePosition = new Vector3d(0d, 10000 * Kilometers, 0d) };
            //OGLDelegate.CameraMode = new JoystickCamera(OGLDelegate, OGLDelegate.TheWorld.ViewTarget) { Eye = new Vector3d(10f, 10f, 10f)};

            OpenGLWindow.TheWorld.Tick();
        }

        void CreateShaders()
        {
            //EarthShader = new EarthShaderProgram("earth_vs_120.glsl", "earth_fs_120.glsl");
            //MoonShader = new ShaderProgram("moon1_vs_120.glsl", "moon1_fs_120.glsl");

            //NOTE: turning this off for now (12/5/2017)
            //MoonShaderPhongRejection1 = new PhongRejection1(@"shaders\textured_phong_vs_120.glsl", @"shaders\phong_rejection1_fs_120.glsl");
            //MoonShaderTexturedPhong = new ShaderProgram(@"shaders\textured_phong_vs_120.glsl", @"shaders\textured_phong_fs_120.glsl");
            //MoonShader = MoonShaderTexturedPhong;
        }

        private void LoadObjects(List<TerrainPatch> patches = null)
        {
            var world = OpenGLWindow.TheWorld;
            if (!world.Wrappers.Contains(OpenGLWindow))
                world.Wrappers.Add(OpenGLWindow);

            world.Sun = new Ball
            {
                Name = "Sun",
                //                Position = new Vector3d(100000d, 100000d, 1000d),
                Position = new Vector3d(0d, 0d, 0d),
                Color = Color.Yellow,
                Radius = (float)(0.5f * Kilometers),
                XSize = 32,
                YSize = 16,
                ShowAxes = false,
                AxisScale = 10f
            };
            world.Sun.Load();

            /*
            world.Earth = new Earth
            {
                Name = "Earth",
                Position = new Vector3d(0d, 0d, 0d),
                TextureFilename = @"Resources\earth_800x400.jpg",
                NightFilename = @"Resources\earth_night_800x400.jpg",
                Radius = (float)(6371 * Kilometers),
                XSize = 48,
                YSize = 24,
                ShowAxes = false,
                AxisScale = 10f,
                Specularity = new[] { 1f, 1f, 1f },
                Shininess = 10f,
                Shader = EarthShader
            };
            world.Earth.Load();
            world.Earth.LoadTexture();
            world.FarShapes.Add(world.Earth);

            world.Stars = new StarBackground();
            world.Stars.Load();
            */

            world.Terrain = new MoonDEM
            {
                Position = new Vector3d(0d, 0d, 0d),
                ShowAxes = true,
                Shader = MoonShader,
            };
            if (patches != null && patches.Count > 0)
                world.Terrain.Load(patches);
            world.FarShapes.Add(world.Terrain);

            // Calculate the center of the terrain patches loaded
            Vector3d center = CenterOfPatches(Patches);

            world.Sun.Position = center;

            world.ViewTarget = new Ball
            {
                XSize = 3, YSize = 3,
                Radius = (float)(1737.4f * Kilometers),
                Position = center,
                ShowAxes = false,
                Color = Color.Red,
                Visible = false
            };
            world.ViewTarget.Load();
            world.FarShapes.Add(world.ViewTarget);

            var moonCenter = new Ball { Radius = (float)(1d * Kilometers), Color = Color.LightGreen };
            world.FarShapes.Add(moonCenter);

            //world.Terrain.Shader = MoonShaderTexturedPhong;
        }

        private Vector3d CenterOfPatches(List<TerrainPatch> patches)
        {
            var c = new Vector3d();
            var count = 0;
            foreach (var p in patches)
            {
                var points = p.Points;
                for (var line=0;line<points.Length;line++)
                {
                    var row = points[line];
                    for (var sample=0;sample<row.Length;sample++)
                    {
                        var pt = row[sample];
                        c.X += pt.X;
                        c.Y += pt.Y;
                        c.Z += pt.Z;
                        count++;
                    }
                }
            }
            return count == 0 ? new Vector3d(0, 0, 0) : new Vector3d(c.X / count, c.Y / count, c.Z / count);
        }

        #region Painting

        private void ogl_Paint(object sender, PaintEventArgs e)
        {
            if (!OpenGLWindow.Loaded) return;
            OpenGLWindow.PaintScene();

            //GL.Begin(BeginMode.Lines);

            //GL.End();
        }

        #endregion


        #region Mouse Handling

        protected void OnMouseDownHandler(object sender, MouseEventArgs e)
        {
            base.OnMouseDown(e);
            _dragging = true;
            OpenGLWindow.CameraMode.DragStart(OpenGLWindow, e);
        }

        protected void OnMouseMoveHandler(object sender, MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (!_dragging) return;
            OpenGLWindow.CameraMode.Drag(OpenGLWindow, e);
        }

        protected void OnMouseUpHandler(object sender, MouseEventArgs e)
        {
            base.OnMouseUp(e);
            _dragging = false;
            OpenGLWindow.CameraMode.DragStop(OpenGLWindow, e);
        }

        #endregion

        private void tbSelectTime_ValueChanged(object sender, EventArgs e)
        {
            UpdateCurrentTime();
        }

        private void cbTimeSpan_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbTimeSpan.SelectedIndex)
            {
                case 0:
                    _timeSpan = new TimeSpan(3 * 28, 0, 0, 0, 0);
                    break;
                case 1:
                    _timeSpan = new TimeSpan(2 * 28, 0, 0, 0, 0);
                    break;
                case 2:
                    _timeSpan = new TimeSpan(1 * 28, 0, 0, 0, 0);
                    break;
                case 3:
                    _timeSpan = new TimeSpan(2 * 7, 0, 0, 0, 0);
                    break;

                case 4:
                    _timeSpan = new TimeSpan(7, 0, 0, 0, 0);
                    break;
                case 5:
                    _timeSpan = new TimeSpan(1, 0, 0, 0, 0);
                    break;
                case 6:
                default:
                    _timeSpan = new TimeSpan(0, 1, 0, 0, 0);
                    break;
            }
            UpdateCurrentTime();
        }

        private void dtStartTime_ValueChanged(object sender, EventArgs e)
        {
            UpdateCurrentTime();
        }

        protected void UpdateCurrentTime()
        {
            _currentTime = new DateTime(dtStartTime.Value.Ticks + (long)(_timeSpan.Ticks * (tbSelectTime.Value / (float)tbSelectTime.Maximum)));
            lbCurrentTime.Text = _currentTime.ToString();
            UpdateShadowCasters();
        }

        private void UpdateShadowCasters()
        {
            var shadowCaster = LunarHorizon.SunPosition(_currentTime);
            OpenGLWindow.TheWorld.Terrain.UpdateShadowcaster(shadowCaster);
            OpenGLWindow.Invalidate();
        }

        private void loadTraversePathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var d = new OpenFileDialog { FilterIndex = 0, Filter = "Traverse summary files (*.latlon)|*.latlon|All files (*.*)|*.*" };
            if (d.ShowDialog() != DialogResult.OK)
                return;
            var summary = TraversePlanner.planning.LatLonTraverseSummary.Load(d.FileName);
            var points = summary.LatLonList.Select(ll =>
            {
                InMemoryTerrainManager.GetLineSampleD(ll.Latitude * Math.PI / 180d, ll.Longitude * Math.PI / 180d, out double line, out double sample);
                var p = Terrain.GetPointInME((float)line, (float)sample);
                return new Vector3((float)(p.X * Kilometers), (float)(p.Y * Kilometers), (float)(p.Z * Kilometers));
            }).ToList();
            var path = new TraversePath();
            path.Paths.Add(Color.Green, points);
            OpenGLWindow.TheWorld.Terrain.TraversePaths.Add(path);
            OpenGLWindow.Invalidate();
        }
    }
}
