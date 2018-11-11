using System;
using System.Runtime.InteropServices;
using OpenTK.Graphics.OpenGL;
using OpenTK;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using PixelFormat = System.Drawing.Imaging.PixelFormat;
using System.Collections.Generic;
using lunar_horizon.patches;
using lunar_horizon.terrain;
using System.Linq;

namespace lunar_horizon.viz
{
    public class MoonDEM : Shape
    {
        public const double Kilometers = 0.001d;
        public const double MetersToFarUnits = 0.000001d;
        public const double Radius = 1.7374d;
        public static bool ShowTexture = true;
        public string TextureFilename = null;
        public Color Color = Color.White;

        public bool SurfaceLines = false;

        //protected int TextureHandle;
        protected int ElementsHandle;
        public int NumElements;
        public InterleavedArrayFormat VertexFormat;

        public uint[] Elements;

        public List<TerrainPatch3D> Meshes = new List<TerrainPatch3D>();
        public List<TerrainPatch> Patches = new List<TerrainPatch>();

        public NormalAverager[] NormalBuffer = new NormalAverager[0];

        public List<TraversePath> TraversePaths = new List<TraversePath>();

        public MoonDEM()
        {
            VertexFormat = InterleavedArrayFormat.T2fN3fV3f;
        }

        public InMemoryTerrainManager Terrain;

        public void Load(List<TerrainPatch> patches)
        {
            Patches = patches;
            //LoadTexture();
            LoadElements();
            LoadDEMs();

            BoundingSphereRadius = Radius + 0.3d;
            BoundingSphereDefined = true;
        }

        public void LoadElements()
        {
            const int xSize = TerrainPatch3D.Width;
            const int ySize = TerrainPatch3D.Height;

            // Define a mesh
            var ptr = 0;
            const int xMax = xSize - 1;
            const int yMax = ySize - 1;
            var elements = new uint[xMax * yMax * 6];
            for (var x = 0; x < xMax; x++)
                for (var y = 0; y < yMax; y++)
                {
                    var v = y * xSize + x;

                    elements[ptr++] = (uint)v;               // c
                    elements[ptr++] = (uint)(v + 1);         // b
                    elements[ptr++] = (uint)(v + xSize);     // a

                    elements[ptr++] = (uint)(v + 1);         // c
                    elements[ptr++] = (uint)(v + xSize + 1); // b
                    elements[ptr++] = (uint)(v + xSize);     // a
                }

            GL.GenBuffers(1, out ElementsHandle);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementsHandle);
            GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)(elements.Length * sizeof(uint)), elements,
                          BufferUsageHint.StaticDraw);
            GL.GetBufferParameter(BufferTarget.ElementArrayBuffer, BufferParameterName.BufferSize, out int size);
            if (elements.Length * sizeof(uint) != size)
                throw new ApplicationException("Element data not uploaded correctly");

            NumElements = elements.Length;
            Elements = elements;
        }

        public void LoadDEMs()
        {
            Meshes = Patches.Select(p => new TerrainPatch3D(this, p)).ToList();

            foreach (var f in Meshes)
            {
                f.LoadTexture();
                f.LoadRam();
                f.LoadGPU();
                f.UnloadRam();
            }
        }

        public override void Paint()
        {
            //if (ShowAxes)
            //    PaintAxes(AxisScale);

            if (Shader != null)
                Shader.UseProgram();

            GL.Color3(Color);
            GL.Material(MaterialFace.Front, MaterialParameter.Specular, Specularity);
            GL.Material(MaterialFace.Front, MaterialParameter.Shininess, Shininess);  //Shininess
            GL.ShadeModel(ShadingModel.Smooth);

            //Debugging
            if (false)
            {
                GL.Disable(EnableCap.Lighting);
                GL.Color3(Color.Beige);
                GL.Begin(BeginMode.Lines);
                foreach (var p in Patches)
                {
                    GL.Vertex3(0f, 0f, 0f);
                    var pt = p.Points[0][0];
                    GL.Vertex3(pt.X * Kilometers, pt.Y * Kilometers, pt.Z * Kilometers);
                }
                GL.End();
            }

            //GL.Enable(EnableCap.Texture2D);
            //GL.BindTexture(TextureTarget.Texture2D, TextureHandle);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementsHandle);

            for (var i = 0; i < Meshes.Count; i++)
            {
                var t = Meshes[i];
                t.Paint(ShowTexture, VertexFormat, NumElements);
            }

            // testing
            //var t = Fragments[0];
            //t.Paint(ShowTexture, VertexFormat, NumElements);

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
            GL.DisableClientState(ArrayCap.VertexArray);
            GL.DisableClientState(ArrayCap.NormalArray);
            GL.DisableClientState(ArrayCap.IndexArray);
            //GL.Disable(EnableCap.Texture2D);

            if (Shader != null)
                Shader.StopUsingProgram();

            GL.Disable(EnableCap.Lighting);
            GL.Disable(EnableCap.DepthTest);
            GL.LineWidth(3f);
            foreach (var traverse in TraversePaths)
                foreach (var kv in traverse.Paths)
                {
                    GL.Color3(kv.Key);
                    GL.Begin(BeginMode.Lines);
                    var lst = kv.Value;
                    for (var i = 0; i < lst.Count - 1; i++)
                    {
                        GL.Vertex3(lst[i]);
                        GL.Vertex3(lst[i + 1]);
                    }
                    GL.End();
                }
            GL.LineWidth(1f);
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.Lighting);
        }

        public NormalAverager[] GetNormalAverager(int count)
        {
            if (NormalBuffer != null && NormalBuffer.Length >= count)
                return NormalBuffer;
            NormalBuffer = new NormalAverager[count];
            return NormalBuffer;
        }

        public void UpdateShadowcaster(math.Vector3d caster)
        {
            foreach (var child in Meshes)
                child.UpdateShadowcaster(caster);
        }
    }

    public class TerrainPatch3D : TexturedShape
    {
        public const double Kilometers = 0.001d;
        public const double MetersToFarUnits = 0.000001d;
        public const int XSteps = TerrainPatch.DefaultSize;       // 128
        public const int YSteps = TerrainPatch.DefaultSize;       // 128
        public const int Width = TerrainPatch.DefaultSize;        // 128 - for now, forget the strips between
        public const int Height = TerrainPatch.DefaultSize;       // 128
        public const int VertexCount = Width * Height;            // 16384
        public const int VertexByteCount = VertexCount * 32;      // 524288   ... sizeof(Vertex)=32;
        public const int TriangleCount = TerrainPatch.DefaultSize * TerrainPatch.DefaultSize * 2;     // 32768
        public const int TriangleByteCount = TriangleCount * 6;   // 393216

        public MoonDEM DEM;
        public TerrainPatch Patch;

        public Vertex[] CornerVertices = new Vertex[4];

        public bool InRam = false;
        public bool OnGPU = false;

        public int VertexHandle;
        public int TextureHandle;

        public string Filename;

        public byte[] Vertices;

        public TerrainPatch3D(MoonDEM dem, TerrainPatch p)
        {
            DEM = dem;
            Patch = p;
        }

        public override void LoadTexture()
        {
            LoadTexture(Patch.GetHillshade());
        }

        public void LoadTexture(Bitmap bitmap)
        {
            if (TextureHandle==0)
                GL.GenTextures(1, out TextureHandle);
            GL.BindTexture(TextureTarget.Texture2D, TextureHandle);

            var data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                                  ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            GL.TexEnv(TextureEnvTarget.TextureEnv, TextureEnvParameter.TextureEnvMode, (float)TextureEnvMode.Modulate);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (float)TextureMinFilter.LinearMipmapLinear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (float)TextureMagFilter.Linear);

            // tell OpenGL to build mipmaps out of the bitmap data
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.GenerateMipmap, (float)1.0f);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0,
                          OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);

            bitmap.UnlockBits(data);
        }

        public void LoadRam()
        {
            Patch.FillPoints(tiles.TileTreeNode.Terrain);
            var width = Patch.Width;
            var height = Patch.Height;
            var vertices = new Vertex[VertexCount];
            if (vertices.Length > 65536)
                throw new Exception("Mesh too large");
            float xf = 1f / (width - 1);
            float yf = 1f / (height - 1);
            for (var line = 0; line < height; line++)
            {
                var points_row = Patch.Points[line];
                for (var sample = 0; sample < width; sample++)
                {
                    var idx = line * height + sample;
                    vertices[idx].TexCoord.X = sample * xf;
                    vertices[idx].TexCoord.Y = line * yf;

                    var pt = points_row[sample];

                    vertices[idx].Normal.X = 0f;
                    vertices[idx].Normal.Y = 0f;
                    vertices[idx].Normal.Z = 1f;

                    vertices[idx].Position.X = (float)(pt.X * Kilometers);
                    vertices[idx].Position.Y = (float)(pt.Y * Kilometers);
                    vertices[idx].Position.Z = (float)(pt.Z * Kilometers);

                    //Console.WriteLine($"[{vertices[idx].Position.X},{vertices[idx].Position.Y},{vertices[idx].Position.Z}]");
                }
            }

            // Define a mesh
            var elements = new ushort[(width - 1) * (height - 1) * 6];
            {
                var ptr = 0;
                var xMax = width - 1;
                var yMax = height - 1;
                for (var x = 0; x < xMax; x++)
                    for (var y = 0; y < yMax; y++)
                    {
                        var v = x * height + y;

                        //Console.WriteLine(@"v={0}", v);

                        elements[ptr++] = (ushort)(v + height);     // a
                        elements[ptr++] = (ushort)(v + 1);         // b
                        elements[ptr++] = (ushort)v;               // c

                        //Console.WriteLine(@"tri [{0}, {1}, {2}]", x + YSize, v + 1, v);

                        elements[ptr++] = (ushort)(v + height);     // a
                        elements[ptr++] = (ushort)(v + height + 1); // b
                        elements[ptr++] = (ushort)(v + 1);         // c

                        //Console.WriteLine(@"tri [{0}, {1}, {2}]", v + YSize, v + YSize + 1, v + 1);
                    }
            }

            if (true)
            {
                // Average the normals
                var buf = DEM.GetNormalAverager(VertexCount);
                for (var i = 0; i < VertexCount; i++)
                    buf[i].Reset();

                var triCount = elements.Length / 3;
                var ptr = 0;
                for (var tri = 0; tri < triCount; tri++)
                {
                    var p1 = elements[ptr++];
                    var p2 = elements[ptr++];
                    var p3 = elements[ptr++];
                    var v1 = vertices[p1].Position;
                    var v2 = vertices[p2].Position;
                    var v3 = vertices[p3].Position;

                    //Console.WriteLine(@"textureCoords=[{0},{1}], [{2},{3}], [{4},{5}]",
                    //    v[p1].TexCoord.X, v[p1].TexCoord.Y,
                    //    v[p2].TexCoord.X, v[p2].TexCoord.Y,
                    //    v[p3].TexCoord.X, v[p3].TexCoord.Y);

                    Shape.FindNormal(ref v1, ref v2, ref v3, out Vector3 n);

                    if (float.IsNaN(n.X))
                        throw new Exception(@"Got NaN when creating a mesh normal");

                    buf[p1].Add(n);
                    buf[p2].Add(n);
                    buf[p3].Add(n);

                    /*
                    if (tri == 0)
                    {
                        Console.WriteLine(@"First tri");
                        Console.WriteLine(@"  p1={0} p2={1} p3={2}", p1, p2, p3);
                        Console.WriteLine(@"  v1={0} v2={1} v3={2}", v1, v2, v3);
                        Console.WriteLine(@"  n1={0} n2={1} n3={2}", v[p1].Normal, v[p2].Normal, v[p3].Normal);
                        Console.WriteLine(@"  calculated normal={0}", n);
                        Console.WriteLine();
                    }
                    */

                    /*
                    buf[p1].Add(v[p1].Normal);
                    buf[p2].Add(v[p1].Normal);
                    buf[p3].Add(v[p2].Normal);
                    */
                }
                for (var i = 0; i < VertexCount; i++)
                {
                    var norm = buf[i].Normal / buf[i].Count;
                    norm.Normalize();

                    /*
                    if (i == 0 || i == 1 || i == 257)
                    {
                        Console.WriteLine(@"point[{0}].Normal (before)={1}", i, v[i].Normal);
                        Console.WriteLine(@"new normal={0}", norm);
                    }
                    */

                    vertices[i].Normal = norm;
                }
            }

            //Buffer = LoadVBO(vertices, elements, InterleavedArrayFormat.T2fN3fV3f);

            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    for (var i = 0; i < vertices.Length; i++)
                    {
                        bw.Write((float)vertices[i].TexCoord.X);
                        bw.Write((float)vertices[i].TexCoord.Y);
                        bw.Write((float)vertices[i].Normal.X);
                        bw.Write((float)vertices[i].Normal.Y);
                        bw.Write((float)vertices[i].Normal.Z);
                        bw.Write((float)vertices[i].Position.X);
                        bw.Write((float)vertices[i].Position.Y);
                        bw.Write((float)vertices[i].Position.Z);
                    }
                }
                Vertices = ms.GetBuffer();
            }

            CornerVertices[0] = vertices[0 * Width + 0];
            CornerVertices[1] = vertices[0 * Width + (Width - 1)];
            CornerVertices[2] = vertices[(Height - 1) * Width + 0];
            CornerVertices[3] = vertices[(Height - 1) * Width + (Width - 1)];

            BoundingSphereRadius = 100f;  // Radius;
            BoundingSphereDefined = true;
        }

        private string tos(double v)
        {
            return v < 0d ? (-v).ToString(@"-000.00000") : v.ToString(@"000.00000");
        }

        public void LoadGPU()
        {
            if (VertexHandle > 0)
                return;
            GL.GenBuffers(1, out VertexHandle);
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexHandle);
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(Vertices.Length * BlittableValueType.StrideOf(Vertices)), Vertices, BufferUsageHint.StaticCopy);
            GL.GetBufferParameter(BufferTarget.ArrayBuffer, BufferParameterName.BufferSize, out int size);
            if (Vertices.Length * BlittableValueType.StrideOf(Vertices) != size)
                throw new ApplicationException("Vertex data not uploaded correctly");
        }

        public void UnloadGPU()
        {
            if (VertexHandle < 1)
                return;
            GL.DeleteBuffer(VertexHandle);
            VertexHandle = 0;
        }

        public void UnloadRam()
        {
            Vertices = null;
        }

        public void Paint(bool showTexture, InterleavedArrayFormat vertexFormat, int numElements)
        {
            //DEM.SurfaceLines = true;
            if (false)
            {
                GL.Disable(EnableCap.Lighting);
                GL.Begin(BeginMode.Lines);
                GL.Color3(Color.Beige);
                var pt1 = Patch.Points[0][0] * Kilometers;
                var pt2 = Patch.Points[127][0] * Kilometers;
                var pt3 = Patch.Points[127][127] * Kilometers;
                var pt4 = Patch.Points[0][127] * Kilometers;

                GL.Vertex3(pt1.X, pt1.Y, pt1.Z);
                GL.Vertex3(pt2.X, pt2.Y, pt2.Z);

                GL.Vertex3(pt2.X, pt2.Y, pt2.Z);
                GL.Vertex3(pt3.X, pt3.Y, pt3.Z);

                GL.Vertex3(pt3.X, pt3.Y, pt3.Z);
                GL.Vertex3(pt4.X, pt4.Y, pt4.Z);

                GL.Vertex3(pt4.X, pt4.Y, pt4.Z);
                GL.Vertex3(pt1.X, pt1.Y, pt1.Z);

                GL.End();
                GL.Enable(EnableCap.Lighting);
            }

            if (VertexHandle > 0)
            {
                GL.Disable(EnableCap.Lighting);
                GL.Color3(Color);
                GL.BindBuffer(BufferTarget.ArrayBuffer, VertexHandle);
                GL.InterleavedArrays(vertexFormat, 0, IntPtr.Zero);       // Can this go outside the loop?

                if (TextureHandle > 0)
                {
                    GL.Enable(EnableCap.Texture2D);
                    GL.BindTexture(TextureTarget.Texture2D, TextureHandle);
                }

                if (!DEM.SurfaceLines)
                {
                    GL.DrawElements(showTexture ? BeginMode.Triangles : BeginMode.LineStrip,
                                    numElements, DrawElementsType.UnsignedInt, IntPtr.Zero);
                }
                else
                {
                    //   GL.DrawElements(BeginMode.Triangles, numElements, DrawElementsType.UnsignedInt, IntPtr.Zero);
                    //   GL.DepthFunc(DepthFunction.Lequal);

                    GL.Disable(EnableCap.Lighting);
                    GL.Color3(1f, 1f, 0f);
                    GL.DrawElements(BeginMode.LineStrip, numElements, DrawElementsType.UnsignedInt, IntPtr.Zero);
                    GL.DepthFunc(DepthFunction.Less);
                    GL.Enable(EnableCap.Lighting);
                }

                if (TextureHandle > 0)
                    GL.Disable(EnableCap.Texture2D);
                GL.Enable(EnableCap.Lighting);
            }
            else
            {
                Console.WriteLine($"Paint: no vertices for {Patch}]");
            }
        }

        internal void UpdateShadowcaster(math.Vector3d caster)
        {
            LoadTexture(Patch.GetShadows(caster));
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct NormalAverager
    {
        public int Count;
        public OpenTK.Vector3 Normal;
        public void Reset()
        {
            Count = 0;
            Normal.X = Normal.Y = Normal.Z = 0f;
        }
        public void Add(OpenTK.Vector3 v)
        {
            Count++;
            Normal.X += v.X;
            Normal.Y += v.Y;
            Normal.Z += v.Z;
        }
    }

    public class TraversePath
    {
        public Dictionary<Color, List<Vector3>> Paths = new Dictionary<Color, List<Vector3>>();
    }
}