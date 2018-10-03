using System;
using System.Collections.Generic;
using System.Linq;

// ReSharper disable once CheckNamespace
namespace paa_hw1
{
    public class PriceWeightRatio
    {
        private readonly List<Tuple<int, int>> _items;
        private readonly int _numItems;
        private readonly int _capacity;
        
        // Solutions
        public bool Found;
        public int BestPrice;
        //public readonly int BestWeight;
        //public readonly List<byte> BestItems;

        public PriceWeightRatio(Instance inst)
        {
            // Init variables
            Found = false;

            // Fill variables with example data
            _numItems = inst.NumberOfItems;
            _capacity = inst.Capacity;
            
            // Calculate best possible solution
            // Sort the items according to price/weight ratio
            _items = inst.Items.OrderByDescending(o => o.Item2 / o.Item1).ToList();
            AddItemsToBag();
        }

        private void AddItemsToBag()
        {
            var currentWeight = 0;
            var index = 0;
            var currentPrice = 0;
            while (index < _items.Count && currentWeight < _capacity)
            {
                if (currentWeight + _items[index].Item1 <= _capacity)
                {
                    // Add Item
                    currentPrice += _items[index].Item2;
                    currentWeight += _items[index].Item1;

                    Found = true;
                }

                index++;
            }
            
            // Save best price
            BestPrice = currentPrice;
        }
    }
}