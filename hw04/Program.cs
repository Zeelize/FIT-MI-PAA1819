using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using paa_hw4.Algorithms;
using paa_hw4.Structs;

namespace paa_hw4
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

            var file = args[0];
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

            var dynamicWeightPrices = new List<int>();
            var saPrices = new List<int>();

            //*******************************************************
            // Dynamic Weight algorithm
            var watchDynamicWeight = new Stopwatch();
            watchDynamicWeight.Start();
            var tmp = 0;
            foreach (var inst in instances)
            {
                var dynamic = new DynamicWeight(inst);
                dynamicWeightPrices.Add(dynamic.BestPrice);
                tmp += dynamic.Measure;
            }

            watchDynamicWeight.Stop();
            var dwm = tmp / instances.Count;

            //*******************************************************
            // TODO Simulated annealing
            var watchSa = new Stopwatch();
            watchSa.Start();
            // zavislost relativni odhcylky na koeficientu ochlazovani
            foreach (var inst in instances)
            {
                var sa = new SimulatedAnnealing(inst, 1000, 10, 0.95, 100);
                var price = sa.SaSolver();
                Console.WriteLine("FINAL: " + price);
                
                /*for (var i = 0.1; i <= 1; i += 0.05)
                {
                    var sa = new SimulatedAnnealing(inst, 1000, 10, i, 100);
                    var price = sa.SaSolver();
                    //saPrices.Add(price);
                    Console.WriteLine(i + ";" + price);
                }*/
            }

            watchSa.Stop();

            var saTime = watchSa.Elapsed.TotalMilliseconds;

            //*******************************************************
            // Check mistake (OPT - PRICE)/OPT * 100 = X%
            /*double maxMistake = 0;
            double avgMistake = 0;
            for (var i = 0; i < instances.Count; i++)
            {
                var mistake = Math.Abs((double) (dynamicWeightPrices[i] - saPrices[i]) /
                                       (double) dynamicWeightPrices[i]);

                avgMistake += mistake;
                if (mistake > maxMistake) maxMistake = mistake;
            }*/

            //*******************************************************
            //Console.WriteLine("\n" + file);
            //Console.WriteLine("Dynamic Weight Measurement: " + dwm);
            //Console.WriteLine("SA Measurement: " + saTime);
            //Console.WriteLine("\t-avg mistake: " + Math.Round((double) (avgMistake / instances.Count) * 100, 4));
            //Console.WriteLine("\t-max mistake: " + Math.Round(maxMistake * 100, 4));
        }
    }
}