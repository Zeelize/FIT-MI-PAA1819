using System;
using System.Collections.Generic;
using System.Linq;

namespace paa_hw5.Entity
{
    public class Formula
    {
        // information about variables
        private int _variablesNum;
        private readonly List<Variable> _variables;
        
        // information about clauses
        private int _clausesNum;
        private readonly List<Clause> _clauses;

        public Formula()
        {
            _variablesNum = 0;
            _variables = new List<Variable>();

            _clausesNum = 0;
            _clauses = new List<Clause>();
        }

        public void AddVariable(int id, int weight)
        {
            var newVar = new Variable(id, weight);

            _variables.Add(newVar);
        }

        public void AddClause(List<Tuple<int, bool>> lits)
        {
            var newClause = new Clause();

            foreach (var lit in lits)
            {
                newClause.AddLiteral(_variables[lit.Item1 - 1], lit.Item2);
            }
            
            _clauses.Add(newClause);
        }

        public List<Variable> GetVariables()
        {
            return _variables;
        }

        public List<Clause> GetClauses()
        {
            return _clauses;
        }
        
        public void SetVariablesNum(int num)
        {
            _variablesNum = num;
        }

        public void SetClausesNum(int num)
        {
            _clausesNum = num;
        }

        public int GetVariablesNum()
        {
            return _variablesNum;
        }

        public bool CheckInstance()
        {
            return _variables.Count == _variablesNum && _clauses.Count == _clausesNum;
        }
    }
}