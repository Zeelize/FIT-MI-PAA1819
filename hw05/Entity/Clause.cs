using System.Collections.Generic;

namespace paa_hw5.Entity
{
    public class Clause
    {
        private readonly List<Literal> _literals;

        public Clause()
        {
            _literals = new List<Literal>();
        }

        public List<Literal> GetClauseLiterals()
        {
            return _literals;
        }

        public void AddLiteral(Variable variable, bool neg)
        {
            var newLit = new Literal(variable, neg);
            
            _literals.Add(newLit);
        }
    }
}