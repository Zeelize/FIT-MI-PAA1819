using System.Collections.Generic;
using paa_hw5.Entity;

namespace paa_hw5.Solver
{
    public static class BruteForce
    {
        /// <summary>
        /// Will generate all subsets (that is what we need) and compare them
        /// https://www.geeksforgeeks.org/finding-all-subsets-of-a-given-set-in-java/ 
        /// </summary>
        public static int Solve(Formula formula)
        {
            var bestWeight = 0;
            var n = formula.GetVariablesNum();
            var sol = new Solution(formula);
            
            for (long i = 0; i < ((long) 1 << n); i++)
            {
                var rating = new bool[n];
                for (var j = 0; j < n; j++)
                {
                    if ((i & (1 << j)) > 0) rating[j] = true;
                    else rating[j] = false;
                }
                
                // Set Solution
                sol.SetRating(rating);
                
                // Check if its true
                if (!sol.EvaluateFormula()) continue;
                
                // Calculate it
                var nw = sol.CalculateWeight();
                if (nw > bestWeight) bestWeight = nw;
            }

            return bestWeight;
        }
    }
}