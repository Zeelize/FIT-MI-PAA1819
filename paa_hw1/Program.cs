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
            
            // Prepare Lists for best prices
            var bfPrices = new List<int>();
            var pwPrices = new List<int>();
            
            // Run every instance for both types of solution
            //var p = Process.GetCurrentProcess();
            var watchBf = new Stopwatch();
            watchBf.Start();
            //var startUserProcessorTm = p.UserProcessorTime.TotalMilliseconds;
            foreach (var inst in instances)
            {
                // BruteForce algorithm
                var bruteForce = new BruteForce(inst);
                bfPrices.Add(bruteForce.BestPrice);
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
                pwPrices.Add(priceWeight.BestPrice);
                /*Console.Write(inst.Id + " " + inst.NumberOfItems + " PW: ");
                if (!priceWeight.Found) Console.Write("Does not have solution!");
                else Console.Write(priceWeight.BestPrice + "\n");*/
            }
            //endUserProcessorTm = p2.UserProcessorTime.TotalMilliseconds;
            watchPw.Stop();
            //Console.WriteLine(endUserProcessorTm - startUserProcessorTm);
            //Console.WriteLine("PW: " + watchPw.Elapsed.TotalMilliseconds);
            var pwTime = watchPw.Elapsed.TotalMilliseconds;
            
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
            Console.WriteLine("BF Time: " + bfTime);
            Console.WriteLine("PW Time: " + pwTime);
            Console.WriteLine("Max Mistake: " + maxMistake);
            Console.WriteLine("Avg Mistake: " + (double)(avgMistake / instances.Count));
            // TODO Mistake to with percentage
        }
    }
}
