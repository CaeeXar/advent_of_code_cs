﻿namespace AOC
{
    using Point = (int x, int y);
    internal class Program
    {
        static void Main(string[] args)
        {
            // 2017
            //AOC2017.Day19.Day19 day = new AOC2017.Day19.Day19();
            //System.Console.WriteLine($"\t- Part 1: {day.Part1()}\n");
            //System.Console.WriteLine($"\t- Part 2: {day.Part2()}");

            // 2024
            AOC2024.Day08.Day08 day = new AOC2024.Day08.Day08();
            System.Console.WriteLine($"\t- Part 1: {day.Part1()}\n");
            System.Console.WriteLine($"\t- Part 2: {day.Part2()}");
        }
    }
}
