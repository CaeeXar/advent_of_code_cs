using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2017.Day12
{
    internal class Program
    {
        public int ID { get; set; }
        public int[] Neighbours { get; set; }

        public Program(int id) : this(id, Array.Empty<int>())
        {
        }

        public Program(int id, int[] neighbours)
        {
            this.ID = id;
            this.Neighbours = neighbours;
        }
    }
}
