using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSimplexTest
{
    class Test
    {
        public List<Performance> performance = new List<Performance>();

        private LibNoise.Primitive.ImprovedPerlin noiseLibrary;
        private Process proc;
        private long seed;
        private double size;
        private Stopwatch timer = new Stopwatch();
        public LibNoise.Builder.NoiseMapBuilderPlane test;
        public LibNoise.Builder.NoiseMap testMap;

        /// <summary>
        /// Generate a cube given a size
        /// </summary>
        /// <param name="size">The size of the cube</param>
        /// <param name="seed">The seed to use, if needed. Can only be set once per object because lazy</param>
        public Test(double size, long seed = 21590421L)
        {
            timer.Start();
            PerformanceMonitor("At start");
            this.size = size;
            this.seed = seed;
            if (noiseLibrary == null)
            {
                //noiseLibrary = new OpenSimplexNoise(seed);
                noiseLibrary = new LibNoise.Primitive.ImprovedPerlin((int)seed, LibNoise.NoiseQuality.Fast);
            }
        }

        public List<Performance> Run(int dimensions = 2)
        {
            int percentOfSize = (dimensions == 2)
                ? (int)Math.Pow(size, dimensions) / 10
                : (int)Math.Pow(size, dimensions) / 100;

            int loopNum = 0;
            int numberOfLoops = 0;
            for (int x = 0; x < size; x++)
            {
                //Generate performance data every x%
                if (loopNum != 0 && percentOfSize == loopNum)
                {
                    numberOfLoops++;
                    PerformanceMonitor("Inside", numberOfLoops * ((dimensions == 2) ? 10 : 1));
                    loopNum = 0;
                }
                for (int y = 0; y < size; y++)
                {
                    if (dimensions == 3)
                    {
                        for (int z = 0; z < size; z++)
                        {
                            //Look up, don't store or display!
                            //noiseLibrary.Evaluate(x, y, z);
                            noiseLibrary.GetValue(x, y, z);
                            loopNum++;
                        }
                    }
                    else
                    {
                        //noiseLibrary.Evaluate(x, y);
                        noiseLibrary.GetValue(x, y);
                        loopNum++;
                    }
                }
            }

            PerformanceMonitor("End", 100);

            timer.Stop();
            return performance;
        }

        public void TestBuilder()
        {
            PerformanceMonitor("Start");
            test = new LibNoise.Builder.NoiseMapBuilderPlane();
            test.SetBounds(-512, 512, -512, 512);
            test.SetSize(1024, 1024);
            testMap = new LibNoise.Builder.NoiseMap();
            test.NoiseMap = testMap;
            LibNoise.Primitive.SimplexPerlin testNoise = new LibNoise.Primitive.SimplexPerlin();
            test.SourceModule = testNoise;
            test.Build();
            PerformanceMonitor("End");
        }

        private void PerformanceMonitor(string Name, int progress = 0)
        {
            proc = Process.GetCurrentProcess();
            Performance pItem = new Performance();
            pItem.Name = "At ";
            pItem.Name += (progress != 0) ? progress + "%" : "Start";
            pItem.memory = proc.PrivateMemorySize64;
            pItem.elapsedInMS = timer.ElapsedMilliseconds;
            performance.Add(pItem);
            Console.WriteLine(pItem.Name + " - Memory: " + pItem.Memory + "kB - Time Elapsed: " + pItem.elapsedInMS / 1000 + " seconds");
        }
    }
}
