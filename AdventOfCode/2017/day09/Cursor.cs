using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2017.day09
{
    internal class Cursor
    {
        public Cursor(string data)
        {
            this.Data = data;
        }

        public string Data { get; set; }

        public int Position { get; set; }
    }
}
