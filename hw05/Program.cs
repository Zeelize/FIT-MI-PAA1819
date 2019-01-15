using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using paa_hw5.Entity;
using paa_hw5.InputReader;
using paa_hw5.Solver;

namespace paa_hw5
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

            var instances = new List<Formula>();
            foreach (var file in files)
            {
                var formula = Reader.LoadInstance(file);

                if (!formula.CheckInstance())
                {
                    Console.WriteLine("Formula wrongly loaded! (count of variables of clauses not match)");
                    continue;
                }
                
                instances.Add(formula);
            }
            
            //  INIT TEMP
            for (var i = 100; i <= 2500; i += 600)
            {
                long time = 0;
                foreach (var formula in instances)
                {
                    var sw = new Stopwatch();
                    sw.Start();
                    var sa = new SimulatedAnnealing(i, 10, 0.95, 400);
                    sa.Solve(formula);
                    sw.Stop();
                    time += sw.ElapsedMilliseconds;
                }
                
                Console.WriteLine("INIT TEMP: " + i);
                Console.WriteLine("TIME: " + Math.Round((double)time / instances.Count, 4) );
            }
            Console.WriteLine();
            
            //  INSIDE CYCLE
            for (var i = 100; i <= 500; i += 100)
            {
                long time = 0;
                foreach (var formula in instances)
                {
                    var sw = new Stopwatch();
                    sw.Start();
                    var sa = new SimulatedAnnealing(500, 10, 0.95, i);
                    sa.Solve(formula);
                    sw.Stop();
                    time += sw.ElapsedMilliseconds;
                }
                
                Console.WriteLine("CYCLE: " + i);
                Console.WriteLine("TIME: " + Math.Round((double)time / instances.Count, 4) );
            }
            Console.WriteLine();
            
            //  COEFF. COOLING
            for (var i = 0.55; i < 1; i += 0.1)
            {
                long time = 0;
                foreach (var formula in instances)
                {
                    var sw = new Stopwatch();
                    sw.Start();
                    var sa = new SimulatedAnnealing(500, 10, i, 400);
                    sa.Solve(formula);
                    sw.Stop();
                    time += sw.ElapsedMilliseconds;
                }
                
                Console.WriteLine("CC: " + i);
                Console.WriteLine("TIME: " + Math.Round((double)time / instances.Count, 4) );
            }


            /*
            var inst = instances[0];
            // solve formula with brute-force
            //var bruteResult = BruteForce.Solve(inst);
            
            //Console.WriteLine(bruteResult);

            // solve formula with simulated annealing
            var sw = new Stopwatch();
            sw.Start();
            var saSolver = new SimulatedAnnealing();
            var saResult = saSolver.Solve(inst);
            sw.Stop();
            
            Console.WriteLine(saResult);
            Console.WriteLine(sw.ElapsedMilliseconds);
            */
        }
    }
}