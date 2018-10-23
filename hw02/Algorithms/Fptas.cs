using System;
using System.Collections.Generic;
using System.Linq;
using paa_hw2.Structs;

namespace paa_hw2.Algorithms
{
    public class Fptas
    {
        private readonly List<Tuple<int, int>> _items;
        private readonly int _numItems;
        private readonly double _allPrices;
        private int?[,] _array;
        
        public readonly int BestPrice = -1;

        /// <summary>
        /// Documentation:
        ///     http://math.mit.edu/~goemans/18434S06/knapsack-katherine.pdf
        ///     https://en.wikipedia.org/wiki/Knapsack_problem
        /// </summary>
        public Fptas(Instance inst, double e)
        {
            // Fill variables with example data
            _items = inst.Items;
            _numItems = inst.NumberOfItems;
            var capacity = inst.Capacity;
            
            // Find highest price
            var p = _items.Max(r => r.Item2);
            
            // Calcul K
            var k = e * ((double)p / _numItems);
            
            // create new array for new prices
            var newP = new double[_numItems];
            _allPrices = 0.0;
            for (var i = 0; i < _numItems; i++)
            {
                var tmp = Math.Floor(_items[i].Item2 / k);
                newP[i] = tmp;
                _allPrices += tmp;
            }
            
            // TODO Start algorithm
            
        }
    }
}