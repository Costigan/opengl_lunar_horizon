using lunar_horizon.patches;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Drawing;

namespace lunar_horizon.gpu
{
    /// <summary>
    /// CombinedProcessor uses the GPU for far shadowcasters and the cpu for the near surface.  This bypasses a bug
    /// currently in the GPUProcessor implementation and should be removed when that bug has been fixed
    /// </summary>
    public class CombinedProcessor : GPUProcessor
    {
        public override void RunQueue(List<TerrainPatch> queue, Action<List<TerrainPatch>> queue_saver = null, int spread = -1, bool overHorizonCheck = true, bool centerOnly = false, bool center = true, bool unloadHorizons = true, bool writeHorizons = true)
        {
            LunarHorizon.Singleton?.SetStatus(true);
            Debug.Assert(Terrain != null);
            centerOnly |= NearHorizonsOnly;
            if (queue == null)
                queue = ReadShadowCalculationQueue();
            if (spread < 0) spread = MaxSpread;

            var patchDictionary = new Dictionary<Point, TerrainPatch>();
            BoundingBox = null;

            while (queue.Count > 0)
            {
                var patch = queue[0];
                Console.WriteLine($"Starting [{patch.Line},{patch.Sample}] ...");
                try
                {
                    var stopwatch = new Stopwatch();
                    stopwatch.Start();

                    var far_field = new List<TerrainPatch>();
                    if (MapView != null)
                        MapView.ProcessingPatches = new List<TerrainPatch> { patch };

                    // GPU version.  Horizons will be slope, not angles
                    //if (center)
                    //    AddNearHorizon(patch);

                    // CPU version
                    if (center)
                        patch.ParallelAddNearHorizon(Terrain);

                    Console.WriteLine($"  near horizon calculated or loaded in {stopwatch.Elapsed}");

                    patch.Matrices = null;      // Force the matrices to be regenerated so they're not left over from the near horizon
                    patch.FillPoints(Terrain);  // Should be a no op
                    patch.FillMatricesRelativeToPoint(Terrain, patch.Points[0][0]);

                    if (true)
                    {
                        var options = new ParallelOptions { MaxDegreeOfParallelism = MaxDegreeOfParallelism };
                        for (var i = 1; i < spread; i++)
                        {
                            // Fetch the surrounding patches from a cache
                            var other1 = patch.SurroundingIds(i).Select(id =>
                            {
                                if (patchDictionary.TryGetValue(id, out TerrainPatch r))
                                    return r;
                                r = TerrainPatch.FromId(id);
                                patchDictionary.Add(id, r);
                                return r;
                            }).ToList();
                            Parallel.ForEach(other1, options, o => o.FillPoints(Terrain));
                            var other2 = overHorizonCheck ? other1.Where(o => patch.IsOverHorizon(o, patch.Points[0][0])).ToList() : other1;
                            if (MapView != null)
                            {
                                far_field.AddRange(other2);
                                MapView.FarPatches = far_field;
                                MapView.Invalidate();
                            }

                            foreach (var p in other2)
                            {
                                var r = new Rectangle(p.Id, new Size(1, 1));
                                if (BoundingBox.HasValue)
                                    BoundingBox = Rectangle.Union(BoundingBox.Value, r);
                                else
                                    BoundingBox = r;
                            }

                            if (patch.Horizons == null)
                                patch.InitializeHorizons();

                            // Old code - Parallel.ForEach(other2, options, o => patch.UpdateHorizon(o));
                            // Here is the GPU Call
                            UpdateHorizons(patch, other2);
                        }
                    }
                    if (writeHorizons)
                        patch.Write();               // This converts the horizon format to angles
                    if (unloadHorizons)
                        patch.InitializeHorizons();  // Unload the horizon data (100MB)
                    stopwatch.Stop();
                    var seconds_per_patch = far_field.Count == 0 ? 0f : (stopwatch.ElapsedMilliseconds / 1000f) / far_field.Count;
                    Console.WriteLine($"  Finished [{patch.Line},{patch.Sample}] time={stopwatch.Elapsed}.  sec/patch={seconds_per_patch}");

                    // Update queue on disk
                    queue.RemoveAt(0);
                    queue_saver?.Invoke(queue);
                }
                catch (Exception e1)
                {
                    Console.WriteLine(e1);
                    Console.WriteLine(e1.StackTrace);
                }
                Console.WriteLine(@"Finished queue with GPU.");
                LunarHorizon.Singleton?.SetStatus(false);
            }
        }
    }
}
