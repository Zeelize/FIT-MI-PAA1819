using System;
using System.Collections.Generic;
using paa_hw2.Structs;

namespace paa_hw2.Algorithms
{
    public class Dynamic
    {
        private readonly List<Tuple<int, int>> _items;
        private readonly int _numItems;
        private readonly int _capacity;
        private int[,] _array;
        
        public Dynamic(Instance inst)
        {
            // Fill variables with example data
            _items = inst.Items;
            _numItems = inst.NumberOfItems;
            _capacity = inst.Capacity;
            
            // Calculate price and weight of all items
            var allPrices = 0;
            foreach (var item in _items)
            {
                allPrices += item.Item2;
                
            }
            
            // Compute bag with dynamic programming - decomposition via Price
            // Create 2D array with number of rows = allPrices and columns = number of items
            // each array item contains minimum weight for the price
            _array = new int[allPrices + 1, _numItems + 1];
            
            // Fill zero-d row
            
        }
    }
}