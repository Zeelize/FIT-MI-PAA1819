using System;

namespace paa_hw5.Entity
{
    /// <summary>
    /// Contains configuration and settings of all variables
    /// </summary>
    public class Solution
    {
        private bool[] _rating;
        private readonly Formula _formula;

        public Solution(Formula form)
        {
            _rating = new bool[form.GetVariablesNum()];
            _formula = form;
        }

        public bool EvaluateFormula()
        {
            // all clauses has to be true
            var clauses = _formula.GetClauses();
            foreach (var clause in clauses)
            {
                if (!EvaluateClause(clause)) return false;
            }

            return true;
        }

        private bool EvaluateClause(Clause clause)
        {
            var literals = clause.GetClauseLiterals();
            var res = false;

            foreach (var literal in literals)
            {
                var index = literal.GetVariable().GetIndex() - 1;
                var oneLiteral = false;
                // 1 with neg = 0
                if (_rating[index] && literal.GetNegation()) oneLiteral = false;
                // 1 no neg = 1
                else if (_rating[index] && !literal.GetNegation()) oneLiteral = true;
                // 0 with neg = 1
                else if (!_rating[index] && literal.GetNegation()) oneLiteral = true;
                // 0 no neg = 0
                else if (!_rating[index] && !literal.GetNegation()) oneLiteral = false;

                res = res || oneLiteral;
            }

            return res;
        }
        
        public bool[] GetRating()
        {
            return _rating;
        }
        
        private void SetRandomRating() {
            var rnd = new Random();
            for (var i = 0; i < _formula.GetVariablesNum(); i++) {
                _rating[i] = (rnd.Next() % 2 == 0);
            }
        }
        
        public bool SetRandomValidRating() {
            for(var i = 0; i < 1000; i++) {
                SetRandomRating();

                if(EvaluateFormula()) return true;
            }
            
            // If not valid, just go with it
            return false;
        }

        public void SetRandomNeighbour()
        {
            // change just one item/variable
            var rnd = new Random();
            var i = rnd.Next(0, _formula.GetVariablesNum());
            _rating[i] = !_rating[i];
        }
        
        public void SetRandomValidNeighbour()
        {
            for(var i = 0; i < 500; i++) {
                SetRandomNeighbour();

                if(EvaluateFormula()) return;
            }
        }
        
        public int CalculateWeight()
        {
            var weight = 0;

            var variables = _formula.GetVariables();
            for (var i = 0; i < variables.Count; i++)
            {
                if (!_rating[i]) continue;

                weight += variables[i].GetWeight();
            }

            return weight;
        }

        public void SetRating(bool[] rating)
        {
            _rating = rating;
        }
    }
}