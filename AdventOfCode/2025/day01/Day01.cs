using System.Diagnostics;
using System.Reflection.Emit;

namespace AOC2025.Day01;

internal class Day01
{
    string path = @"2025\day01\Input.txt";

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
        var rotations = ReadFile()
            .Split("\r\n", StringSplitOptions.TrimEntries)
            .Select(line => (line[0], int.Parse(line[1..])));
        int dial = 50, times = 0;

        foreach (var (dir, dis) in rotations)
        {
            dial = (dir, dis) switch
            {
                ('L', var s) => (dial - s + 100) % 100,
                ('R', var s) => (dial + s + 100) % 100,
                _ => dial
            };

            if (dial == 0) times++;
        }

        return times;
    }

    public int Part2()
    {
        var rotations = ReadFile()
            .Split("\r\n", StringSplitOptions.TrimEntries)
            .Select(line => (line[0], int.Parse(line[1..])));
        int dial = 50, times = 0;

        foreach (var (dir, dis) in rotations)
        {
            times += dis / 100;

            int mod = (dir == 'L' ? -dis : dis) % 100;
            if ((dial != 0) &&                          // starting at 0 not counting as cycle
                (dial + mod <= 0 || dial + mod >= 100)) // causes cycle
            {
                times++;
            }

            dial = (dial + mod + 100) % 100;
        }

        return times;
    }
}
