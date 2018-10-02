using System;
using System.Collections.Generic;

namespace paa_hw1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // Knapback problem
            Console.WriteLine("Knapback problem!");
            
            // BruteForce algorithm
            Console.WriteLine("\nSolve with recursion brute force!");
            var bruteForce = new BruteForce();
            if (!bruteForce.Found) Console.WriteLine("Does not have solution!");
            else
            {
                Console.WriteLine("Best found price: " + bruteForce.BestPrice);
                Console.WriteLine("Weight of solution: " + bruteForce.BestWeight);
                Console.Write("Vector of items: ");
                foreach (var item in bruteForce.BestItems)
                {
                    Console.Write(item + " ");
                }
                Console.Write("\n");
            }
            
            // Price-Weight algorithm
            Console.WriteLine("\nSolve with price-weight distribution!");

        }
    }
}
