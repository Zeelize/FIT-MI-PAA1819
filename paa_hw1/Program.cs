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
            
            // Run every instance for both types of solution
            var p = Process.GetCurrentProcess();
            //var watch = new Stopwatch();
            //watch.Start();
            var startUserProcessorTm = p.UserProcessorTime.TotalMilliseconds;
            foreach (var inst in instances)
            {
                // BruteForce algorithm
                Console.Write(inst.Id + " " + inst.NumberOfItems + " BF: ");
                var bruteForce = new BruteForce(inst);
                if (!bruteForce.Found) Console.Write("Does not have solution!");
                else
                {
                    Console.Write(bruteForce.BestPrice + " ");
                    foreach (var item in bruteForce.BestItems)
                    {
                        Console.Write(item + " ");
                    }
                    Console.Write("\n");
                }
            }
            var endUserProcessorTm = p.UserProcessorTime.TotalMilliseconds;
            //watch.Stop();
            Console.WriteLine(endUserProcessorTm - startUserProcessorTm);
            //Console.WriteLine(watch.Elapsed.TotalMilliseconds);
            
            // Price-Weight algorithm
            //Console.WriteLine("\nSolve with price-weight distribution!");
            //var watch = new Stopwatch();
            //watch.Start();
            startUserProcessorTm = p.UserProcessorTime.TotalMilliseconds;
            foreach (var inst in instances)
            {
                // PriceWeight algorithm
                Console.Write(inst.Id + " " + inst.NumberOfItems + " PW: ");
                var priceWeight = new PriceWeightRatio(inst);
                if (!priceWeight.Found) Console.Write("Does not have solution!");
                else Console.Write(priceWeight.BestPrice + "\n");
            }
            endUserProcessorTm = p.UserProcessorTime.TotalMilliseconds;
            //watch.Stop();
            Console.WriteLine(endUserProcessorTm - startUserProcessorTm);

        }
    }
}
