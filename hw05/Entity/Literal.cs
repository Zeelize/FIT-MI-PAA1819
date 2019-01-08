namespace paa_hw5.Entity
{
    public class Literal
    {
        private readonly Variable _variable;
        private readonly bool _negation;

        public Literal(Variable var, bool neg)
        {
            _variable = var;
            _negation = neg;
        }

        public Variable GetVariable()
        {
            return _variable;
        }

        public bool GetNegation()
        {
            return _negation;
        }
    }
}