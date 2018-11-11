using OpenTK.Graphics.OpenGL;
using System.Runtime.InteropServices;
using OpenTK;

namespace lunar_horizon.viz
{
    public struct VBO
    { 
        public int VboID, EboID, NumElements;
        public InterleavedArrayFormat VertexFormat;
    }

    [StructLayout(LayoutKind.Sequential, Pack=1)]
    public struct Vertex
    { // mimic InterleavedArrayFormat.T2fN3fV3f
        public Vector2 TexCoord;
        public Vector3 Normal;
        public Vector3 Position;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct VertexNormal
    {
        // mimic InterleavedArrayFormat.N3fV3f
        public Vector3 Normal;
        public Vector3 Position;
    }

}
