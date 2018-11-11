using System;
using System.Collections.Generic;
using System.IO;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace lunar_horizon.viz
{
    public class StarBackground : Shape
    {
        public const float StarDistance = 1600000f;
        public string Filename = @"stars.mag.data";
        public List<VBO> Handles;
        public float[] PointSizes = {3f, 2f, 1.5f, 1f, 1f, 1f, 1f, 1f, 1f, 1f};

        public void Load()
        {
            using (var s = new StreamReader(Filename))
            {
                Handles = new List<VBO>();
                var seps = new[] {' '};
                var count = int.Parse(s.ReadLine());
                float threshold = 2f;
                var lst = new List<Vector3>();
                string l;
                while (null != (l = s.ReadLine()))
                {
                    string[] tokens = l.Split(seps, StringSplitOptions.RemoveEmptyEntries);
                    float x = float.Parse(tokens[0]);
                    float y = float.Parse(tokens[1]);
                    float z = float.Parse(tokens[2]);
                    float mag = float.Parse(tokens[3]);
                    if (mag > threshold)
                    {
                        Handles.Add(GetHandleForList(lst.ToArray()));
                        lst = new List<Vector3>();
                        threshold += 2f;
                    }
                    lst.Add(new Vector3(StarDistance*x, StarDistance*y, StarDistance*z));
                }
                if (lst.Count > 0)
                    Handles.Add(GetHandleForList(lst.ToArray()));
            }
        }

        private VBO GetHandleForList(Vector3[] ary)
        {
            var handle = new VBO();
            GL.GenBuffers(1, out handle.VboID);
            GL.BindBuffer(BufferTarget.ArrayBuffer, handle.VboID);
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr) (ary.Length*BlittableValueType.StrideOf(ary)), ary,
                          BufferUsageHint.StaticDraw);
            GL.GetBufferParameter(BufferTarget.ArrayBuffer, BufferParameterName.BufferSize, out int size);
            if (ary.Length*BlittableValueType.StrideOf(ary) != size)
                throw new ApplicationException("Vertex data not uploaded correctly");
            handle.NumElements = ary.Length;
            return handle;
        }

        public override void Transform(bool near, Vector3d eye)
        {
            Attitude.ToAxisAngle(out Vector3 axis, out float angle);
            GL.Rotate(-angle * 180.0f / 3.141593f, axis);
        }

        public override void Paint()
        {
            GL.Color3(1f, 1f, 1f);
            var min = Math.Min(Handles.Count, PointSizes.Length);
            for (var i = 0; i < min; i++)
            {
                VBO handle = Handles[i];
                GL.PointSize(PointSizes[i]);
                GL.BindBuffer(BufferTarget.ArrayBuffer, handle.VboID);
                GL.InterleavedArrays(InterleavedArrayFormat.V3f, 0, IntPtr.Zero);
                GL.DrawArrays(BeginMode.Points, 0, handle.NumElements);
            }
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }
    }
}