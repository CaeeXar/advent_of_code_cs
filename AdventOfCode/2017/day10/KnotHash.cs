using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2017.Day10
{
    internal class KnotHash
    {
        private static readonly int[] sequence = [17, 31, 73, 47, 23];

        public static string GetKnotHash(string input)
        {
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

        private static int[,] GetSparseHash(int[] input)
        {
            int[,] arr = new int[16, 16];
            for (int i = 0; i < 256; i++)
            {
                arr[i / 16, i % 16] = input[i];
            }

            return arr;
        }

        private static int[] GetDenseHash(int[,] sparse)
        {
            int[] dense = new int[16];
            int i = 0;
            for (int row = 0; row < 16; row++)
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

        private static void KnotHash64(int[] lengths, ref int[] list)
        {
            int pos = 0, sz = 0;
            for (int round = 0; round < 64; round++)
            {
                foreach (int length in lengths)
                {
                    int[] sub = CopySubsetReverse(list, pos, length);
                    for (int i = 0; i < length; i++)
                    {
                        list[(pos + i) % list.Length] = sub[i];
                    }

                    pos = (pos + length + sz) % list.Length;
                    sz++;
                }
            }
        }

        private static int[] CopySubsetReverse(int[] list, int pos, int length)
        {
            int[] sub = new int[length];
            int index = 0;
            for (int i = pos; i < pos + length; i++)
            {
                sub[index++] = list[i % list.Length];
            }

            Array.Reverse(sub);
            return sub;
        }

        private static int[] ParseLengthSequence(string input)
        {
            return input
                   .Select(c => (int)c).ToArray()
                   .Concat(KnotHash.sequence).ToArray();
        }
    }
}
