using System.Runtime.CompilerServices;
using System.Text;

namespace AOC2025.Day03;

internal class Day03
{
    string path = @"2025\day03\Input.txt";

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

    public long Part1()
    {
        var lines = ReadFile().Split("\r\n", StringSplitOptions.TrimEntries);
        long sum = 0;
        foreach (var line in lines)
        {
            int firstIndex = 0, firstDigit = 0, secondDigit = 0;
            for (int i = 0; i < line.Length - 1; i++)
            {
                var digit = line[i] - '0';
                if (digit > firstDigit)
                {
                    (firstDigit, firstIndex) = (digit, i);
                }
            }

            for (int i = firstIndex + 1; i < line.Length; i++)
            {
                var digit = line[i] - '0';
                if (digit > secondDigit)
                {
                    secondDigit = digit;
                }
            }

            sum += (firstDigit * 10 + secondDigit);
        }
        
        return sum;
    }

    public long Part2()
    {
        var lines = ReadFile().Split("\r\n", StringSplitOptions.TrimEntries);
        long sum = 0;

        foreach (var line in lines)
        {
            var digits = line.Select(d => d - '0');
            int from = 0;
            for (int offset = 11; offset >= 0; offset--)
            {
                int to = line.Length - offset;
                var (digit, index) = this.Max(digits, from, to);

                from = index + 1;
                sum += digit * ((long)Math.Pow(10, offset));
            }

        }

        return sum;
    }

    private (int, int) Max(IEnumerable<int> s, int from, int to)
    {
        int digit = 0, index = -1;
        for (int i = from; i < to; i++)
        {
            var d = s.ElementAt(i);
            if (d > digit) (digit, index) = (d, i);
        }

        return (digit, index);
    }
}
