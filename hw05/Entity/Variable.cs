using System.Dynamic;

namespace paa_hw5.Entity
{
    public class Variable
    {
        private readonly int _index;
        private readonly int _weight;

        public Variable(int id, int w)
        {
            _index = id;
            _weight = w;
        }

        public int GetIndex()
        {
            return _index;
        }

        public int GetWeight()
        {
            return _weight;
        }
    }
}