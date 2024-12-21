using Microsoft.VisualBasic;
using System.Runtime.CompilerServices;

namespace AOC2024.Day11
{
    internal class Day11
    {
        string path = @"2024\day11\Input.txt";

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
            List<string> stones = ReadFile().Split(" ").Select(stone => stone.Trim()).ToList();
            List<string> stonesAfterBlink = new List<string>();
            int blinks = 25;

            for (int i = 0; i < blinks; i++)
            {
                foreach (string stone in stones)
                {
                    if (stone.Length == 1 && stone[0] == '0')
                    {
                        stonesAfterBlink.Add("1");
                    }
                    else if (stone.Length % 2 == 0)
                    {
                        int middle = stone.Length / 2;
                        long left = long.Parse(stone.Substring(0, middle)),
                               right = long.Parse(stone.Substring(middle));
                        stonesAfterBlink.AddRange(new List<string> 
                        { 
                            left.ToString(), 
                            right.ToString() 
                        });
                    }
                    else
                    {
                        long num = long.Parse(stone);
                        stonesAfterBlink.Add((num * 2024).ToString());
                    }
                }

                stones = new List<string>(stonesAfterBlink);
                stonesAfterBlink.Clear();
            }

            return stones.Count;
        }

        public long Part2()
        {
            Dictionary<long, long> stones = ReadFile().Split(" ").Select(stone => {
                return new KeyValuePair<long, long>(long.Parse(stone.Trim()), 1);
            }).ToDictionary();
            Dictionary<long, long> nextStones = new Dictionary<long, long>(); ;

            int blinks = 75;
            for (int i = 0; i < blinks; i++)
            {
                foreach (KeyValuePair<long, long> kvp in stones)
                {
                    long stone = kvp.Key;
                    long count = kvp.Value; // n stones create n more stones (at least)

                    if (stone == 0)
                    {
                        Update(ref nextStones, 1, count);
                    }
                    else if (stone.ToString().Length % 2 == 0)
                    {
                        string number = stone.ToString();
                        int middle = number.Length / 2;
                        long left = long.Parse(number.Substring(0, middle));
                        long right = long.Parse(number.Substring(middle));

                        Update(ref nextStones, left, count);
                        Update(ref nextStones, right, count);
                    }
                    else
                    {
                        Update(ref nextStones, stone * 2024, count);
                    }
                }

                stones = nextStones.Where(kvp => kvp.Value > 0).ToDictionary();
                nextStones.Clear();
            }

            return stones.Aggregate(0L, (acc, next) => acc += next.Value);
        }

        private void Update(ref Dictionary<long, long> stones, long key, long count)
        {
            if (stones.ContainsKey(key)) stones[key] += count; // only add, if already contained
            else stones.Add(key, count);
        }
    }
}
