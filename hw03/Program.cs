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

            var files = Directory.GetFiles(args[0], "*.txt");

            foreach (var file in files)
            {
                var instances = new List<Instance>();
                using (var reader = new StreamReader(file))
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
                const int NUMBER_OF_RUNS = 1;
                // ReSharper disable once InconsistentNaming
                var bbm = 0;
                var dpm = 0;
                var dwm = 0;
                var pwm = 0;

                // Prepare Lists for best prices
                var bbPrices = new List<int>();
                var dynamicPricePrices = new List<int>();
                var dynamicWeightPrices = new List<int>();
                var priceWeightPrices = new List<int>();

                for (var i = 0; i < NUMBER_OF_RUNS; i++)
                {
                    // Branch and Bound algorithm
                    var watchBb = new Stopwatch();
                    watchBb.Start();
                    var tmp = 0;
                    foreach (var inst in instances)
                    {
                        continue;
                        var branchBound = new BbMethod(inst);
                        if (i == 0) bbPrices.Add(branchBound.BestPrice);
                        tmp += branchBound.Measure;
                    }

                    watchBb.Stop();
                    bbm = tmp / instances.Count;
                    //*******************************************************

                    // Dynamic Price algorithm
                    var watchDynamicPrice = new Stopwatch();
                    watchDynamicPrice.Start();
                    tmp = 0;
                    foreach (var inst in instances)
                    {
                        continue;
                        var dynamic = new DynamicPrice(inst);
                        if (i == 0) dynamicPricePrices.Add(dynamic.BestPrice);
                        tmp += dynamic.Measure;
                    }

                    watchDynamicPrice.Stop();
                    dpm = tmp / instances.Count;
                    //*******************************************************

                    // Dynamic Weight algorithm
                    var watchDynamicWeight = new Stopwatch();
                    watchDynamicWeight.Start();
                    tmp = 0;
                    foreach (var inst in instances)
                    {
                        var dynamic = new DynamicWeight(inst);
                        if (i == 0) dynamicWeightPrices.Add(dynamic.BestPrice);
                        tmp += dynamic.Measure;
                    }

                    watchDynamicWeight.Stop();
                    dwm = tmp / instances.Count;
                    //*******************************************************

                    // Add price weight ratio algorithm
                    tmp = 0;
                    foreach (var inst in instances)
                    {
                        var dynamic = new PriceWeightRatio(inst);
                        if (i == 0) priceWeightPrices.Add(dynamic.BestPrice);
                        tmp += dynamic.Measure;
                    }

                    pwm = tmp / instances.Count;
                }

                //*******************************************************
                // Check mistake (OPT - PRICE)/OPT * 100 = X%
                double maxMistake = 0;
                double avgMistake = 0;
                for (var i = 0; i < instances.Count; i++)
                {
                    var mistake = Math.Abs((double) (dynamicWeightPrices[i] - priceWeightPrices[i]) / (double) dynamicWeightPrices[i]);

                    avgMistake += mistake;
                    if (mistake > maxMistake) maxMistake = mistake;
                }

                //*******************************************************
                Console.WriteLine("\n" + file);
                Console.WriteLine("BB Measurement: " + bbm);
                Console.WriteLine("Dynamic Price Measurement: " + dpm);
                Console.WriteLine("Dynamic Weight Measurement: " + dwm);
                Console.WriteLine("Price Weight Ratio Measurement: " + pwm);
                Console.WriteLine("\t-avg mistake: " + Math.Round((double) (avgMistake / instances.Count) * 100, 4));
                Console.WriteLine("\t-max mistake: " + Math.Round(maxMistake * 100, 3));
            }
        }
    }
}