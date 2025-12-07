namespace AOC2024.Day14;

internal class Day14
{
    string path = @"2024\day14\Input.txt";

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
        var input = ReadFile();
        var parts = input.Split("\r\n\r\n", StringSplitOptions.TrimEntries);
        
        Warehouse warehouse = Warehouse.Parse(parts[0]);
        Mover mover = Mover.Parse(parts[1]);
        while (mover.Movements.Count > 0)
        {
            mover.Move(warehouse);
        }

        //warehouse.DebugPrint();        

        return warehouse.Boxes.Sum(p => p.X + 100 * p.Y); 
    }

    public long Part2()
    {
        var input = ReadFile();
        var parts = input.Split("\r\n\r\n", StringSplitOptions.TrimEntries);

        Warehouse warehouse = Warehouse.ParseDouble(parts[0]);
        Mover mover = Mover.Parse(parts[1]);
        while (mover.Movements.Count > 0)
        {
            mover.Move(warehouse);
        }

        warehouse.DebugPrint();

        return warehouse.Boxes.Sum(p => p.X + 100 * p.Y);
    }
}
