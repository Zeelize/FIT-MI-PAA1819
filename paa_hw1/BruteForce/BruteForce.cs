using System;
using System.Collections.Generic;
using System.Linq;

namespace paa_hw1
{
    public class BruteForce
    {
        private readonly List<int> _prices;
        private readonly List<int> _weights;
        private readonly int _items;
        private readonly int _capacity;
        
        // Solutions
        public readonly bool Found;
        public readonly int BestPrice;
        public readonly int BestWeight;
        public readonly List<byte> BestItems;
        

        public BruteForce()
        {
            // Init variables
            _prices = new List<int>();
            _weights = new List<int>();
            Found = false;

            // Fill variables with exmaple data
            _items = 3;
            _capacity = 5;
            _prices.Add(10);
            _prices.Add(20);
            _prices.Add(30);
            _weights.Add(2);
            _weights.Add(3);
            _weights.Add(5);

            // Check if all variables are right set
            if (_prices.Count != _weights.Count || _prices.Count != _items)
            {
                return;
            }

            // Calculate best possible combination with brute force
            // With recursion -> every single item has to branches -> take it or not take it
            var solution = RecursionSolver(0, 0, 0, new List<byte>());
            if (solution.Item3.Count == 0 || solution.Item2 == 0) return;

            BestPrice = solution.Item2;
            BestWeight = solution.Item1;
            BestItems = solution.Item3;
            Found = true;
        }
        
        private Tuple<int, int, List<byte>> RecursionSolver(int index, int currentWeight, int currentPrice, List<byte> currentItems) {
            // If index is bigger than List count do not continue 
            if (index >= _items) return Tuple.Create(currentWeight, currentPrice, currentItems);
            
            // Take price and weight from List
            var price = _prices[index];
            var weight = _weights[index];
            index++;
            
            // Returning variables
            Tuple<int, int, List<byte>> added = null;

            // If you can put it in, do it
            if (weight + currentWeight <= _capacity)
            {
                added = RecursionSolver(index, weight + currentWeight, price + currentPrice, currentItems.Concat(new []{(byte)1}).ToList());
            }
            
            // Do not put it in
            var notAdded = RecursionSolver(index, currentWeight, currentPrice, currentItems.Concat(new []{(byte)0}).ToList());
            
            // Return better solution
            if (added == null) return notAdded;

            return added.Item2 >= notAdded.Item2 ? added : notAdded;
        }
    }
}