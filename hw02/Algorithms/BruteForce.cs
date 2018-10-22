using System;
using System.Collections.Generic;
using paa_hw2.Structs;

// ReSharper disable once CheckNamespace
namespace paa_hw2.Algorithms
{
    public class BruteForce
    {
        private readonly List<Tuple<int, int>> _items;
        private readonly int _numItems;
        private readonly int _capacity;
        
        // Solutions
        public readonly int BestPrice;
        
        public BruteForce(Instance inst)
        {
            // Fill variables with example data
            _items = inst.Items;
            _numItems = inst.NumberOfItems;
            _capacity = inst.Capacity;
            
            // Calculate best possible combination with brute force
            // With recursion -> every single item has to branches -> take it or not take it
            var solution = RecursionSolver(0, 0, 0/*, new List<byte>()*/);
            
            BestPrice = solution.Item2;
        }
        
        private Tuple<int, int> RecursionSolver(int index, int currentWeight, int currentPrice) {
            // If index is bigger than List count do not continue 
            if (index >= _numItems) return Tuple.Create(currentWeight, currentPrice);
            
            // Take price and weight from List
            var price = _items[index].Item2;
            var weight = _items[index].Item1;
            index++;
            
            // Returning variables
            Tuple<int, int> added = null;

            // If you can put it in, do it
            if (weight + currentWeight <= _capacity)
            {
                added = RecursionSolver(index, weight + currentWeight, price + currentPrice);
            }
            
            // Do not put it in
            var notAdded = RecursionSolver(index, currentWeight, currentPrice);
            
            // Return better solution
            if (added == null) return notAdded;

            return added.Item2 >= notAdded.Item2 ? added : notAdded;
        }
    }
}