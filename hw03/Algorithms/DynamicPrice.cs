using System;
using System.Collections.Generic;
using paa_hw3.Structs;

namespace paa_hw3.Algorithms
{
    public class DynamicPrice
    {
        private readonly List<Tuple<int, int>> _items;
        private readonly int _numItems;
        private readonly int _allPrices;
        private int?[,] _array;

        public readonly int BestPrice = -1;
        
        public DynamicPrice(Instance inst)
        {
            // Fill variables with example data
            _items = inst.Items;
            _numItems = inst.NumberOfItems;
            var capacity = inst.Capacity;
            
            // Calculate price and weight of all items
            _allPrices = 0;
            foreach (var item in _items)
            {
                _allPrices += item.Item2;
                
            }
            
            // Compute bag with dynamic programming - decomposition via Price
            // Create 2D array with number of rows = allPrices and columns = number of items
            // each array item contains minimum weight for the price
            _array = new int?[_allPrices + 1, _numItems + 1];
            
            // Fill zero-d row
            for (var i = 0; i <= _numItems; i++)
            {
                _array[0, i] = 0;
            }
            
            // Start algortihm
            StartAlgorithm();
            
            // Find best solution
            for (var i = _allPrices; i >= 0; i--)
            {
                if (_array[i, _numItems] == null || _array[i, _numItems].Value > capacity) continue;
                BestPrice = i;
                return;
            }
        }

        private void StartAlgorithm()
        {
            for (var c = 1; c <= _allPrices; c++)
            {
                for (var i = 1; i <= _numItems; i++)
                {
                    var tmpIndex = c - _items[i - 1].Item2;

                    // If index is negative continue
                    if (tmpIndex < 0)
                    {
                        _array[c, i] = _array[c, i - 1];
                        continue;
                    }
                    
                    // Own Min function
                    _array[c, i] = PutMin(c, i, tmpIndex);
                }
            }
        }

        private int? PutMin(int c, int i, int tmp)
        {
            var a = _array[c, i - 1];
            var b = _array[tmp, i - 1];
            var w = _items[i - 1].Item1;

            if (a == null && b == null) return null;
            if (a == null) return b + w;
            if (b == null) return a;

            return Math.Min(a.Value, b.Value + w);
        }
    }
}