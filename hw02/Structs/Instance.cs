using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace paa_hw2.Structs
{
    public struct Instance
    {
        public Instance(int id, int n, int c, List<Tuple<int, int>> i)
        {
            Id = id;
            NumberOfItems = n;
            Capacity = c;
            Items = i;
        }

        public readonly int Id;
        public readonly int NumberOfItems;
        public readonly int Capacity;
        public readonly List<Tuple<int, int>> Items;
    }
}