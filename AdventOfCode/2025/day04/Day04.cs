namespace AOC2025.Day04;

internal class Day04
{
    string path = @"2025\day04\Input.txt";

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

    private static readonly (int dx, int dy)[] Directions =
    [
        (-1, -1), (0, -1), (1, -1),
        (-1,  0),          (1,  0),
        (-1,  1), (0,  1), (1,  1)
    ];

    public long Part1()
    {
        var rolls = ParseInput();
        return rolls.Count(roll => CountNeighbors(roll, rolls) < 4);
    }

    public long Part2()
    {
        var rolls = ParseInput();
        long totalRemoved = 0;
        while (true)
        {
            var toRemove = rolls
                .Where(roll => CountNeighbors(roll, rolls) < 4)
                .ToList();

            if (toRemove.Count == 0) break;

            totalRemoved += toRemove.Count;
            foreach (var item in toRemove)
            {
                rolls.Remove(item);
            }
        }

        return totalRemoved;
    }

    private int CountNeighbors((int x, int y) pos, HashSet<(int, int)> grid)
    {
        int count = 0;
        foreach (var (dx, dy) in Directions)
        {
            if (grid.Contains((pos.x + dx, pos.y + dy)))
            {
                count++;
            }
        }

        return count;
    }

    private HashSet<(int x, int y)> ParseInput()
    {
        return ReadFile()
            .Split("\r\n", StringSplitOptions.TrimEntries)
            .SelectMany((line, y) => line.Select((c, x) => (c, x, y)))  // Flatten grid
            .Where(t => t.c == '@')                                     // Filter '@'
            .Select(t => (t.x, t.y))                                    // Map to Point-Tuple
            .ToHashSet();
    }
}
