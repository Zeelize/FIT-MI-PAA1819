using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace paa_hw1
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
            const int NUMBER_OF_RUNS = 2000;
            var allBfTimes = 0.0;
            var allPwTimes = 0.0;
            
            // Prepare Lists for best prices
            var bfPrices = new List<int>();
            var pwPrices = new List<int>();

            for (var i = 0; i < NUMBER_OF_RUNS; i++)
            {
                Console.WriteLine(i);
            // Run every instance for both types of solution
            //var p = Process.GetCurrentProcess();
            var watchBf = new Stopwatch();
            watchBf.Start();
            //var startUserProcessorTm = p.UserProcessorTime.TotalMilliseconds;
            foreach (var inst in instances)
            {
                // BruteForce algorithm
                var bruteForce = new BruteForce(inst);
                if (i == 0) bfPrices.Add(bruteForce.BestPrice);
                /*Console.Write(inst.Id + " " + inst.NumberOfItems + " BF: ");
                if (!bruteForce.Found) Console.Write("Does not have solution!");
                else
                {
                    Console.Write(bruteForce.BestPrice + "\n");
                    foreach (var item in bruteForce.BestItems)
                    {
                        Console.Write(item + " ");
                    }
                    Console.Write("\n");
                }*/
            }
            //var endUserProcessorTm = p.UserProcessorTime.TotalMilliseconds;
            watchBf.Stop();
            //Console.WriteLine(endUserProcessorTm - startUserProcessorTm);
            //Console.WriteLine("BF: " + watchBf.Elapsed.TotalMilliseconds);
            var bfTime = watchBf.Elapsed.TotalMilliseconds;
                allBfTimes += bfTime;
            
            // Price-Weight algorithm
            //Console.WriteLine("\nSolve with price-weight distribution!");
            //var p2 = Process.GetCurrentProcess();
            var watchPw = new Stopwatch();
            watchPw.Start();
            //startUserProcessorTm = p2.UserProcessorTime.TotalMilliseconds;
            foreach (var inst in instances)
            {
                // PriceWeight algorithm
                var priceWeight = new PriceWeightRatio(inst);
                if (i == 0)  pwPrices.Add(priceWeight.BestPrice);
                /*Console.Write(inst.Id + " " + inst.NumberOfItems + " PW: ");
                if (!priceWeight.Found) Console.Write("Does not have solution!");
                else Console.Write(priceWeight.BestPrice + "\n");*/
            }
            //endUserProcessorTm = p2.UserProcessorTime.TotalMilliseconds;
            watchPw.Stop();
            //Console.WriteLine(endUserProcessorTm - startUserProcessorTm);
            //Console.WriteLine("PW: " + watchPw.Elapsed.TotalMilliseconds);
            var pwTime = watchPw.Elapsed.TotalMilliseconds;
                allPwTimes += pwTime;

            }
            
            // Output
            if (instances.Count != bfPrices.Count || instances.Count != pwPrices.Count ||
                pwPrices.Count != bfPrices.Count) return;

            double maxMistake = 0;
            double avgMistake = 0;
            for (var i = 0; i < instances.Count; i++)
            {
                var mistake = (double)(bfPrices[i] - pwPrices[i]) / (double)bfPrices[i];
                Console.WriteLine(instances[i].Id + " " + bfPrices[i] + " " + pwPrices[i] + " " + mistake);

                avgMistake += mistake;
                if (mistake > maxMistake) maxMistake = mistake;
            }
            
            Console.WriteLine("\nNumber of runs: " + NUMBER_OF_RUNS);
            Console.WriteLine("BF Avg Time: " + allBfTimes / NUMBER_OF_RUNS);
            Console.WriteLine("PW Avg Time: " + allPwTimes / NUMBER_OF_RUNS);
            Console.WriteLine("Max Mistake: " + Math.Round(maxMistake * 100, 3));
            Console.WriteLine("Avg Mistake: " + Math.Round((double)(avgMistake / instances.Count) * 100, 3));
        }
    }
}
