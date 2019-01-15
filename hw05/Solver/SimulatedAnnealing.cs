using System;
using paa_hw5.Entity;

namespace paa_hw5.Solver
{
    public class SimulatedAnnealing
    {
        // ReSharper disable InconsistentNaming
        private readonly double INIT_TEMP;
        private readonly double FINAL_TEMP;
        private readonly double COOLING_CONSTANT;
        private readonly int STEPS;
        // ReSharper restore InconsistentNaming
        
        public SimulatedAnnealing() {
            INIT_TEMP = 400;
            FINAL_TEMP = 10;
            COOLING_CONSTANT = 0.90;
            STEPS = 400;
        }
        
        public SimulatedAnnealing(double it, double ft, double cc, int s)
        {
            // Set constants
            INIT_TEMP = it;
            FINAL_TEMP = ft;
            COOLING_CONSTANT = cc;
            STEPS = s;
        }
        
        public int Solve(Formula formula)
        {
            var oldSol = new Solution(formula);
            var newSol = new Solution(formula);
            
            var globalMax = oldSol.SetRandomValidRating() ? oldSol.CalculateWeight() : 0;
            
            var temp = INIT_TEMP;
            while (!Frozen(temp))
            {
                //Console.WriteLine(oldSol.CalculateWeight());
                for (var i = 0; i < STEPS; i++)
                {
                    // find new state
                    newSol.SetRating(oldSol.GetRating());
                    newSol.SetRandomNeighbour();
                    
                    // compare
                    oldSol = FindBestSolution(oldSol, newSol, temp);
                    
                    // compare it to globalBest
                    var tmp = oldSol.CalculateWeight();
                    if (oldSol.EvaluateFormula() && tmp > globalMax)
                    {
                        globalMax = tmp;
                    }
                }

                temp *= COOLING_CONSTANT;
            }

            // If not found, return 0
            return globalMax;
        }

        private static Solution FindBestSolution(Solution currSol, Solution newSol, double temp)
        {
            var rnd = new Random();
            var currWeight = currSol.CalculateWeight();
            var newWeight = newSol.CalculateWeight();
            var oldTrue = currSol.EvaluateFormula();
            var newTrue = newSol.EvaluateFormula();
            
            // if better accept always
            //if (newWeight > currWeight) return newSol;
            
            // if better accept always
            if ((!oldTrue && newTrue) ||
                ((oldTrue && newTrue) && (newWeight > currWeight)) ||
                (!oldTrue && (newWeight > currWeight))) {
                return newSol;
            }

            var exponent = (newWeight - currWeight) / temp;
            var delta = Math.Exp(exponent);
            if (rnd.NextDouble() < delta) return newSol;

            return currSol;
        }
        
        private bool Frozen(double temp)
        {
            return (temp < FINAL_TEMP);
        }
    }
}