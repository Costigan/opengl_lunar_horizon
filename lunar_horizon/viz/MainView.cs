using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lunar_horizon.viz
{
    public partial class MainView : Form
    {
        public const double Meters = 0.000001d;
        public const double Kilometers = 0.001d;

        public OpenGLControlWrapper OpenGLWindow;

        public ShaderProgram EarthShader;
        public ShaderProgram MoonShader;
        public PhongRejection1 MoonShaderPhongRejection1;
        public ShaderProgram MoonShaderTexturedPhong;

        public MainView()
        {
            InitializeComponent();
        }

        private void MainView_Load(object sender, EventArgs e)
        {
            Console.WriteLine(@"in load");
            Toolkit.Init();

            OpenGLWindow = new OpenGLControlWrapper { Dock = DockStyle.Fill };
            tabMap.Controls.Add(OpenGLWindow);
            OpenGLWindow.Loaded = true;

            GL.ClearColor(Color.Black);

            GL.Disable(EnableCap.Lighting);   // Turn off lighting to get color

            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.ColorMaterial);  // lets me use colors rather than changing materials
            //GL.Enable(EnableCap.CullFace);
            GL.Enable(EnableCap.Normalize);      // Do I need this?  (this make a difference, although I don't know why)

            OpenGLWindow.CameraMode = new ArcBall(OpenGLWindow, OpenGLWindow.TheWorld.ViewTarget) { RelativePosition = new Vector3d(0d, 10000 * Kilometers, 0d) };
            //OGLDelegate.CameraMode = new JoystickCamera(OGLDelegate, OGLDelegate.TheWorld.ViewTarget) { Eye = new Vector3d(10f, 10f, 10f)};

            OpenGLWindow.TheWorld.Tick();
        }
    }
}
