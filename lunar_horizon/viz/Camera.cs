﻿using System;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Drawing;
using OpenTK.Input;
using MouseEventArgs = System.Windows.Forms.MouseEventArgs;

namespace lunar_horizon.viz
{
    public class Camera
    {
        public const double NearFieldDistanceThreshold = 0.01d;
        public OpenGLControlWrapper Wrapper;
        public Shape Target;
        protected bool _dirty = false;
        protected Vector3d _eye;
        protected Vector3d _lookat; // position we're looking at
        protected Vector3d _up = new Vector3d(0d, 0d, 1d); // always normalized
        public float FieldOfView = 10f;
        public Matrix4 ViewMatrix = Matrix4.Identity;

        public Camera(OpenGLControlWrapper w, Shape t)
        {
            Wrapper = w;
            Target = t;
        }

        public virtual Vector3d Eye
        {
            get { return _eye; }
            set { _eye = value; }
        }

        public virtual Vector3d Lookat
        {
            get { return _lookat; }
            set { _lookat = value; }
        }

        public virtual Vector3d Up
        {
            get { return _up; }
            set { _up = value; }
        }

        public virtual bool Dirty
        {
            get { return _dirty; }
            set { _dirty = value; }
        }

        #region Ticking

        public virtual void Tick()
        {
        }

        public bool IsDirty()
        {
            return Dirty;
        }

        #endregion

        #region Dragging

        public virtual void DragStart(OpenGLControlWrapper w, MouseEventArgs e)
        {
        }

        public virtual void Drag(OpenGLControlWrapper w, MouseEventArgs e)
        {
        }

        public virtual void DragStop(OpenGLControlWrapper w, MouseEventArgs e)
        {
        }

        #endregion Dragging

        #region Painting

        public virtual void Paint(OpenGLControlWrapper w)
        {
            var eye = new Vector3(0f, 0f, 0f);
            var target = (Lookat - Eye).ToNear();
            var up = new Vector3(Up.ToFloat());
            ViewMatrix = Matrix4.LookAt(eye, target, up);

            //todo
            //Console.WriteLine(ViewMatrix);

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref ViewMatrix);

            PaintInternal(w);

            w.SwapBuffers();
        }

        public virtual void PaintInternal(OpenGLControlWrapper w)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            //w.SetupViewport(FieldOfView, 1000f, 2000000f);
            //w.DrawStars();
            w.SetupViewport(FieldOfView, 0.001f, 800f);

            //w.SetupViewport(FieldOfView, 0.001f, 1000f);
            w.PaintScene(w, Eye);
        }

        #endregion
    }

    public class ArcBall : Camera
    {
        public const double ZoomFactor = 1.1d;
        public const double ZoomGain = 0.01d;
        public const double YawGain = -Math.PI / 500d;
        public const double PitchGain = -Math.PI / 500d;
        private Point _lastLocation;
        private Vector3d _startPosition;

        public enum DragState { Idle, Scaling, ArcBalling, RotatingUp };
        private DragState _dragging;

        public ArcBall(OpenGLControlWrapper w, Shape t)
            : base(w, t)
        {
            _lookat = new Vector3d(0d, 0d, 0d);
            _eye = new Vector3d(10d, 0d, 0d);
        }

        public override Vector3d Eye
        {
            get { return Target == null ? _eye : Target.Position + _eye; }
            set { _eye = Target == null ? value : value - Target.Position; }
        }

        public override Vector3d Lookat
        {
            get { return Target == null ? _lookat : Target.Position + _lookat; }
            set { _lookat = Target == null ? value : value - Target.Position; }
        }

        public Vector3d RelativePosition
        {
            get { return _eye - _lookat; }
            set { _eye = value - _lookat; }
        }

        public override void Tick()
        {
        }

        public override void DragStart(OpenGLControlWrapper w, MouseEventArgs e)
        {
            _lastLocation = e.Location;
            _startPosition = _eye - _lookat;
            var circleDistance = 0.4d * Math.Min(w.Width, w.Height);
            var dx = e.X - w.Width / 2;
            var dy = e.Y - w.Height / 2;
            var d = Math.Sqrt(dx * dx + dy * dy);
            if (d > circleDistance)
                _dragging = DragState.RotatingUp;
            else switch (e.Button)
                {
                    case MouseButtons.Left:
                        _dragging = DragState.ArcBalling;
                        break;
                    case MouseButtons.Right:
                        _dragging = DragState.Scaling;
                        break;
                    default:
                        _dragging = DragState.Idle;
                        break;
                }
        }

        public override void Drag(OpenGLControlWrapper w, MouseEventArgs e)
        {
            if (_dragging == DragState.Idle)
                return;
            Lookat = Target.Position;
            switch (_dragging)
            {
                case DragState.ArcBalling: // moving
                    {
                        var relativePosition = _eye - _lookat;
                        var lookToward = new Vector3d(-relativePosition);
                        lookToward.Normalize();
                        var right = Vector3d.Cross(lookToward, Up);
                        var yaw = YawGain * (e.Location.X - _lastLocation.X);
                        var yawMatrix = Matrix4d.CreateFromAxisAngle(Up, yaw);
                        var pitch = PitchGain * (e.Location.Y - _lastLocation.Y);
                        var pitchMatrix = Matrix4d.CreateFromAxisAngle(right, pitch);
                        var distance = relativePosition.Length;
                        relativePosition = Vector3d.TransformVector(relativePosition, yawMatrix * pitchMatrix);
                        relativePosition.Normalize();
                        relativePosition = relativePosition * distance;

                        _eye = _lookat + relativePosition;
                        relativePosition.Normalize();
                        Up = Vector3d.Cross(relativePosition, right);
                        Up.Normalize();
                        _lastLocation = e.Location;
                    }
                    break;
                case DragState.Scaling: // scaling
                    {
                        var d = ZoomGain * (e.Location.Y - _lastLocation.Y);
                        var zoom = Math.Exp(d);
                        var rp = _startPosition * (ZoomFactor * zoom);
                        _eye = _lookat + rp;
                    }
                    break;
                case DragState.RotatingUp:
                    {
                        var centerX = w.Width / 2;
                        var centerY = w.Height / 2;
                        var v1 = new Vector3d(_lastLocation.X - centerX, _lastLocation.Y - centerY, 0d);
                        var v2 = new Vector3d(e.Location.X - centerX, e.Location.Y - centerY, 0d);
                        var v3 = Vector3d.Cross(v1, v2);
                        var v3Mag = v3[2];
                        var angDeg = Math.Asin(v3Mag / (v1.Length * v2.Length));  //*180d/Math.PI
                        var axis = _lookat - _eye;
                        axis.Normalize();
                        var mat = Matrix4d.CreateFromAxisAngle(axis, -angDeg);
                        Up = Vector3d.TransformVector(Up, mat);   // was _startUp
                        Up.Normalize();
                        _lastLocation = e.Location;  // not sure about this
                    }
                    break;
            }
            Dirty = true;
            w.Invalidate();
        }

        public override void DragStop(OpenGLControlWrapper w, MouseEventArgs e)
        {
            _dragging = DragState.Idle;
        }
    }

    public class ZoomTo : Camera
    {
        public Camera Next;

        private Vector3d _eye2;
        public double LookRelaxation = 0.999d;
        public double LookThreshold = 0.99d;
        public double ZoomRelaxation = 0.95d;
        public double ZoomThreshold = 0.1d;
        //public double Distance = 1000d;

        private int _state;  // 1 means relaxing lookat, 2 means relaxing eye, 0 means idle

        public ZoomTo(OpenGLControlWrapper w, Shape t, double distanceFrom, double threshold, Camera next)
            : base(w, t)
        {
            Wrapper = w;
            _eye = w.CameraMode.Eye;
            _lookat = w.CameraMode.Lookat;
            Target = t;
            Next = next;
            ZoomThreshold = threshold;

            _eye2 = (t.Position - _eye);
            _eye2.NormalizeFast();
            _eye2 = _eye2 * -distanceFrom + t.Position;

            _state = 1;
        }

        public override void Tick()
        {
            switch (_state)
            {
                case 0:
                    break;
                case 1:
                    {
                        var p = (Lookat - Target.Position) * LookRelaxation;
                        Lookat = p + Target.Position;
                        var d1 = Lookat - Eye;
                        var d2 = Target.Position - Eye;
                        d1.NormalizeFast();
                        d2.NormalizeFast();
                        var dot = Vector3d.Dot(d1, d2);
                        if (dot > LookThreshold)
                        {
                            _state = 2;
                            Lookat = Target.Position;
                        }
                    }
                    break;
                case 2:
                    {
                        var p = (Eye - _eye2) * ZoomRelaxation;
                        Eye = p + _eye2;
                        var d = (Eye - _eye2).LengthFast;
                        //Console.WriteLine(@"relax distance={0} eye={1}", d, Eye);
                        if (d < ZoomThreshold)
                        {
                            _state = 0;
                            Wrapper.CameraMode = Next;
                            Next.Eye = Eye;
                            Next.Lookat = Lookat;
                            Next.Target = Target;
                        }
                    }
                    break;
            }
            Wrapper.Invalidate();
        }
    }

    public class InstrumentView : Camera
    {
        public Shape Satellite;
        public float Azimuth = 0f;
        public float Elevation = 0f;

        public static Vector3 X = new Vector3(1f, 0f, 0f);
        public static Vector3 Y = new Vector3(0f, 1f, 0f);
        public static Vector3 Z = new Vector3(0f, 0f, 1f);

        public InstrumentView(OpenGLControlWrapper w, Shape t)
            : base(w, t)
        {
            Satellite = t;
        }

        public override void Tick()
        {
            Eye = Satellite.Position;
        }

        public override void Paint(OpenGLControlWrapper w)
        {
            var e = MathHelper.DegreesToRadians(Elevation);
            var a = MathHelper.DegreesToRadians(Azimuth);
            var z = (float)Math.Sin(e);
            var d = Math.Cos(e);
            var x = (float)(Math.Cos(a) * d);
            var y = (float)(Math.Sin(a) * d);
            var target = new Vector3(x, y, z);

            var eye = new Vector3(0f, 0f, 0f);
            var up = new Vector3(0f, 0f, 1f);
            ViewMatrix = Matrix4.LookAt(eye, target, up);

            GL.MatrixMode(MatrixMode.Modelview);  // This worked with the lookat in s/c frame
            GL.LoadMatrix(ref ViewMatrix);
            Satellite.Attitude.ToAxisAngle(out Vector3 axis, out float angle);
            GL.Rotate(angle * 180.0f / 3.141593f, axis);

            PaintInternal(w);

            w.SwapBuffers();
        }
    }

    public class TelescopeView : InstrumentView
    {
        public TelescopeView(OpenGLControlWrapper w, Shape center, float fov = 4f)
            : base(w, center)
        {
            Azimuth = 170.1f;
            Elevation = 0f;
            FieldOfView = fov;
        }

        public override void PaintInternal(OpenGLControlWrapper w)
        {
            base.PaintInternal(w);

            GL.Disable(EnableCap.Lighting);

            GL.MatrixMode(MatrixMode.Modelview);  // This worked with the lookat in s/c frame
            GL.LoadIdentity();
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();

            var width = w.Width;
            var height = w.Height;
            var aspectRatio = width / (float)height;

            GL.Ortho(-aspectRatio, aspectRatio, -1.0, 1.0, 0.0, 4.0);
            GL.Viewport(0, 0, width, height);

            const double solarViewerFOVRadius = 0.5d;
            var radius = solarViewerFOVRadius * (2f / FieldOfView);
            const int numPoints = 32;
            const double frac = (2d * Math.PI) / numPoints;

            GL.Color3(Color.CornflowerBlue);
            GL.Begin(BeginMode.LineStrip);
            for (var i = 0; i <= numPoints; i++)
            {
                var px = radius * Math.Cos(i * frac);
                var py = radius * Math.Sin(i * frac);
                GL.Vertex2(px, py);
            }
            GL.End();

            GL.Enable(EnableCap.Lighting);
        }
    }

    public class SolarViewerView : InstrumentView
    {
        public float HackAngle = 0f;
        public SolarViewerView(OpenGLControlWrapper w, Shape center, float fov = 4f)
            : base(w, center)
        {
            Azimuth = 157.657f;
            Elevation = -0.109f;
            FieldOfView = fov;
        }

        public override void PaintInternal(OpenGLControlWrapper w)
        {
            base.PaintInternal(w);

            GL.Disable(EnableCap.Lighting);

            GL.MatrixMode(MatrixMode.Modelview);  // This worked with the lookat in s/c frame
            GL.LoadIdentity();
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();

            var width = w.Width;
            var height = w.Height;
            var aspectRatio = width / (float)height;

            GL.Ortho(-aspectRatio, aspectRatio, -1.0, 1.0, 0.0, 4.0);
            GL.Viewport(0, 0, width, height);

            const double solarViewerFOVRadius = 0.5d;
            var radius = solarViewerFOVRadius * (2f / FieldOfView);
            const int numPoints = 32;
            const double frac = (2d * Math.PI) / numPoints;

            GL.Color3(Color.Yellow);
            GL.Begin(BeginMode.LineStrip);
            for (var i = 0; i <= numPoints; i++)
            {
                var px = radius * Math.Cos(i * frac);
                var py = radius * Math.Sin(i * frac);
                GL.Vertex2(px, py);
            }
            GL.End();

            GL.Enable(EnableCap.Lighting);
        }
    }

    public class JoystickCamera : Camera
    {
        public const double InitialWalkSpeed = 0.02d;
        public const double InitialTurnSpeed = 0.02d; //0.005d;
        public const double InitialStrafeSpeed = 0.003d;
        public const double RotateUpSpeed = -InitialWalkSpeed;  // /3d

        private const float Slowdown = 2f;

        // Used to scale the motion based on framerate
        public double MotionScale = .2d;
        public double StrafeSpeed = InitialStrafeSpeed;
        public double TurnSpeed = InitialTurnSpeed;
        public double WalkSpeed = InitialWalkSpeed;
        public double RotateSpeed = InitialTurnSpeed;

        private bool _lastGoFast;
        private bool _lastGoSlow;

        private Vector3d _forward;
        private Vector3d _right;

        public JoystickCamera(OpenGLControlWrapper w, Shape t)
            : base(w, t)
        {
            Wrapper = w;

            //_eye = new Vector3d(10d, 10d, 5d);
            // _lookat = new Vector3d(0.0d, 0.0d, 0.0d);
        }

        public override Vector3d Eye
        {
            get { return Target == null ? _eye : Target.Position + _eye; }
            set { _eye = Target == null ? value : value - Target.Position; }
        }

        public override Vector3d Lookat
        {
            get { return Target == null ? _lookat : Target.Position + _lookat; }
            set { _lookat = Target == null ? value : value - Target.Position; }
        }


        // Rebuild _forward and _right from _lookat, _eye and _up
        public void ResetVectors()
        {
            _forward = _lookat - _eye;
            _forward.Normalize();
            _up.Normalize();
            _right = Vector3d.Cross(_forward, _up);
            _right.Normalize();
            _lookat = _eye + _forward;
        }

        public void Reset()
        {
            _eye = new Vector3d(10d, 10d, 5d);
            _lookat = new Vector3d(0.0d, 0.0d, 0.0d);
            StrafeSpeed = InitialStrafeSpeed;
            TurnSpeed = InitialTurnSpeed;
            WalkSpeed = InitialWalkSpeed;
        }

        public void MoveForward(double d)
        {
            var t = _forward * MotionScale * d;
            _eye += t;
            _lookat = _eye + _forward;
            _dirty = true;
            //Console.WriteLine(@"Joystick move forward");
        }

        public void MoveRight(double d)
        {
            var t = _right * MotionScale * d;
            _eye += t;
            _lookat = _eye + _forward;
            _dirty = true;
            //Console.WriteLine(@"Joystick move right");
        }

        public void Strafe(double xd, double yd)
        {
            var x = _right * MotionScale * xd;
            var y = _up * MotionScale * yd;
            _eye = _eye - x + y;
            _lookat = _eye + _forward;
            _dirty = true;
            //Console.WriteLine(@"Joystick strafe");
        }

        public void Rotate(double x, double y)
        {
            _forward = _forward + x * MotionScale * _right;
            _forward.Normalize();
            _right = Vector3d.Cross(_forward, _up);
            _right.Normalize();

            _forward = _forward + y * MotionScale * _up;
            _forward.Normalize();
            _up = Vector3d.Cross(_right, _forward);
            _up.Normalize();

            _lookat = _eye + _forward;
            _dirty = true;
            //Console.WriteLine(@"Joystick rotate");
        }

        public void RotateUp(double x)
        {
            _right = _right + x * RotateUpSpeed * _up;
            _right.Normalize();
            _up = Vector3d.Cross(_right, _forward);
            _up.Normalize();
            _dirty = true;
            //Console.WriteLine(@"Joystick rotate up");
        }

        public void SlowDown()
        {
            WalkSpeed /= Slowdown;
            //TurnSpeed /= Slowdown;
            StrafeSpeed /= Slowdown;

            Console.WriteLine(@"WalkSpeed={0}", WalkSpeed);
        }

        public void SpeedUp()
        {
            WalkSpeed *= Slowdown;
            //TurnSpeed *= Slowdown;
            StrafeSpeed *= Slowdown;

            Console.WriteLine(@"WalkSpeed={0}", WalkSpeed);
        }

        public void Describe()
        {
            Console.WriteLine(@"Camera info:");
            Console.WriteLine(@"eye:    [" + Eye.X + "," + Eye.Y + "," + Eye.Z + "]");
            Console.WriteLine(@"lookat: [" + Lookat.X + "," + Lookat.Y + "," + Lookat.Z + "]");
        }

        public override void Tick()
        {
            UpdateInputState();
            //Console.WriteLine(@"Tick");
            //Describe();
            Wrapper.Invalidate();
        }

        public void UpdateInputState()
        {
            JoystickState state = Joystick.GetState(0);
            if (state.IsConnected)
            {
                if (false)
                {
                    Console.WriteLine(@"axis0={0}", state.GetAxis(JoystickAxis.Axis0)); // side to side
                    Console.WriteLine(@"axis1={0}", state.GetAxis(JoystickAxis.Axis1)); // forward back
                    Console.WriteLine(@"axis2={0}", state.GetAxis(JoystickAxis.Axis2)); // knob
                    Console.WriteLine(@"axis3={0}", state.GetAxis(JoystickAxis.Axis3)); // z-rotation
                    Console.WriteLine(@"axis4={0}", state.GetAxis(JoystickAxis.Axis4));
                    Console.WriteLine(@"axis5={0}", state.GetAxis(JoystickAxis.Axis5));
                    Console.WriteLine(@"axis6={0}", state.GetAxis(JoystickAxis.Axis6));
                    Console.WriteLine(@"axis7={0}", state.GetAxis(JoystickAxis.Axis7));

                    Console.WriteLine(@"button0={0}", state.GetButton(JoystickButton.Button0));
                    Console.WriteLine(@"button1={0}", state.GetButton(JoystickButton.Button1));
                    Console.WriteLine(@"button2={0}", state.GetButton(JoystickButton.Button2));
                    Console.WriteLine(@"button3={0}", state.GetButton(JoystickButton.Button3));
                    Console.WriteLine(@"button4={0}", state.GetButton(JoystickButton.Button4));
                    Console.WriteLine(@"button5={0}", state.GetButton(JoystickButton.Button5));
                    Console.WriteLine(@"button6={0}", state.GetButton(JoystickButton.Button6));
                    Console.WriteLine(@"button7={0}", state.GetButton(JoystickButton.Button7));
                    Console.WriteLine(@"button8={0}", state.GetButton(JoystickButton.Button8));
                    Console.WriteLine(@"button9={0}", state.GetButton(JoystickButton.Button9));
                    Console.WriteLine(@"button10={0}", state.GetButton(JoystickButton.Button10)); // hat up
                    Console.WriteLine(@"button11={0}", state.GetButton(JoystickButton.Button11)); // hat left
                    Console.WriteLine(@"button12={0}", state.GetButton(JoystickButton.Button12)); // hat right
                    Console.WriteLine(@"button13={0}", state.GetButton(JoystickButton.Button13)); // hat down
                    Console.WriteLine(@"button14={0}", state.GetButton(JoystickButton.Button14));
                    Console.WriteLine(@"button15={0}", state.GetButton(JoystickButton.Button15));

                    Console.ReadKey();
                }

                var x = state.GetAxis(JoystickAxis.Axis0);     // used for rotating up
                var y = state.GetAxis(JoystickAxis.Axis1);
                var z = state.GetAxis(JoystickAxis.Axis3);
                var forward = state.GetAxis(JoystickAxis.Axis2);

                var strafeUp = state.GetButton(JoystickButton.Button10) == OpenTK.Input.ButtonState.Pressed;
                var strafeLeft = state.GetButton(JoystickButton.Button11) == OpenTK.Input.ButtonState.Pressed;
                var strafeRight = state.GetButton(JoystickButton.Button12) == OpenTK.Input.ButtonState.Pressed;
                var strafeDown = state.GetButton(JoystickButton.Button13) == OpenTK.Input.ButtonState.Pressed;
                var reset = state.GetButton(JoystickButton.Button8) == OpenTK.Input.ButtonState.Pressed;
                var goSlow = state.GetButton(JoystickButton.Button1) == OpenTK.Input.ButtonState.Pressed;
                var goFast = state.GetButton(JoystickButton.Button3) == OpenTK.Input.ButtonState.Pressed;

                if (Math.Abs(forward) > 0.1f)
                    MoveForward(-forward * WalkSpeed);

                if (Math.Abs(x) > 0.1f)
                    RotateUp(x);

                if (strafeUp || strafeDown
                    || strafeRight
                    || strafeLeft)
                {
                    var strafeX = strafeLeft ? StrafeSpeed : strafeRight ? -StrafeSpeed : 0f;
                    var strafeY = strafeUp ? StrafeSpeed : strafeDown ? -StrafeSpeed : 0f;
                    Strafe(strafeX, strafeY);
                }

                if (z > 0.05f || z < -0.05f || y > 0.05f || y < -0.05f)
                {
                    //Console.Out.WriteLine("x="+z+" y="+z);
                    Rotate(z * RotateSpeed, y * RotateSpeed);
                }

                if (reset)
                    Reset();

                if (!_lastGoSlow && goSlow)
                    SlowDown();
                if (!_lastGoFast && goFast)
                    SpeedUp();
                _lastGoSlow = goSlow;
                _lastGoFast = goFast;
            }

            if (false) // this.Focused || GraphicsWindow.Focused)
            {
                //DirectInput.KeyboardState ks = keyboardDevice.GetCurrentKeyboardState();
                //float m = 1f;
                //if (ks[DirectInput.Key.LeftShift]) m = 10f;
                //if (ks[DirectInput.Key.W]) camera.MoveForward(m);
                //if (ks[DirectInput.Key.S]) camera.MoveForward(-m);
                //if (ks[DirectInput.Key.A]) camera.Strafe(-m, 0f);
                //if (ks[DirectInput.Key.D]) camera.Strafe(m, 0f);
                //if (ks[DirectInput.Key.E]) camera.Strafe(0f, m);
                //if (ks[DirectInput.Key.C]) camera.Strafe(0f, -m);
                //if (ks[DirectInput.Key.P]) camera.describe();
            }
        }

    }


}
