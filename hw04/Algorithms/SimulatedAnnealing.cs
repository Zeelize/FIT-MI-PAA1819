using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Xml;
using paa_hw4.Structs;

namespace paa_hw4.Algorithms
{
    public class SimulatedAnnealing
    {
        // ReSharper disable InconsistentNaming
        private readonly double INIT_TEMP;
        private readonly double FINAL_TEMP;
        private readonly double COOLING_CONSTANT;
        private readonly int STEPS;
        // ReSharper restore InconsistentNaming

        private readonly List<Tuple<int, int>> _items;
        private readonly int _numItems;
        private readonly int _capacity;

        public SimulatedAnnealing(Instance inst)
        {
            // Set constants
            INIT_TEMP = 1000.0;
            FINAL_TEMP = 10.0;
            COOLING_CONSTANT = 0.85;
            STEPS = 100;
            
            // Fill variables with example data
            _items = inst.Items;
            _numItems = inst.NumberOfItems;
            _capacity = inst.Capacity;
        }
        
        public SimulatedAnnealing(Instance inst, double it, double ft, double cc, int s)
        {
            // Set constants
            INIT_TEMP = it;
            FINAL_TEMP = ft;
            COOLING_CONSTANT = cc;
            STEPS = s;
            
            // Fill variables with example data
            _items = inst.Items;
            _numItems = inst.NumberOfItems;
            _capacity = inst.Capacity;
        }

        public int SaSolver()
        {
            var initOpt = SetRandomValidOption();
            var bestPrice = GetPriceForOption(initOpt);

            var temp = INIT_TEMP;

            while (!Frozen(temp))
            {
                //Console.WriteLine(bestPrice);
                for (var i = 0; i < STEPS; i++)
                {
                    // find new state
                    var newOpt = SetRandomValidOption();
                    
                    // compare
                    bestPrice = FindBestSolution(bestPrice, newOpt, temp);
                }

                temp *= COOLING_CONSTANT;
            }

            return bestPrice;
        }

        private List<bool> SetRandomOption()
        {
            var opt = new List<bool>();
            var rnd = new Random();

            for (var i = 0; i < _numItems; i++)
            {
                opt.Add(rnd.Next() % 2 == 0);
            }

            return opt;
        }
        
        private List<bool> SetRandomValidOption()
        {
            var opt = SetRandomOption();
            while (GetWeightForOption(opt) > _capacity)
            {
                opt = SetRandomOption();
            }

            return opt;
        }

        private int GetWeightForOption(List<bool> opt)
        {
            var weight = 0;
            for (var i = 0; i < _numItems; i++)
            {
                if (opt[i])
                {
                    weight += _items[i].Item1;
                }
            }

            return weight;
        }

        private int GetPriceForOption(List<bool> opt)
        {
            var price = 0;
            for (var i = 0; i < _numItems; i++)
            {
                if (opt[i])
                {
                    price += _items[i].Item2;
                }
            }

            return price;
        }

        private int FindBestSolution(int bestPrice, List<bool> opt, double temp)
        {
            var rnd = new Random();
            var newPrice = GetPriceForOption(opt);
            var delta = newPrice - bestPrice;
            // if the new distance is better, accept it
            if (delta > 0)
            {
                return newPrice;
            }

            // if it is worse, accept it with prob level
            var prob = rnd.NextDouble();
            return prob < Math.Exp(delta/temp) ? newPrice : bestPrice;

            //return bestPrice < GetPriceForOption(opt) ? GetPriceForOption(opt) : bestPrice;
        }

        private bool Frozen(double temp)
        {
            return (temp < FINAL_TEMP);
        }
    }
}