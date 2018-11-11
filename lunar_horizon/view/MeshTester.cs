using lunar_horizon.patches;
using lunar_horizon.terrain;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace lunar_horizon.view
{
    public partial class MeshTester : Form
    {
        public InMemoryTerrainManager Terrain;
        Panel _pnlDraw;
        MeshGenerator _gen;
        Point[] _vertices;
        int[][] _faces;

        MeshGenerator _generator;

        public MeshTester()
        {
            InitializeComponent();

            _pnlDraw = new Panel { Location = new Point(0, 0), Size = new Size(10000,10000) };
            _pnlDraw.Paint += pnlDraw_Paint;
            pnlScroll.Controls.Add(_pnlDraw);
        }

        void GetVerticesAndEdges()
        {
            _vertices = _generator?.EnumerateVertices().ToArray();

        }

        private void pnlDraw_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            //g.DrawRectangle(Pens.Green, 0, 0, 1, 1);

            DrawVertices(e.Graphics);
            DrawFaces(e.Graphics);
        }

        PointF TransformVertex(Point v) => new PointF(30f + VertexDrawScale * v.X + VertexDrawOffset.X, 30f + VertexDrawScale * v.Y + VertexDrawOffset.Y);

        public PointF VertexDrawOffset;
        public float VertexDrawScale = 8f;

        private void DrawVertices(Graphics g)
        {
            const float vertex_radius = 0.1f;
            const float vertex_radius2 = 2f * vertex_radius;

            if (_vertices == null)
                GetVerticesAndEdges();

            if (_vertices == null)
                return;
            foreach (var v in _vertices)
            {
                var tv = TransformVertex(v);
                g.FillEllipse(Brushes.Red, tv.Y - vertex_radius, tv.X - vertex_radius, vertex_radius2, vertex_radius2);
            }
        }

        private void DrawFaces(Graphics g)
        {
            if (_vertices == null || _generator == null)
                return;
            var drawEdges = edgesToolStripMenuItem.Checked;
            var drawFaces = facesToolStripMenuItem.Checked;
            var points = new PointF[4];  // We know they're all triangles
            foreach (var face in _generator.EnumerateFaces())
            {
                for (var i = 0; i < face.Length; i++)
                {
                    var v = _vertices[face[i]];
                    points[i] = TransformVertex(v);
                }
                points[3] = points[0];

                if (drawFaces)
                    g.FillPolygon(Brushes.Beige, points);
                if (drawEdges)
                    g.DrawLines(Pens.DarkRed, points);
            }
        }

        private void edgesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            edgesToolStripMenuItem.Checked = !edgesToolStripMenuItem.Checked;
            _pnlDraw.Invalidate();
        }

        private void facesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            facesToolStripMenuItem.Checked = !facesToolStripMenuItem.Checked;
            _pnlDraw.Invalidate();
        }

        private void generateSampleMeshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _generator = new MeshGenerator
            {
                //DistanceToZoom = new Dictionary<float, int> { { 0.5f, 1 }, { 2, 2 }, { 3, 4 }, { 1000f, 8 } },
                DistanceToZoom = new Dictionary<float, int> { { 1000f, 1 } },
                ZoomToColor = new Dictionary<int, Color> { { 1, Color.White }, { 2, Color.LightGreen }, { 4, Color.LightSkyBlue }, { 8, Color.PaleVioletRed } }
            };
            var center = new Point(12544 / TerrainPatch.DefaultSize, 17920 / TerrainPatch.DefaultSize);
            var rectangle = new Rectangle(center, new Size(1, 1));
            rectangle.Inflate(0,0);

            _generator.GenerateMesh(new List<Point> { center }, rectangle);
            var vertex_count = _generator.GenerateVertices();
            Console.WriteLine($"There are {vertex_count} vertices");

            _generator.GenerateFaces();
            Console.WriteLine($"There are {_generator.EnumerateFaces().Count()} faces");

            _generator.WritePlyFile(Terrain, "test-mesh.ply", true);

            UpdateVertexDrawOffset();

            _vertices = null;
            _pnlDraw.Invalidate();
        }

        void UpdateVertexDrawOffset()
        {
            if (_generator == null)
                return;
            var xmin = int.MaxValue;
            var ymin = int.MaxValue;
            foreach (var v in _generator.EnumerateVertices())
            {
                xmin = Math.Min(xmin, v.X);
                ymin = Math.Min(ymin, v.Y);
            }
            var transformed = TransformVertex(new Point(xmin, ymin));
            VertexDrawOffset = new PointF(50f - transformed.X, 50f - transformed.Y);
        }

        private void generateVariableSizedMeshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var center_point = new Point(12423, 17772);
            var center = new Point(center_point.X / TerrainPatch.DefaultSize, center_point.Y / TerrainPatch.DefaultSize);

            var bounds = new Rectangle(new Point(77, 130), new Size(47, 25));
            bounds.Inflate(4, 4);

            var g = new MeshGenerator { Terrain = Terrain };
            g.GenerateMesh(new List<Point> { center }, bounds);
            var vertex_count = g.GenerateVertices();
            Console.WriteLine($"There are {vertex_count} vertices");

            g.GenerateFaces();
            Console.WriteLine($"There are {g.EnumerateFaces().Count()} faces");

            var test = g.EveryFacePointsUp();
            Console.WriteLine($"Every face points up = {test}");

            g.WritePlyFile(Terrain, "variable-mesh.ply", false);
        }

        private void printBinaryFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog { InitialDirectory = @"c:\git\github\lunar_horizon\lunar_horizon\bin\x64\Debug", Filter = "ply files (*.ply)|*.ply|All files (*.*)|*.*" };
            if (dialog.ShowDialog() != DialogResult.OK)
                return;
            try
            {
                var stream = dialog.OpenFile();
                using (var br = new BinaryReader(stream))
                {
                    Expect(br, "ply");
                    Expect(br, "format binary_little_endian 1.0");
                    Expect(br, null);
                    Expect(br, null);
                    Expect(br, "element vertex 342393");
                    var vertex_count = 342393;

                    Expect(br, "property double x");
                    Expect(br, "property double y");
                    Expect(br, "property double z");

                    Expect(br, "element face 684344");
                    var face_count = 684344;

                    Expect(br, "property list uchar int vertex_index");
                    Expect(br, "end_header");

                    for (var i = 0; i < vertex_count; i++)
                    {
                        var x = br.ReadDouble();
                        var y = br.ReadDouble();
                        var z = br.ReadDouble();
                        //Console.WriteLine($"{i}: {x} {y} {z}");
                    }

                    for (var i = 0; i < face_count; i++)
                    {
                        var elts = br.ReadByte();
                        if (elts!=3)
                        {
                            Console.WriteLine($"Invalid vertex count at position {br.BaseStream.Position}");
                        }
                        for (var j = 0; j < elts; j++)
                        {
                            var idx = br.ReadInt32();
                            if (idx < 0 || idx >= vertex_count)
                            {
                                Console.WriteLine($"Invalid vertex index at position {br.BaseStream.Position}");
                            }
                        }
                    }

                    if (stream.Length != br.BaseStream.Position)
                    {
                        Console.WriteLine("Not at end of stream");
                    }
                }
            }
            catch (System.Runtime.InteropServices.ExternalException e1)
            {
                Console.WriteLine(e1);
            }
        }

        StringBuilder _sb = new StringBuilder();
        void Expect(BinaryReader sr, string line)
        {
            _sb.Clear();
            var c = sr.ReadChar();
            while (c != 13)
            {
                _sb.Append(c);
                c = sr.ReadChar();
            }
            c = sr.ReadChar();
            if (c!=10)
                throw new System.Runtime.InteropServices.ExternalException();
            if (line == null)
                return;
            var seen = _sb.ToString();
            if (!line.Equals(seen))
            {
                Console.WriteLine($"Expected: {line} seen: {seen}");
                throw new System.Runtime.InteropServices.ExternalException();
            }
        }

        private void centerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            centerToolStripMenuItem.Checked = !centerToolStripMenuItem.Checked;
            MeshPatch.DrawCenter = centerToolStripMenuItem.Checked;
            generateSampleMeshToolStripMenuItem_Click(sender, e);
            _pnlDraw.Invalidate();
        }

        private void diagonalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            diagonalToolStripMenuItem.Checked = !diagonalToolStripMenuItem.Checked;
            MeshPatch.DrawDiagonal = diagonalToolStripMenuItem.Checked;
            generateSampleMeshToolStripMenuItem_Click(sender, e);
            _pnlDraw.Invalidate();
        }

        private void right1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            right1ToolStripMenuItem.Checked = !right1ToolStripMenuItem.Checked;
            MeshPatch.DrawRight1 = right1ToolStripMenuItem.Checked;
            generateSampleMeshToolStripMenuItem_Click(sender, e);
            _pnlDraw.Invalidate();
        }

        private void right2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            right2ToolStripMenuItem.Checked = !right2ToolStripMenuItem.Checked;
            MeshPatch.DrawRight2 = right2ToolStripMenuItem.Checked;
            generateSampleMeshToolStripMenuItem_Click(sender, e);
            _pnlDraw.Invalidate();
        }

        private void right3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            right3ToolStripMenuItem.Checked = !right3ToolStripMenuItem.Checked;
            MeshPatch.DrawRight3 = right3ToolStripMenuItem.Checked;
            generateSampleMeshToolStripMenuItem_Click(sender, e);
            _pnlDraw.Invalidate();
        }

        private void down1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            down1ToolStripMenuItem.Checked = !down1ToolStripMenuItem.Checked;
            MeshPatch.DrawDown1 = down1ToolStripMenuItem.Checked;
            generateSampleMeshToolStripMenuItem_Click(sender, e);
            _pnlDraw.Invalidate();
        }

        private void down2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            down2ToolStripMenuItem.Checked = !down2ToolStripMenuItem.Checked;
            MeshPatch.DrawDown2 = down2ToolStripMenuItem.Checked;
            generateSampleMeshToolStripMenuItem_Click(sender, e);
            _pnlDraw.Invalidate();
        }

        private void down3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            down3ToolStripMenuItem.Checked = !down3ToolStripMenuItem.Checked;
            MeshPatch.DrawDown3 = down3ToolStripMenuItem.Checked;
            generateSampleMeshToolStripMenuItem_Click(sender, e);
            _pnlDraw.Invalidate();
        }
    }
}
