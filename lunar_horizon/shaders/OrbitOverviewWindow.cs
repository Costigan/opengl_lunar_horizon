using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using OrbitOverview.Spice;
using OrbitOverview2.Viz;
using OpenTK.Graphics.OpenGL;
using OpenTK;

namespace OrbitOverview2
{
    public partial class OrbitOverviewWindow : Form
    {
        public const float FarUnit = 1000f;   // 1000 km
        public const float NearUnit = 1f;     // 1 m
        public const double Meters = 0.000001d;
        public const double Kilometers = 0.001d;
        public const long Minutes = 65536L * 60;
        public const long Seconds = 65536L;
        public const long Hours = 65536L * 60 * 60;
        public const long Days = 65536L * 60 * 60 * 24;

        public World TheWorld;

        public enum PresentationMode { PhasingLoopsOverview, Science };
        public PresentationMode Presentation = PresentationMode.PhasingLoopsOverview;

        //public const string SpiceDir = @"C:\UVS\svn_soc\spice-kernels\";
        public const string SpiceDir = @"spice-kernels\";

        private DateTime _scenarioStart;
        private DateTime _scenarioEnd;
        private bool _scenarioUpdated;
        private bool _scenarioInhibit;

        private readonly UVSTimestampManager _timestampManager;
        private bool _inhibitTimestampUpdate;



        public OrbitOverviewWindow()
        {
            InitializeComponent();
            TheWorld = new World(SpiceDir);
            TheWorld.Wrappers.Add(glControl1);

            InitializeCameraCombo();
            InitlializePhaseCombo();
            _timestampManager = new UVSTimestampManager("uvs_spectra_timestamps.tim");

        }

        #region Loading

        private void glControl1_Load(object sender, EventArgs e)
        {
            glControl1.Loaded = true;

            CreateShaders();

            glControl1.MakeCurrent();

            //WavefrontShape.MakeCone(20);
            STKModel.MakeCone(20);

            glControl1.TheWorld = TheWorld;

            LoadObjects();

            GL.ClearColor(Color.Black);
            //SetupViewport();

            GL.Enable(EnableCap.Lighting);   // Turn off lighting to get color
            GL.Enable(EnableCap.Light0);

            GL.Hint(HintTarget.PerspectiveCorrectionHint, HintMode.Nicest);  //??
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

            GL.ShadeModel(ShadingModel.Smooth);

            // Enable Light 0 and set its parameters.
            //GL.Light(LightName.Light0, LightParameter.Position, SunPosition);

            const float ambient = 0.1f;

            GL.Light(LightName.Light0, LightParameter.Ambient, new[] { ambient, ambient, ambient, 1.0f });
            //GL.Light(LightName.Light0, LightParameter.Ambient, new[] { 0.6f, 0.6f, 0.6f, 1.0f });
            GL.Light(LightName.Light0, LightParameter.Diffuse, new[] { 1.0f, 1.0f, 1.0f, 1.0f });
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
            GL.Enable(EnableCap.CullFace);
            GL.Enable(EnableCap.Normalize);      // Do I need this?  (this make a difference, although I don't know why)

            GL.PointSize(5f);
            GL.Enable(EnableCap.PointSmooth);
            GL.Hint(HintTarget.PointSmoothHint, HintMode.Nicest);

            var start = TheWorld.Fetcher.SpiceStartDate.AddDays(0);
            SetScenarioTimes(LadeeStateFetcher.StateFrame.MoonFixed, start, TheWorld.Fetcher.SpiceStartDate.AddDays(70));

            var t = TimeUtilities.DateTimeToTime42(start);
            UpdateToTime(t);

            glControl1.CameraMode = new ArcBall(glControl1, TheWorld.LADEE) { RelativePosition = new Vector3d(0d, 100 * Meters, 0d) };
            TheWorld.Tick();
        }

        void CreateShaders()
        {
            glControl1.EarthShaderProgram = new ShaderProgramWrapper("simple_vs.glsl", "simple_fs.glsl");
            //glControl1.EarthShaderProgram = new ShaderProgramWrapper("earth_vs.glsl", "earth_fs.glsl");
        }

        public void SetScenarioTimes(LadeeStateFetcher.StateFrame frame, DateTime start, DateTime stop)
        {
            _scenarioInhibit = true;
            TheWorld.Frame = frame;
            _scenarioStart = start;
            _scenarioEnd = stop;
            var start42 = TimeUtilities.DateTimeToTime42(_scenarioStart);
            var stop42 = TimeUtilities.DateTimeToTime42(_scenarioEnd);
            tbScenarioBegin.Text = TimeUtilities.Time42ToSTK(start42);
            tbScenarioEnd.Text = TimeUtilities.Time42ToSTK(stop42);

            TheWorld.Trajectory.Frame = frame;
            TheWorld.Trajectory.DrawStart = start42;
            TheWorld.Trajectory.DrawStop = stop42;

            _scenarioUpdated = true;
            _scenarioInhibit = false;
        }

        private void LoadObjects()
        {
            TheWorld.Trajectory = new TrajectoryShape(TheWorld);

            TheWorld.LADEE = STKModel.Load("ladee_new.mdl");
            TheWorld.LADEE.ShowAxes = true;
            TheWorld.NearShapes.Add(TheWorld.LADEE);

            if (false)
            {
                for (var azimuth = 0d; azimuth < MathHelper.TwoPi; azimuth += MathHelper.DegreesToRadians(5))
                    for (var elevation = MathHelper.DegreesToRadians(-60d); elevation < MathHelper.DegreesToRadians(60d); elevation += MathHelper.DegreesToRadians(5))
                    {
                        const double d = 100000d;
                        var w = Math.Cos(elevation) * d;
                        var z = Math.Sin(elevation) * d;
                        var x = Math.Cos(azimuth) * w;
                        var y = Math.Sin(azimuth) * w;
                        var cube = new CubeShape() { Scale = 0.1f, Position = new Vector3d(x, y, z) };
                        TheWorld.FarShapes.Add(cube);
                    }
            }

            //var cube = new CubeShape() { Scale = 1.5f, Position = new Vector3d(1000d,0d,0d) };
            //TheWorld.FarShapes.Add(cube);

            /*
            _ladee = new WavefrontShape
                {
                    Name = "LADEE",
                    WavefrontFilename = "0a-003-ladee_simplified2.obj",
                    Position = new Vector3d(386400 * Kilometers, 0000 * Kilometers, 0d),
                    ShowAxes = true,
                    AxisScale = 40f,
                    ShowModel = true
                };
            _ladee.Load();
            //glControl1.NearShapes.Add(_ladee);
             */

            //foreach (var c in _stkModel.Components)
            //    foreach (var m in c.Meshes)
            //        Console.WriteLine("VBO {0}", m.Handle.VboID);
            //Console.WriteLine("VBO for Wavefront = {0}", _ladee._vboid);

            /*
            _moon = new MoonHeightField { Name = "Moon", Position = new Vector4d(384400 * Kilometers, 0d, 0d, 1d), TextureFilename = "moon_8k_color_brim16.jpg", Radius = (float)(1734.4 * Kilometers)};
            _moon.Load();
            _moon.LoadTexture();
            glControl1.FarShapes.Add(_moon);
            */

            if (true)
            {
                TheWorld.Moon = new MoonDEM()
                {
                    Name = "Moon",
                    Position = new Vector3d(384400 * Kilometers, 0d, 0d),
                    TextureFilename = "moon_8k_color_brim16.jpg",
                    ShowAxes = true,
                    AxisScale = 4f
                };
                ((MoonDEM)TheWorld.Moon).Load();
                TheWorld.FarShapes.Add(TheWorld.Moon);
            }
            else
            {
                TheWorld.Moon = new MoonHeightField
                {
                    Name = "Moon",
                    Position = new Vector3d(384400 * Kilometers, 0d, 0d),
                    TextureFilename = "moon_8k_color_brim16.jpg",
                    Radius = (float)(1734.4 * Kilometers),
                    XSize = 96,
                    YSize = 48,
                    ShowAxes = true,
                    AxisScale = 4f
                };
                ((MoonHeightField)TheWorld.Moon).Load();
                ((MoonHeightField)TheWorld.Moon).LoadTexture();
                TheWorld.FarShapes.Add(TheWorld.Moon);
            }

            // earth_800x400.jpg
            // land_shallow_topo_2011_8192.jpg
            TheWorld.Earth = new TexturedBall
            {
                Name = "Earth",
                Position = new Vector3d(0d, 0d, 0d),
                TextureFilename = "earth_800x400.jpg",
                Radius = (float)(6371 * Kilometers),
                XSize = 48,
                YSize = 24,
                ShowAxes = true,
                AxisScale = 10f,
                Specularity = new float[]{1f, 1f, 1f},
                Shininess = 1f
            };
            TheWorld.Earth.Shader = glControl1.EarthShaderProgram;
            TheWorld.Earth.Load();
            TheWorld.Earth.LoadTexture();
            TheWorld.FarShapes.Add(TheWorld.Earth);

            // earth_800x400.jpg
            // land_shallow_topo_2011_8192.jpg
            TheWorld.Sun = new TexturedBall
            {
                Name = "Sun",
                Position = new Vector3d(0d, 0d, 0d),
                TextureFilename = "sun.png",
                Color = Color.Yellow,
                Radius = (float)(695500 * Kilometers),
                XSize = 32,
                YSize = 16,
                ShowAxes = false,
                AxisScale = 10f
            };
            TheWorld.Sun.Load();
            TheWorld.Sun.LoadTexture();

            TheWorld.Stars = new StarBackground();
            TheWorld.Stars.Load();
        }

        #endregion Loading

        #region Viewport

        private void glControl1_Resize(object sender, EventArgs e)
        {
            if (!glControl1.Loaded) return;
            glControl1.SetupViewport();
        }

        #endregion Viewport

        #region Painting

        private void glControl1_Paint(object sender, PaintEventArgs e)
        {
            if (!glControl1.Loaded) return;
            glControl1.PaintScene();
        }

        #endregion Painting

        private void InitializeCameraCombo()
        {
            cmbCamera.Items.AddRange(
                new object[]{
                    new ComboAction {
                        Name="LADEE",
                        Func = () => glControl1.CameraMode = new ZoomTo(glControl1, TheWorld.LADEE, 10 * Meters, 2 * Meters, new ArcBall(glControl1, TheWorld.LADEE))
                    },
                    new ComboAction {
                        Name="Earth",
                        Func = () => glControl1.CameraMode = new ZoomTo(glControl1, TheWorld.Earth, 60000d, 1d, new ArcBall(glControl1, TheWorld.Earth))
                    },
                    new ComboAction {
                        Name="Moon",
                        Func = () => glControl1.CameraMode = new ZoomTo(glControl1, TheWorld.Moon, 20000d, 1d, new ArcBall(glControl1, TheWorld.Moon))
                    },
                    new ComboAction {
                        Name="Sun",
                        Func = () => glControl1.CameraMode = new ZoomTo(glControl1, TheWorld.Sun, 6000000d, 1d, new ArcBall(glControl1, TheWorld.Sun))
                    },
                    new ComboAction {
                        Name="UVS Telescope 4 deg",
                        Func = () => glControl1.CameraMode = new TelescopeView(glControl1, TheWorld.LADEE)
                    },
                    new ComboAction {
                        Name="UVS Telescope 40 deg",
                        Func = () =>  glControl1.CameraMode = new TelescopeView(glControl1, TheWorld.LADEE, 40f)
                    },
                    new ComboAction {
                        Name="UVS Solar Viewer 4 deg",
                        Func = () => glControl1.CameraMode = new SolarViewerView(glControl1, TheWorld.LADEE)
                    },
                    new ComboAction {
                        Name="UVS Solar Viewer 40 deg",
                        Func = () => glControl1.CameraMode = new SolarViewerView(glControl1, TheWorld.LADEE, 40f)
                    },
                    new ComboAction {
                        Name="Joystick",
                        Func = () => {
                                        var oldEye = new Vector3d(glControl1.CameraMode.Eye);
            var m = new JoystickCamera(glControl1, glControl1.CameraMode.Target) { Eye = oldEye };
            m.ResetVectors();
            glControl1.CameraMode = m;
                        }
                    }
                });
            cmbCamera.SelectedIndex = 0;
        }

        private void cmbCamera_SelectedValueChanged(object sender, EventArgs e)
        {
            if (!glControl1.Loaded) return;
            var i = cmbCamera.SelectedIndex;
            var f = cmbCamera.Items[i] as ComboAction;
            if (f == null) return;
            f.Func();
            TheWorld.Tick();
        }

        private void InitlializePhaseCombo()
        {
            cmbMissionPhase.Items.AddRange(
                new object[]{
                    new ComboAction {
                        Name="All Phases",
                        Func = () => SetScenarioTimes(TheWorld.Fetcher.Frame, new DateTime(2013, 9, 8), new DateTime(2014, 1, 30))
                    },
                    new ComboAction {
                        Name="Phasing Loops",
                        Func = () => SetScenarioTimes(TheWorld.Fetcher.Frame, new DateTime(2013, 9, 8), new DateTime(2013, 10, 6, 0, 0, 0))
                    },
                    new ComboAction {
                        Name="Lunar Orbit Acquisition",
                        Func = () => SetScenarioTimes(TheWorld.Fetcher.Frame, new DateTime(2013, 10, 6, 0, 0, 0), new DateTime(2013, 10, 10, 3, 0, 0))
                    },
                    new ComboAction {
                        Name="Commissioning",
                        Func = () => SetScenarioTimes(TheWorld.Fetcher.Frame, new DateTime(2013, 10, 10, 3, 0, 0), new DateTime(2013, 11, 9, 3, 0, 0))
                    },
                    new ComboAction {
                        Name="Science",
                        Func = () =>  SetScenarioTimes(TheWorld.Fetcher.Frame, new DateTime(2013, 11, 9, 3, 0, 0), new DateTime(2014, 1, 30))
                    },
                    new ComboAction {
                        Name="Sample Occultation",
                        Func = () => SetScenarioTimes(TheWorld.Fetcher.Frame, new DateTime(2013, 11, 22, 19, 20, 0), new DateTime(2013, 11, 22, 19, 36, 00))
                    },
                    new ComboAction {
                        Name="Sample Limb Orbit",
                        Func = () => SetScenarioTimes(TheWorld.Fetcher.Frame, new DateTime(2013, 10, 23, 17, 45, 00), new DateTime(2013, 10, 24, 1, 7, 00))
                    },
                    new ComboAction {
                        Name="Sample OMM",
                        Func = () => SetScenarioTimes(TheWorld.Fetcher.Frame, new DateTime(2013, 11, 11, 2, 30, 00), new DateTime(2013, 11, 11, 4, 0, 00))
                    },
                    new ComboAction {
                        Name="Sample OMM (zoomed)",
                        Func = () => SetScenarioTimes(TheWorld.Fetcher.Frame, DateTime.Parse("11 Nov 2013 03:30:36.711"), DateTime.Parse("11 Nov 2013 03:32:14.458"))
                    },
                });
            cmbMissionPhase.SelectedIndex = 0;
        }


        private void cmbMissionPhase_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!glControl1.Loaded) return;
            var i = cmbMissionPhase.SelectedIndex;
            var f = cmbMissionPhase.Items[i] as ComboAction;
            if (f == null) return;
            f.Func();
            TheWorld.Tick();
        }

        private void tbScenarioBegin_TextChanged(object sender, EventArgs e)
        {
            if (_scenarioInhibit) return;
            DateTime d;
            if (DateTime.TryParse(tbScenarioBegin.Text, out d))
            {
                _scenarioStart = d;
                tbScenarioBegin.ForeColor = Color.Gray;
            }
            else
            {
                tbScenarioBegin.ForeColor = Color.Red;
            }
            _scenarioUpdated = false;
        }

        private void tbScenarioEnd_TextChanged(object sender, EventArgs e)
        {
            if (_scenarioInhibit) return;
            DateTime d;
            if (DateTime.TryParse(tbScenarioEnd.Text, out d))
            {
                _scenarioEnd = d;
                tbScenarioEnd.ForeColor = Color.Gray;
            }
            else
            {
                tbScenarioEnd.ForeColor = Color.Red;
            }
            _scenarioUpdated = false;
        }

        private void tbDateSlider_Scroll(object sender, EventArgs evt)
        {
            if (!_scenarioUpdated)
                SetScenarioTimes(TheWorld.Fetcher.Frame, _scenarioStart, _scenarioEnd);
            var b = TimeUtilities.DateTimeToTime42(_scenarioStart);
            var e = TimeUtilities.DateTimeToTime42(_scenarioEnd);
            var t = b + (long)((e - b) * (tbDateSlider.Value / (double)tbDateSlider.Maximum));

            _scenarioInhibit = true;
            tbCurrentTime.Text = TimeUtilities.Time42ToSTK(t);
            _scenarioInhibit = false;

            UpdateToTime(t);
        }

        public void UpdateToTime(long t)
        {
            TheWorld.Update(t);
        }

        private void tbCurrentTime_TextChanged(object sender, EventArgs e)
        {
            if (_scenarioInhibit) return;
            DateTime d;
            if (DateTime.TryParse(tbCurrentTime.Text, out d))
            {
                UpdateToTime(TimeUtilities.DateTimeToTime42(d));
                tbCurrentTime.ForeColor = Color.Gray;
            }
            else
            {
                tbCurrentTime.ForeColor = Color.Red;
            }
        }

        private void cbShowLadeeModel_CheckedChanged(object sender, EventArgs e)
        {
            TheWorld.LADEE.ShowModel = cbShowLadeeModel.Checked;
            TheWorld.Tick();
        }

        private void cbShowLadeeAxes_CheckedChanged(object sender, EventArgs e)
        {
            TheWorld.LADEE.ShowAxes = cbShowLadeeAxes.Checked;
            TheWorld.Tick();
        }

        private void cbShowTelescope_CheckedChanged(object sender, EventArgs e)
        {
            TheWorld.LADEE.ShowTelescope = cbShowTelescope.Checked;
            TheWorld.Tick();
        }

        private void cbShowSolarViewer_CheckedChanged(object sender, EventArgs e)
        {
            TheWorld.LADEE.ShowSolarViewer = cbShowSolarViewer.Checked;
            TheWorld.Tick();
        }

        private void cbShowTrajectory_CheckedChanged(object sender, EventArgs e)
        {
            glControl1.ShowTrajectory = cbShowTrajectory.Checked;
            TheWorld.Trajectory.Visible = cbShowTrajectory.Checked;
            TheWorld.Tick();
        }

        private void rbFrameEarth_CheckedChanged(object sender, EventArgs e)
        {
            if (!rbFrameEarth.Checked) return;
            TheWorld.Trajectory.Frame = TheWorld.Fetcher.Frame = LadeeStateFetcher.StateFrame.EarthFixed;
            TheWorld.Tick();
        }

        private void rbFrameMoon_CheckedChanged(object sender, EventArgs e)
        {
            if (!rbFrameMoon.Checked) return;
            TheWorld.Trajectory.Frame = TheWorld.Fetcher.Frame = LadeeStateFetcher.StateFrame.MoonFixed;
            TheWorld.Tick();
        }

        private void rbFrameSSE_CheckedChanged(object sender, EventArgs e)
        {
            if (!rbFrameSSE.Checked) return;
            TheWorld.Trajectory.Frame = TheWorld.Fetcher.Frame = LadeeStateFetcher.StateFrame.SelenocentricSolarEcliptic;
            TheWorld.Tick();
        }

        private void createNewViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var f = new SecondaryView(glControl1.TheWorld) {Wrapper = {ShowTrajectory = glControl1.ShowTrajectory}};
            f.Wrapper.CameraMode = new InstrumentView(f.Wrapper, f.Wrapper.TheWorld.LADEE);
            f.Show();
        }

        private void cbTextures_CheckedChanged(object sender, EventArgs e)
        {
            TexturedShape.ShowTexture = cbTextures.Checked;
            MoonDEM.ShowTexture = cbTextures.Checked;
            foreach (var w in TheWorld.Wrappers)
                w.Invalidate();
        }

        private void udActivity_ValueChanged(object sender, EventArgs e)
        {
            if (_inhibitTimestampUpdate) return;
            _inhibitTimestampUpdate = true;
            var idx = _timestampManager.GetIndexOfSequenceNumber((long)udActivity.Value, (long)udSequenceNumber.Value);
            if (idx < 0)
            {
                udActivity.ForeColor = Color.Red;
                udSequenceNumber.ForeColor = Color.Red;
                _inhibitTimestampUpdate = false;
                return;
            }
            var timestamp = _timestampManager.GetTimestampOfIndex(idx);
            tbSpectrumTimestamp.Text = TimeUtilities.Time42ToSTK(timestamp);
            UpdateToTime(timestamp);
            udActivity.ForeColor = Color.Gray;
            udSequenceNumber.ForeColor = Color.Gray;
            tbSpectrumTimestamp.ForeColor = Color.Gray;
            _inhibitTimestampUpdate = false;
        }

        private void udSequenceNumber_ValueChanged(object sender, EventArgs e)
        {
            if (_inhibitTimestampUpdate) return;
            _inhibitTimestampUpdate = true;
            var idx = _timestampManager.GetIndexOfSequenceNumber((long)udActivity.Value, (long)udSequenceNumber.Value);
            if (idx < 0)
            {
                udActivity.ForeColor = Color.Red;
                udSequenceNumber.ForeColor = Color.Red;
                _inhibitTimestampUpdate = false;
                return;
            }
            var timestamp = _timestampManager.GetTimestampOfIndex(idx);
            tbSpectrumTimestamp.Text = TimeUtilities.Time42ToSTK(timestamp);
            UpdateToTime(timestamp);
            udActivity.ForeColor = Color.Gray;
            udSequenceNumber.ForeColor = Color.Gray;
            tbSpectrumTimestamp.ForeColor = Color.Gray;
            _inhibitTimestampUpdate = false;
        }

        private void tbSpectrumTimestamp_TextChanged(object sender, EventArgs e)
        {
            if (_inhibitTimestampUpdate) return;
            _inhibitTimestampUpdate = true;
            DateTime d;
            if (!DateTime.TryParse(tbSpectrumTimestamp.Text, out d))
            {
                tbSpectrumTimestamp.ForeColor = Color.Red;
                _inhibitTimestampUpdate = false;
                return;
            }
            var timestamp = TimeUtilities.DateTimeToTime42(d);
            var idx = _timestampManager.GetIndexOfTimestamp(timestamp);
            if (idx < 0)
            {
                tbSpectrumTimestamp.ForeColor = Color.Red;
                _inhibitTimestampUpdate = false;
                return;
            }
            UpdateToTime(timestamp);
            udActivity.Value = _timestampManager.GetActivityOfIndex(idx);
            udSequenceNumber.Value = _timestampManager.GetSequenceNumberOfIndex(idx);
            udActivity.ForeColor = Color.Gray;
            udSequenceNumber.ForeColor = Color.Gray;
            tbSpectrumTimestamp.ForeColor = Color.Gray;
            _inhibitTimestampUpdate = false;
        }

        private void udAmbient_ValueChanged(object sender, EventArgs e)
        {
            var ambient = ((float)udAmbient.Value) / 100f;
            GL.Light(LightName.Light0, LightParameter.Ambient, new[] { ambient, ambient, ambient, 1.0f });
            glControl1.Invalidate();
        }

    }

    internal class ComboAction
    {
        internal string Name;
        internal Action Func;
        public override string ToString()
        {
            return Name;
        }
    }

    internal class UVSTimestampManager
    {
        internal long[] Timestamp;
        internal long[] ActivitySequence;

        internal UVSTimestampManager(string filename)
        {
            var bytes = File.ReadAllBytes(filename);
            Load(bytes);
        }

        internal void Load(byte[] ary)
        {
            var length = ary.Length/16;
            var ptr1 = 0;
            var ptr2 = length*8;
            Timestamp = new long[length];
            ActivitySequence = new long[length];
            for (var i = 0; i < length; i++)
            {
                Timestamp[i] = BitConverter.ToInt64(ary, ptr1);
                ActivitySequence[i] = BitConverter.ToInt64(ary, ptr2);
                ptr1 += 8;
                ptr2 += 8;
            }
        }

        internal int GetIndexOfActivity(long activityNumber)
        {
            var key = activityNumber << 32;
            var idx = Array.BinarySearch(ActivitySequence, key);
            return idx >= 0 ? idx : -1;
        }

        internal int GetIndexOfSequenceNumber(long activityNumber, long sequenceNumber)
        {
            var key = (activityNumber << 32) | sequenceNumber;
            var idx = Array.BinarySearch(ActivitySequence, key);
            return idx >= 0 ? idx : -1;
        }

        internal int GetIndexOfTimestamp(long timestamp)
        {
            var key = timestamp;
            var idx = Array.BinarySearch(Timestamp, key);
            if (idx >= 0) return idx;
            idx = ~idx;
            if (idx >= Timestamp.Length) return -1;
            return idx;
        }

        internal int GetIndexOfTimestamp(string timestamp)
        {
            DateTime d;
            if (DateTime.TryParse(timestamp, out d))
                return GetIndexOfTimestamp(TimeUtilities.DateTimeToTime42(d));
            return -1;
        }

        internal long GetTimestampOfIndex(int index)
        {
            if (index < 0) return -1;
            if (index >= Timestamp.Length) return -1;
            return Timestamp[index];
        }

        internal long GetActivityOfIndex(int index)
        {
            if (index < 0) return -1;
            if (index >= Timestamp.Length) return -1;
            var v = ActivitySequence[index];
            return v >> 32;
        }

        internal long GetSequenceNumberOfIndex(int index)
        {
            if (index < 0) return -1;
            if (index >= Timestamp.Length) return -1;
            var v = ActivitySequence[index];
            return v & 0xFFFFFFFF;
        }

    }

}
