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
        return this.Solve(batteries: 2);
    }

    public long Part2()
    {
        return this.Solve(batteries: 12);
    }

    private long Solve(int batteries)
    {
        var lines = ReadFile().Split("\r\n", StringSplitOptions.TrimEntries);
        long sum = 0;

        foreach (var line in lines)
        {
            var digits = line.Select(d => d - '0');
            int from = 0;
            for (int exp = batteries - 1; exp >= 0; exp--)
            {
                int to = line.Length - exp;
                var (digit, index) = this.Max(digits, from, to);

                from = index + 1;
                sum += digit * ((long)Math.Pow(10, exp));
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
