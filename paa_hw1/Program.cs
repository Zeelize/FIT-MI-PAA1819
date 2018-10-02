using System;
using System.Collections.Generic;

namespace paa_hw1
{
    class Program
    {
        private List<int> _prices;
        private List<int> _weights;
        private int _items;
        private int _capacity;
        
        static void Main(string[] args)
        {
            // Knapback problem
            Console.WriteLine("Knapback problem!");

            // Init variables
            _prices = new List<int>();
            _weights = new List<int>();

            // Fill variables with exmaple data
            _items = 3;
            capacity = 5;
            prices.Add(10);
            prices.Add(20);
            prices.Add(30);
            weights.Add(2);
            weights.Add(3);
            weights.Add(5);

            // Check if all variables are right set
            // TODO

            // Calculate best possible combination with brute force
            // With recursion -> every single item has to branches -> take it or not take it


        }

        private int RecursionSolver(int index, int currentCapacity) {
            // If index is bigger than List count 
            // TODO
        }
    }
}
