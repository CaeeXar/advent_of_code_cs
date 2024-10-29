using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2017.Day08
{
    internal class Instruction
    {
        public Instruction(string line)
        {
            // b inc 5 if a > 1
            // 0  1  2  3 4 5 6
            string[] trimmedData = line.Split(" ").Select(d => d.Trim()).ToArray();
            
            // this reg
            this.RegisterToModify = trimmedData[0];
            
            // inc or dec
            if (trimmedData[1] == "inc")
            {
                this.Increase = true;
            }
            else
            {
                this.Increase = false;
            }

            // how much
            this.Amount = int.Parse(trimmedData[2]);

            // other reg
            this.RegisterToCheck = trimmedData[4];

            // <, >, <=, ...
            this.Condition = trimmedData[5];
            this.ConditionValue = int.Parse(trimmedData[6]);
        }

        public string RegisterToModify { get; set; }

        public string RegisterToCheck { get; set; }

        public bool Increase { get; set; }
        
        public int Amount { get; set; }

        public string Condition { get; set; }

        public int ConditionValue { get; set; }
    }
}
