using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using lunar_horizon.patches;

namespace lunar_horizon.viz
{
    public partial class NearFieldVisualization : Form
    {
        public const double Meters = 0.000001d;
        public const double Kilometers = 0.001d;

        public OpenGLControlWrapper OpenGLWindow;

        private bool _dragging;

        float[] _ary;
        int _lines;
        int _samples;

        public NearFieldVisualization(float[] ary, int lines, int samples)
        {
            _ary = ary;
            _lines = lines;
            _samples = samples;
            InitializeComponent();
        }

        private void NearFieldVisualization_Load(object sender, EventArgs e)
        {
            Toolkit.Init();
            OpenGLWindow = new OpenGLControlWrapper { Dock = DockStyle.Fill };

            OpenGLWindow.MouseDown += OnMouseDownHandler;
            OpenGLWindow.MouseMove += OnMouseMoveHandler;
            OpenGLWindow.MouseUp += OnMouseUpHandler;
            OpenGLWindow.Paint += ogl_Paint;

            Controls.Add(OpenGLWindow);
            OpenGLWindow.Loaded = true;

            LoadObjects();

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

        private void LoadObjects()
        {

        }
    }
}
