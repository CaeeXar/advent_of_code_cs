using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2017.Day04
{
    internal class Day04
    {
        string path = @"2017\day04\Input.txt";

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
            string text = ReadFile();
            return FindValidPassphrases(text);
        }

        public int Part2()
        {
            string text = ReadFile();
            return FindValidPassphrasesAnagram(text);
        }

        private int FindValidPassphrases(string input) 
        {
            int valid = 0;
            string[] lines = input.Split('\n');
            foreach (string line in lines)
            {
                string[] phrases = line.Split(' ').Select(phrase => phrase.Trim()).ToArray();
                int bef = phrases.Length;
                string[] distinct = phrases.Distinct().ToArray();
                int aft = distinct.Length;
                if (bef == aft) valid++;
            }

            return valid;
        }

        private int FindValidPassphrasesAnagram(string input)
        {
            int valid = 0;
            string[] lines = input.Split('\n');
            foreach (string line in lines)
            {
                string[] phrases = line.Split(' ')
                                       .Select(phrase => new string(phrase.Trim().OrderBy(c => c).ToArray()))
                                       .ToArray();
                int bef = phrases.Length;

                string[] distinct = phrases.Distinct().ToArray();
                int aft = distinct.Length;

                if (bef == aft) valid++;
            }

            return valid;
        }

    }
}
