using lunar_horizon.patches;
using lunar_horizon.terrain;
using System;
using System.Drawing;

namespace lunar_horizon.tiles
{
    public class BugHunt
    {
        public static TerrainPatch To;
        public static TerrainPatch From;
        public InMemoryTerrainManager Terrain => TileTreeNode.Terrain;

        public void Test1()
        {
            To = TerrainPatch.FromId(new Point(103, 138));
            From = TerrainPatch.FromId(new Point(103, 139));

            To.FillPointsAndMatrices(Terrain);
            From.FillPoints(Terrain);
            To.UpdateHorizonBugHunt(From);
            Console.WriteLine(@"Test1 finished.");
        }

        public static bool EqualPoints(TerrainPatch a, TerrainPatch b)
        {
            for (var line = 0; line < TerrainPatch.DefaultSize; line++)
                for (var sample = 0; sample < TerrainPatch.DefaultSize; sample++)
                    if (!a.Points[line][sample].Equals(b.Points[line][sample]))
                        return false;
            return true;
        }

        public static bool EqualsMatrices(TerrainPatch a, TerrainPatch b)
        {
            for (var line = 0; line < TerrainPatch.DefaultSize; line++)
                for (var sample = 0; sample < TerrainPatch.DefaultSize; sample++)
                    if (!a.Matrices[line][sample].Equals(b.Matrices[line][sample]))
                        return false;
            return true;
        }
    }
}
