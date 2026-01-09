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
        var ranges = ParseRanges();
        var ids = ParseIds();
        long freshIngredients = 0;
        foreach (var id in ids)
        {
            foreach (var range in ranges)
            {
                if (id >= range.Item1 &&
                    id <= range.Item2)
                {
                    freshIngredients++;
                    break;
                }
            }
        }

        return freshIngredients;
    }

    public long Part2()
    {
        var ranges = ParseRanges();
        HashSet<(long, long)> mergedRanges = new HashSet<(long, long)>();

        for (int i = 0; i < ranges.Count; i++)
        {
            bool isMerged = false; 
            for (int j = 0; j < ranges.Count; j++)
            {
                if (i == j) continue;
                if (ranges[i].TryMerge(ranges[j], out var merged))
                {
                    mergedRanges.Add(merged);
                    Console.WriteLine($"merged {ranges[i]} with {ranges[j]} to {merged}");
                    isMerged = true;
                    ranges.Add(merged);
                }
            }

            if (!isMerged) mergedRanges.Add(ranges[i]);
        }

        Console.WriteLine(string.Join(", ", mergedRanges));

        return 0;
    }

    private List<(long, long)> ParseRanges()
    {
        return ReadFile().Split("\r\n\r\n", StringSplitOptions.RemoveEmptyEntries)[0]
            .Split("\r\n", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Select(line =>
            {
                var parts = line.Split('-');
                long v1 = long.Parse(parts[0]);
                long v2 = long.Parse(parts[1]);
                return (Math.Min(v1, v2), Math.Max(v1, v2));
            })
            .ToList();
    }

    private List<long> ParseIds()
    {
        return ReadFile().Split("\r\n\r\n", StringSplitOptions.RemoveEmptyEntries)[1]
            .Split("\r\n", StringSplitOptions.TrimEntries)
            .Select(long.Parse)
            .ToList();
    }
}

public static class RangeExtensions
{
    public static bool TryMerge(this (long Start, long End) a, (long Start, long End) b, out (long Start, long End) merged)
    {
        // 1. Sortieren (Wer ist links, wer rechts?)
        var (left, right) = (a.Start <= b.Start) ? (a, b) : (b, a);

        // 2. Prüfen (Überlappung oder direkt angrenzend +1)
        if (right.Start <= left.End + 1)
        {
            merged = (left.Start, Math.Max(left.End, right.End));
            return true;
        }

        merged = default;
        return false;
    }
}