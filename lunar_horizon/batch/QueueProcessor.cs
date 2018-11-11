using lunar_horizon.patches;
using lunar_horizon.terrain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace lunar_horizon.batch
{
    public class QueueProcessor
    {
        public const float ObserverHeight = 4f / 1000f;  // 1 meter

        public string ShadowCalculationQueueFilename = @"shadow_calculation_queue.json";
        public int MaxDegreeOfParallelism = 6;
        public int MaxSpread = 177; // 70;
        public bool FilterTentpoles = true;
        public bool NearHorizonsOnly = false;
        public views.MapView MapView;

        public InMemoryTerrainManager Terrain;

        protected string _ShadowCalculationQueuePath;
        public string ShadowCalculationQueuePath
        {
            get
            {
                if (_ShadowCalculationQueuePath==null)
                    _ShadowCalculationQueuePath = Path.Combine(LunarHorizon.MapRoot, ShadowCalculationQueueFilename);
                return _ShadowCalculationQueuePath;
            }
            set
            {
                _ShadowCalculationQueuePath = value;
            }
        }

        public virtual void RunQueue(List<TerrainPatch> queue, Action<List<TerrainPatch>> queue_saver = null, int spread = -1, bool overHorizonCheck = true, bool centerOnly = false, bool center = true, bool unloadHorizons = true, bool writeHorizons = true)
        {
            LunarHorizon.Singleton?.SetStatus(true);
            Debug.Assert(Terrain != null);
            centerOnly |= NearHorizonsOnly;
            if (queue == null)
                queue = ReadShadowCalculationQueue();
            if (spread < 0) spread = MaxSpread;
            while (queue.Count > 0)
            {
                var patch = queue[0];
                Console.WriteLine($"Starting [{patch.Line},{patch.Sample}] ...");
                try
                {
                    var stopwatch = new Stopwatch();
                    var far_field = new List<TerrainPatch>();
                    if (MapView != null)
                        MapView.ProcessingPatches = new List<TerrainPatch> { patch };
                    stopwatch.Start();
                    patch.FillPointsAndMatrices(Terrain);
                    if (center)
                        patch.ParallelAddNearHorizon(Terrain);
                    Console.WriteLine($"  near horizon calculated or loaded in {stopwatch.Elapsed}");

                    if (true)
                    {
                        var options = new ParallelOptions { MaxDegreeOfParallelism = MaxDegreeOfParallelism };
                        for (var i = 1; i < spread; i++)
                        {
                            var other1 = patch.SurroundingPatches(i).ToList();
                            Parallel.ForEach(other1, options, o => o.FillPoints(Terrain));
                            var other2 = overHorizonCheck ? other1.Where(patch.IsOverHorizon).ToList() : other1;
                            if (MapView != null)
                            {
                                far_field.AddRange(other2);
                                MapView.FarPatches = far_field;
                                MapView.Invalidate();
                            }
                            Parallel.ForEach(other2, options, o => patch.UpdateHorizon(o));
                        }
                    }
                    if (writeHorizons)
                        patch.Write();
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
                Console.WriteLine(@"Finished queue with CPU.");
                LunarHorizon.Singleton?.SetStatus(false);
            }
        }

        public void RunQueue()
        {
            RunQueue(ReadShadowCalculationQueue(), q => WriteShadowCalculationQueue(q));
        }

        #region Helper Functions

        public List<TerrainPatch> ReadShadowCalculationQueue() => ReadShadowCalculationQueue(ShadowCalculationQueuePath);

        public static List<TerrainPatch> ReadShadowCalculationQueue(string path)
        {
            if (!File.Exists(path)) return new List<TerrainPatch>();
            using (var sr = new StreamReader(path))
            using (var jr = new JsonTextReader(sr))
            {
                var serializer = new JsonSerializer();
                return serializer.Deserialize<List<TerrainPatch>>(jr);
            }
        }

        public void WriteShadowCalculationQueue(List<TerrainPatch> q) => WriteShadowCalculationQueue(q, ShadowCalculationQueuePath);

        public static void WriteShadowCalculationQueue(List<TerrainPatch> q, string path)
        {
            using (var sr = new StreamWriter(path))
            using (var jr = new JsonTextWriter(sr))
            {
                var serializer = new JsonSerializer();
                serializer.Serialize(jr, q);
            }
        }

        #endregion

        #region Extend

        public void ExtendExistingPatches()
        {
            if (MaxSpread != 177)
                throw new Exception($"Unexpected value of MaxSpread={MaxSpread}");
            var filenames = (new DirectoryInfo(LunarHorizon.HorizonsRoot)).EnumerateFiles("*.bin").Select(fi => fi.FullName).ToList();
            foreach (var filename in filenames)
            {
                var patch = TerrainPatch.ReadFrom(filename);
                var stopwatch = new Stopwatch();
                stopwatch.Start();
                Console.WriteLine($"Starting [{patch.Line},{patch.Sample}] ...");
                patch.FillPointsAndMatrices(Terrain);
                try
                {
                    var far_field = new List<TerrainPatch>();
                    if (MapView != null)
                        MapView.ProcessingPatches = new List<TerrainPatch> { patch };
                    var options = new ParallelOptions { MaxDegreeOfParallelism = MaxDegreeOfParallelism };
                    for (var i = 1; i < MaxSpread; i++)
                    {
                        var other1 = patch.SurroundingPatches(i).Where(p => !patch.ShadowCasters.Contains(p.Id)).ToList();
                        Parallel.ForEach(other1, options, o => o.FillPoints(Terrain));
                        var other2 = other1.Where(patch.IsOverHorizon).ToList();
                        if (MapView != null)
                        {
                            far_field.AddRange(other2);
                            MapView.FarPatches = far_field;
                            MapView.Invalidate();
                        }
                        Parallel.ForEach(other2, options, o => patch.UpdateHorizon(o));
                    }
                    patch.Write();
                    patch.InitializeHorizons();  // Unload the horizon data (100MB)
                    stopwatch.Stop();
                    var seconds_per_patch = far_field.Count == 0 ? 0f : (stopwatch.ElapsedMilliseconds / 1000f) / far_field.Count;
                    Console.WriteLine($"  Finished [{patch.Line},{patch.Sample}] time={stopwatch.Elapsed}.  sec/patch={seconds_per_patch}");
                }
                catch (Exception e1)
                {
                    Console.WriteLine(e1);
                    Console.WriteLine(e1.StackTrace);
                }
            }
        }

        #endregion
    }
}
