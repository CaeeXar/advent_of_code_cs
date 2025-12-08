namespace AOC2025.Day02;

internal class Day02
{
    string path = @"2025\day02\Input.txt";

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
        return Ranges()
               .SelectMany(rv => rv.FindInvalidIds(isPart2: false))
               .Sum();
    }

    public long Part2()
    {
        return Ranges()
               .SelectMany(rv => rv.FindInvalidIds(isPart2: true))
               .Sum();
    }

    private IEnumerable<RangeValidator> Ranges()
    {
        return ReadFile().Split(",", StringSplitOptions.TrimEntries)
                         .Select(s =>
                         {
                             var r = s.Split("-", StringSplitOptions.TrimEntries);
                             return new RangeValidator(long.Parse(r[0]), long.Parse(r[1]));
                         });
    }
}
