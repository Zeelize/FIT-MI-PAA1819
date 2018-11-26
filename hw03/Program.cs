using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using paa_hw3.Algorithms;
using paa_hw3.Structs;

namespace paa_hw3
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
            // ReSharper disable once InconsistentNaming
            const double EPSILON = 0.9;
            var allBbTimes = 0.0;
            var allDynamicPriceTimes = 0.0;
            var allDynamicWeightTimes = 0.0;
            
            // Prepare Lists for best prices
            var bbPrices = new List<int>();
            var dynamicPricePrices = new List<int>();
            var dynamicWeightPrices = new List<int>();
            
            //TODO FOR ALL ALGORITHM ADD MEASURE PARAMETER FOR COMPARISION
            for (var i = 0; i < NUMBER_OF_RUNS; i++)
            {
                // Branch and Bound algorithm
                var watchBb = new Stopwatch();
                watchBb.Start();
                foreach (var inst in instances)
                {
                    continue;
                    var branchBound = new BbMethod(inst);
                    if (i == 0) bbPrices.Add(branchBound.BestPrice);
                }
                watchBb.Stop();
                allBbTimes += watchBb.Elapsed.TotalMilliseconds;
                //*******************************************************
                
                // Dynamic Price algorithm
                var watchDynamicPrice = new Stopwatch();
                watchDynamicPrice.Start();
                foreach (var inst in instances)
                {
                    continue;
                    var dynamic = new DynamicPrice(inst);
                    if (i == 0) dynamicPricePrices.Add(dynamic.BestPrice);
                }
                watchDynamicPrice.Stop();
                allDynamicPriceTimes += watchDynamicPrice.Elapsed.TotalMilliseconds;
                //*******************************************************
                
                // Dynamic Weight algorithm
                var watchDynamicWeight = new Stopwatch();
                watchDynamicWeight.Start();
                foreach (var inst in instances)
                {
                    var dynamic = new DynamicWeight(inst);
                    if (i == 0) dynamicWeightPrices.Add(dynamic.BestPrice);
                }
                watchDynamicWeight.Stop();
                allDynamicWeightTimes += watchDynamicWeight.Elapsed.TotalMilliseconds;
                //*******************************************************

                // TODO Add price weight ratio algorithm
                
            }
            
            //*******************************************************
            // Check mistake (OPT - PRICE)/OPT * 100 = X%
            double maxMistake = 0;
            double avgMistake = 0;
            for (var i = 0; i < instances.Count; i++)
            {
                // TODO Measure mistake with price-weight ratio 
                //var mistake = Math.Abs((double) (dynamicWeightPrices[i] - fptasPrices[i]) / (double) dynamicWeightPrices[i]);
                //Console.WriteLine(instances[i].Id + " " + dynamicWeightPrices[i] + " " + fptasPrices[i] + " " + mistake);
                
                //avgMistake += mistake;
                //if (mistake > maxMistake) maxMistake = mistake;
            }
            
            //*******************************************************
            Console.WriteLine("\n" + args[0]);
            Console.WriteLine("Number of runs: " + NUMBER_OF_RUNS);
            Console.WriteLine("BB Avg Time: " + Math.Round(allBbTimes / NUMBER_OF_RUNS, 4));
            Console.WriteLine("Dynamic Price Avg Time: " + Math.Round(allDynamicPriceTimes / NUMBER_OF_RUNS, 4));
            Console.WriteLine("Dynamic Weight Avg Time: " + Math.Round(allDynamicWeightTimes / NUMBER_OF_RUNS, 4));
            Console.WriteLine("\t-epsilon: " + EPSILON * 100  + "%");
            Console.WriteLine("\t-avg mistake: " + Math.Round((double) (avgMistake / instances.Count) * 100, 3));
            Console.WriteLine("\t-max mistake: " + Math.Round(maxMistake * 100, 3));
        }
    }
}