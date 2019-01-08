using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using paa_hw5.InputReader;
using paa_hw5.Solver;

namespace paa_hw5
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            // load file from args
            var formula = Reader.LoadInstance(args[0]);

            if (!formula.CheckInstance())
            {
                Console.WriteLine("Formula wrongly loaded! (count of variables of clauses not match)");
                return;
            }

            // solve formula with brute-force
            var bruteResult = BruteForce.Solve(formula);
            
            Console.WriteLine(bruteResult);

            // solve formula with simulated annealing
            var saSolver = new SimulatedAnnealing();

            for (int i = 0; i < 20; i++)
            {
                var saResult = saSolver.Solve(formula);
            
                Console.WriteLine(saResult);   
            }

            // todo count error and time
            // todo print results
        }
    }
}