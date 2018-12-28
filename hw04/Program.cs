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
            
            //*******************************************************
            // Dynamic Weight algorithm
            foreach (var inst in instances)
            {
                var dynamic = new DynamicWeight(inst);
                dynamicWeightPrices.Add(dynamic.BestPrice);
            }

            //*******************************************************
            // Cooling coefficient
            Console.WriteLine("*********** COOLING COEFF *****************");
            for (var i = 0.5; i <= 1; i += 0.05)
            {
                double avgMistake = 0;
                double avgTime = 0;
                for (var j = 0; j < instances.Count; j++)
                {
                    var watchSa = new Stopwatch();
                    watchSa.Start();
                    
                    var sa = new SimulatedAnnealing(instances[j], 1000, 10, i, 50);
                    var price = sa.SaSolver();
                    
                    watchSa.Stop();
                    
                    avgTime += watchSa.Elapsed.TotalMilliseconds;
                    
                    var mistake = Math.Abs((double) (dynamicWeightPrices[j] - price) /
                                           (double) dynamicWeightPrices[j]);
                    avgMistake += mistake;
                }
                
                
                var timeFinal = Math.Round((double) (avgTime / instances.Count), 4);
                var avgFinal = Math.Round((double) (avgMistake / instances.Count) * 100, 4);
                
                // Count Mistake and time
                Console.WriteLine("CC: " + i);
                Console.WriteLine("Mistake: " + avgFinal);
                Console.WriteLine("Time: " + timeFinal);
                Console.WriteLine();
            }
            
            // Number of steps
            Console.WriteLine("*********** STEPS *****************");
            for (var i = 50; i <= 500; i += 50)
            {
                double avgMistake = 0;
                double avgTime = 0;
                for (var j = 0; j < instances.Count; j++)
                {
                    var watchSa = new Stopwatch();
                    watchSa.Start();
                    
                    var sa = new SimulatedAnnealing(instances[j], 1000, 10, 0.9, i);
                    var price = sa.SaSolver();
                    
                    watchSa.Stop();
                    
                    avgTime += watchSa.Elapsed.TotalMilliseconds;
                    
                    var mistake = Math.Abs((double) (dynamicWeightPrices[j] - price) /
                                           (double) dynamicWeightPrices[j]);
                    avgMistake += mistake;
                }
                
                
                var timeFinal = Math.Round((double) (avgTime / instances.Count), 4);
                var avgFinal = Math.Round((double) (avgMistake / instances.Count) * 100, 4);
                
                // Count Mistake and time
                Console.WriteLine("Steps: " + i);
                Console.WriteLine("Mistake: " + avgFinal);
                Console.WriteLine("Time: " + timeFinal);
                Console.WriteLine();
            }
                
            // Init temp
            Console.WriteLine("*********** INIT TEMP *****************");
            for (var i = 100; i <= 2000; i += 300)
            {
                double avgMistake = 0;
                double avgTime = 0;
                for (var j = 0; j < instances.Count; j++)
                {
                    var watchSa = new Stopwatch();
                    watchSa.Start();
                    
                    var sa = new SimulatedAnnealing(instances[j], i, 10, 0.9, 50);
                    var price = sa.SaSolver();
                    
                    watchSa.Stop();
                    
                    avgTime += watchSa.Elapsed.TotalMilliseconds;
                    
                    var mistake = Math.Abs((double) (dynamicWeightPrices[j] - price) /
                                           (double) dynamicWeightPrices[j]);
                    avgMistake += mistake;
                }
                
                
                var timeFinal = Math.Round((double) (avgTime / instances.Count), 4);
                var avgFinal = Math.Round((double) (avgMistake / instances.Count) * 100, 4);
                
                // Count Mistake and time
                Console.WriteLine("Init temp: " + i);
                Console.WriteLine("Mistake: " + avgFinal);
                Console.WriteLine("Time: " + timeFinal);
                Console.WriteLine();
            }
           

            

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
            //Console.WriteLine("SA Measurement: " + saTime);
            //Console.WriteLine("\t-avg mistake: " + Math.Round((double) (avgMistake / instances.Count) * 100, 4));
            //Console.WriteLine("\t-max mistake: " + Math.Round(maxMistake * 100, 4));
        }
    }
}