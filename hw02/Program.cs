using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using paa_hw2.Algorithms;
using paa_hw2.Structs;

namespace paa_hw2
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            // Load file with instances
            if (args.Length != 1)
            {
                Console.WriteLine("Wrong number of arguments! Second argument has to be file with instances!");
                return;
            }

            var instances = new List<Instance>();
            using (var reader = new StreamReader(args[0]))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var split = line.Split(' ');
                    // Parse line into instance
                    var id = int.Parse(split[0]);
                    var n = int.Parse(split[1]);
                    var cap = int.Parse(split[2]);

                    var index = 3;
                    var items = new List<Tuple<int, int>>();
                    for (var i = 0; i < n; i++)
                    {
                        items.Add(Tuple.Create(int.Parse(split[index]), int.Parse(split[index + 1])));
                        index += 2;
                    }

                    // Create instance
                    instances.Add(new Instance(id, n, cap, items));
                }
            }

            // ReSharper disable once InconsistentNaming
            const int NUMBER_OF_RUNS = 200;
            var allBfTimes = 0.0;
            var allBbTimes = 0.0;

            // Prepare Lists for best prices
            var bfPrices = new List<int>();
            var pwPrices = new List<int>();

            for (var i = 0; i < NUMBER_OF_RUNS; i++)
            {
                Console.WriteLine("RUN: " + i);
                
                // BruteForce algorithm
                var watchBf = new Stopwatch();
                watchBf.Start();
                foreach (var inst in instances)
                {
                    continue;
                    var bruteForce = new BruteForce(inst);
                    if (i == 0) bfPrices.Add(bruteForce.BestPrice);
                }
                watchBf.Stop();
                var bfTime = watchBf.Elapsed.TotalMilliseconds;
                allBfTimes += bfTime;
                //*******************************************************

                // Branch and Bound algorithm
                var watchBb = new Stopwatch();
                watchBb.Start();
                foreach (var inst in instances)
                {
                    var branchBound = new BbMethod(inst);
                    if (i == 0) pwPrices.Add(branchBound.BestPrice);
                }
                watchBb.Stop();
                var bbTime = watchBb.Elapsed.TotalMilliseconds;
                allBbTimes += bbTime;
                //*******************************************************
                
                // TODO Dynamic algorithm
                
                //*******************************************************
                
                // TODO FPTAS algorithm
            }

            Console.WriteLine("\nNumber of runs: " + NUMBER_OF_RUNS);
            //Console.WriteLine("BF Avg Time: " + allBfTimes / NUMBER_OF_RUNS);
            Console.WriteLine("BB Avg Time: " + allBbTimes / NUMBER_OF_RUNS);
        }
    }
}