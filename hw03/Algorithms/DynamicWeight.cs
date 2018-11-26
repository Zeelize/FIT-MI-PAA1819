using System;
using System.Collections.Generic;
using paa_hw3.Structs;

namespace paa_hw3.Algorithms
{
    public class DynamicWeight
    {
        private readonly List<Tuple<int, int>> _items;
        private readonly int _numItems;
        private readonly int _capacity;
        private int[,] _array;

        public readonly int BestPrice;
        public int Measure = 0;
        
        public DynamicWeight(Instance inst)
        {
            // Fill variables with example data
            _items = inst.Items;
            _numItems = inst.NumberOfItems;
            _capacity = inst.Capacity;
            
            // Compute bag with dynamic programming - decomposition via Price
            // Create 2D array with number of rows = allPrices and columns = number of items
            // each array item contains minimum weight for the price
            _array = new int[_capacity + 1, _numItems + 1];
            
            StartAlgorithm();

            BestPrice = _array[_capacity, _numItems];
        }

        private void StartAlgorithm()
        {
            for (var m = 0; m <= _capacity; m++)
            {
                for (var i = 0; i <= _numItems; i++)
                {
                    // We are in new state
                    Measure++;
                    
                    // Check trivial
                    if (m == 0 || i == 0) continue;

                    var item = _items[i - 1];
                    
                    // tmp index
                    var tmp = m - item.Item1;
                    var c1 = _array[m, i - 1];
                    var cn = item.Item2;

                    // if negative = does not fit -> just put it c1
                    if (tmp < 0)
                    {
                        _array[m, i] = c1;
                        continue;
                    }

                    var c2 = _array[tmp, i - 1];

                    // c2 + cn > c1
                    if (c2 + cn > c1)
                    {
                        _array[m, i] = c2 + cn;
                        continue;
                    }

                    _array[m, i] = c1;
                }
            }
        }
    }
}