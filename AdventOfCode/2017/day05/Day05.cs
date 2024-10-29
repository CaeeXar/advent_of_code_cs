using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2017.Day05
{
    internal class Day05
    {
        string path = @"2017\day05\Input.txt";

        public string ReadFile()
        {
            try
            {
                return File.ReadAllText(path);
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
                return null;
            }
        }

        public int Part1()
        {
            int[] instructions = ParseInput(ReadFile());
            return Solve(instructions);
        }

        public int Part2()
        {
            int[] instructions = ParseInput(ReadFile());
            return SolveStrange(instructions);
        }

        private int Solve(int[] ins)
        {
            int steps = 0, pos = 0;
            
            while (pos < ins.Length)
            {
                int next = pos + ins[pos];
                ins[pos]++;
                pos = next;
                steps++;
            }

            return steps;
        }

        private int SolveStrange(int[] ins)
        {
            int steps = 0, pos = 0;

            while (pos < ins.Length)
            {
                int next = pos + ins[pos];
                if (ins[pos] >= 3) ins[pos]--;
                else ins[pos]++;
                pos = next;
                steps++;
            }

            return steps;
        }

        private int[] ParseInput(string input)
        {
            return input.Split('\n')
                        .Select(line => line.Trim())
                        .ToArray()
                        .Select(int.Parse)
                        .ToArray();
        }
    }
}
