using System.Collections.Generic;

namespace AOC2017.Day10
{
    internal class Day10
    {
        private int total { get; set; }
        string path = @"2017\day10\Input.txt";

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
            int[] list = Enumerable.Range(0, 256).ToArray();
            int[] lengths = ReadFile()
                            .Split(",")
                            .Select(d => int.Parse(d.Trim()))
                            .ToArray();
            KnotHash(lengths, list);
            return list[0] * list[1];
        }

        public string Part2()
        {
            string input = ReadFile();
            int[] lengths = ParseLengthSequence(input), 
                  list = Enumerable.Range(0, 256).ToArray();

            KnotHash64(lengths, ref list);
            int[,] sparse = GetSparseHash(list);
            int[] dense = GetDenseHash(sparse);
            string s = string.Join("", 
                                   dense.Select(i =>
                                   {
                                       string s = "";
                                       if (i < 16) s += "0";
                                       s += i.ToString("x");
                                       return s;
                                   })
            );

            return s;
        }

        private void KnotHash(int[] lengths, int[] list)
        {
            int pos = 0, sz = 0;
            foreach (int length in lengths)
            {
                int[] sub = CopySubset(list, pos, length);
                for (int i = 0; i < length; i++)
                {
                    list[(pos + i) % list.Length] = sub[i];
                }

                pos = (pos + length + sz) % list.Length;
                sz++;
            }
        }

        private void KnotHash64(int[] lengths, ref int[] list) 
        {
            int pos = 0, sz = 0;
            for(int round = 0; round < 64; round++)
            {
                foreach (int length in lengths)
                {
                    int[] sub = CopySubset(list, pos, length);
                    for (int i = 0; i < length; i++)
                    {
                        list[(pos + i) % list.Length] = sub[i];
                    }

                    pos = (pos + length + sz) % list.Length;
                    sz++;
                }
            }
        }

        private int[,] GetSparseHash(int[] input)
        {
            int[,] arr = new int[16, 16];
            for(int i = 0; i < 256; i++)
            {
                arr[i / 16, i % 16] = input[i];
            }

            return arr;
        }

        private int[] GetDenseHash(int[,] sparse)
        {
            int[] dense = new int[16];
            int i = 0;
            for(int row = 0; row < 16; row++)
            {
                int sum = 0;
                
                for (int col = 0; col < 16; col++)
                {
                    sum ^= sparse[row, col];
                }

                dense[i++] = sum;
            }

            return dense;
        }

        private int[] CopySubset(int[] list, int pos, int length)
        {
            int[] sub = new int[length];
            int index = 0;
            for(int i = pos; i < pos+length; i++)
            {
                sub[index++] = list[i % list.Length];
            }

            Array.Reverse(sub);
            return sub;
        }

        private int[] ParseLengthSequence(string input)
        {
            return input
                   .Select(c => (int)c).ToArray()
                   .Concat(new int[] { 17, 31, 73, 47, 23 }).ToArray();
        }
    }
}
