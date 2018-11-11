using lunar_horizon.batch;
using lunar_horizon.terrain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace lunar_horizon
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                if (true)
                    Application.Run(new LunarHorizon());
                else
                    Application.Run(new viz.MainView());
                return;
            }

            RunQueue(args);
        }

        private static void RunQueue(string[] args)
        {
            bool runTest = false;
            var dem_filename = InMemoryTerrainManager.NorthDEM;
            var processor = new QueueProcessor();
            var stack = new Stack<string>(args.Reverse());
            while (stack.Count>0)
            {
                var arg = stack.Pop();
                switch (arg)
                {
                    case "--map-root":
                        LunarHorizon.MapRoot = stack.Pop();
                        break;
                    case "--horizons-root":
                        LunarHorizon.HorizonsRoot = stack.Pop();
                        break;
                    case "--dem":
                        dem_filename = stack.Pop();
                        InMemoryTerrainManager.NorthDEM = dem_filename;
                        break;
                    case "--queue":
                        processor.ShadowCalculationQueuePath = stack.Pop();
                        break;
                    case "--spread":
                        {
                            var spread = stack.Pop();
                            if (!int.TryParse(spread, out processor.MaxSpread))
                            {
                                Console.WriteLine($"Can't parse argument for --spread: {spread}");
                                return;
                            }
                            break;
                        }
                    case "--parallel":
                        {
                            var parallel = stack.Pop();
                            if (!int.TryParse(parallel, out processor.MaxDegreeOfParallelism))
                            {
                                Console.WriteLine($"Can't parse argument for --parallel: {parallel}");
                                return;
                            }
                            break;
                        }
                    case "--near-only":
                        processor.NearHorizonsOnly = true;
                        break;
                    case "--help":
                        Console.WriteLine(@"Lunar Horizon Calculator.  Usage:");
                        Console.WriteLine(@"usage: lunar_horizon --map-root <dir1> --horizons-root  <dir2> --dem <filename1> --queue <filename2> --spread <int> --parallel <int>");
                        Console.WriteLine(@"Where:");
                        Console.WriteLine(@"  --horizons-root contains horizon files, one file per 128 x 128 pixel square of the DEM (~100 MB per file)");
                        Console.WriteLine(@"  --dem is the LOLA DEM file.  We're using the LOLA 20 meter polar products");
                        Console.WriteLine(@"  --queue is a json file giving a sequence of 128 x 128 pixel squares to generate horizons for");
                        Console.WriteLine(@"  --spread is the number of squares away from the center square to use when generating the horizons");
                        Console.WriteLine(@"    This defaults to 67, which corresponds to a distance of ~170 km, beyond which no lunar feature is tall enough to generate shadows");
                        Console.WriteLine(@"  --parallel the maximum number of CPU cores to use");
                        return;
                    case "--test":
                        runTest = true;
                        break;
                }
            }

            Console.WriteLine($"Starting to service the queue");
            Console.WriteLine($"map-root      = {LunarHorizon.MapRoot}");
            Console.WriteLine($"horizons-root = {LunarHorizon.HorizonsRoot}");
            Console.WriteLine($"DEM           = {dem_filename}");
            Console.WriteLine($"queue         = {processor.ShadowCalculationQueuePath}");
            Console.WriteLine($"spread        = {processor.MaxSpread}");
            Console.WriteLine($"parallelism   = {processor.MaxDegreeOfParallelism}");
            Console.WriteLine(@"Starting.");
            processor.Terrain = (InMemoryTerrainManager)new InMemoryTerrainManager().LoadNorth();

            if (runTest)
            {
                LunarHorizon.RunVerboseComparisonToolStripMenuItem_Click(processor.Terrain);
                return;
            }

            var stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();
            processor.RunQueue();
            stopwatch.Stop();
            Console.WriteLine($"Total time {stopwatch.Elapsed}");
            Console.WriteLine(@"Exiting");
        }
    }
}