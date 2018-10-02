using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace paa_hw1
{
    public class PriceWeightRatio
    {
        private readonly List<Tuple<int, int>> _items;
        private readonly int _numItems;
        private readonly int _capacity;
        
        // Solutions
        public readonly bool Found;
        public readonly int BestPrice;
        public readonly int BestWeight;
        public readonly List<byte> BestItems;

        public PriceWeightRatio(Instance inst)
        {
            // Init variables
            Found = false;

            // Fill variables with exmaple data
            _items = inst.Items;
            _numItems = inst.NumberOfItems;
            _capacity = inst.Capacity;
            
            // Calculate best possible solution
            // Sort the items according to price/weight ratio
            // TODO
        }
        
    }
}