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
        private readonly int _allPrices;
        private int?[,] _array;
        private int[] _newP;
        
        public readonly int BestPrice = 0;

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
            
            // Calcul e and K
            var k = e * ((double)p / _numItems);
            
            // create new array for new prices
            _newP = new int[_numItems];
            _allPrices = 0;
            for (var i = 0; i < _numItems; i++)
            {
                var tmp = (int)(_items[i].Item2 / k);
                _newP[i] = tmp;
                _allPrices += tmp;
            }
            
            _array = new int?[_allPrices + 1, _numItems + 1];
            
            // Fill zero-d row
            for (var i = 0; i <= _numItems; i++)
            {
                _array[0, i] = 0;
            }
            
            // Start algortihm
            StartAlgorithm();
            
            // Find best solution
            var j = -1;
            for (var i = _allPrices; i >= 0; i--)
            {
                if (_array[i, _numItems] == null || _array[i, _numItems].Value > capacity) continue;
                //BestPrice = (int)((double)i * k);
                j = i;
                break;
            }
            
            // Backtrack of an items
            for (var i = _numItems; i > 0; i--)
            {
                if (j <= 0) break;
                if (_array[j, i] == _array[j, i - 1]) continue;
                
                BestPrice += _items[i - 1].Item2;
                //result -= _newP[i - 1];
                j -= _newP[i - 1];
            }
        }
        
        private void StartAlgorithm()
        {
            for (var c = 1; c <= _allPrices; c++)
            {
                for (var i = 1; i <= _numItems; i++)
                {
                    var tmpIndex = c - _newP[i - 1];

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