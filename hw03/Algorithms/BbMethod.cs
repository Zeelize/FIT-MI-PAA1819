using System;
using System.Collections.Generic;
using paa_hw3.Structs;

namespace paa_hw3.Algorithms
{
    public class BbMethod
    {
        private readonly List<Tuple<int, int>> _items;
        private readonly int _numItems;
        private readonly int _capacity;
        
        // Solutions
        public int Measure;
        public int BestPrice;
        
        public BbMethod(Instance inst)
        {
            // Fill variables with example data
            _items = inst.Items;
            _numItems = inst.NumberOfItems;
            _capacity = inst.Capacity;
            Measure = 0;
            
            // Calculate price and weight of all items
            var allPrices = 0;
            foreach (var item in _items)
            {
                allPrices += item.Item2;
                
            }
            
            // Calculate best possible combination with brute force
            // With recursion -> every single item has to branches -> take it or not take it
            BestPrice = -1;
            RecursionSolver(0, 0, 0, allPrices);
        }
        
        private Tuple<int, int> RecursionSolver(int index, int currentWeight, int currentPrice, int possiblePrice) {
            // If index is bigger than List count do not continue 
            if (index >= _numItems)
            {
                // We are at the end, save possible best price
                if (currentPrice > BestPrice) BestPrice = currentPrice;
                return Tuple.Create(currentWeight, currentPrice);
            }
            
            // If currentPrice + possiblePrice < optimal then return
            if (BestPrice != -1 && currentPrice + possiblePrice < BestPrice)
            {
                // Cant be better than current best price, ignore this branch
                return null;
            }
            
            // We are in new state, which we are trying solve
            Measure++;
            
            // Take price and weight from List
            var price = _items[index].Item2;
            var weight = _items[index].Item1;
            index++;
            
            // Returning variables
            Tuple<int, int> added = null;

            // If you can put it in, do it
            if (weight + currentWeight <= _capacity)
            {
                added = RecursionSolver(index, weight + currentWeight, price + currentPrice, possiblePrice - price);
            }
            
            // Do not put it in
            var notAdded = RecursionSolver(index, currentWeight, currentPrice, possiblePrice - price);
            
            // Return better solution
            // ReSharper disable once ConvertIfStatementToSwitchStatement
            if (added == null && notAdded == null) return null;
            if (added == null) return notAdded;
            if (notAdded == null) return added;
            
            return added.Item2 >= notAdded.Item2 ? added : notAdded;
        }   
    }
}