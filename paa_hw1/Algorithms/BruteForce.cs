using System;
using System.Collections.Generic;
using System.Linq;

// ReSharper disable once CheckNamespace
namespace paa_hw1
{
    public class BruteForce
    {
        //private readonly List<int> _prices;
        //private readonly List<int> _weights;
        private readonly List<Tuple<int, int>> _items;
        private readonly int _numItems;
        private readonly int _capacity;
        
        // Solutions
        public readonly bool Found;
        public readonly int BestPrice;
        public readonly int BestWeight;
        public readonly List<byte> BestItems;
        

        public BruteForce(Instance inst)
        {
            // Init variables
            Found = false;

            // Fill variables with exmaple data
            _items = inst.Items;
            _numItems = inst.NumberOfItems;
            _capacity = inst.Capacity;
            
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
            if (index >= _numItems) return Tuple.Create(currentWeight, currentPrice, currentItems);
            
            // Take price and weight from List
            var price = _items[index].Item2;
            var weight = _items[index].Item1;
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