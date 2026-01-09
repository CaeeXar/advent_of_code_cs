namespace AOC2025.Day05;

internal class Day05
{
    string path = @"2025\day05\Input.txt";

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
        return 0;
    }

    public long Part2()
    {
        return 0;
    }
}
