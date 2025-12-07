using System.Drawing;

namespace AOC2024.Day14;

internal class Warehouse
{
    public List<Point> Walls = new List<Point>();
    public List<Point> Boxes = new List<Point>();
    public Point RobotPosition = new Point();
    public Size Dimension;

    public const char WALL = '#';
    public const char BOX = 'O';
    public const char ROBOT = '@';

    public static Warehouse Parse(string input)
    {
        var warehouse = new Warehouse();
        var map = input.Split("\r\n", StringSplitOptions.TrimEntries);
        warehouse.Dimension = new Size(map[0].Length, map.Length);

        for (int y = 0; y < map.Length; y++)
        {
            for (int x = 0; x < map[y].ToCharArray().Length; x++)
            {
                var item = map[y][x];
                if (item == WALL) warehouse.Walls.Add(new Point(x, y));
                else if (item == BOX) warehouse.Boxes.Add(new Point(x, y));
                else if (item == ROBOT) warehouse.RobotPosition = new Point(x, y);
            }
        }

        return warehouse;
    }

    public static Warehouse ParseDouble(string input)
    {
        // man muss vermutlich mit ranges arbeiten, statt punkten...
        var warehouse = new Warehouse();
        var map = input.Split("\r\n", StringSplitOptions.TrimEntries);
        warehouse.Dimension = new Size(map[0].Length * 2, map.Length);

        for (int y = 0; y < map.Length; y++)
        {
            for (int x = 0; x < map[y].ToCharArray().Length; x++)
            {
                var item = map[y][x];
                if (item == WALL)
                {
                    warehouse.Walls.Add(new Point(2 * x, y));
                    warehouse.Walls.Add(new Point(2 * x + 1, y));
                }
                else if (item == BOX)
                {
                    warehouse.Boxes.Add(new Point(2 * x, y));
                    warehouse.Boxes.Add(new Point(2 * x + 1, y));
                }
                else if (item == ROBOT)
                {
                    warehouse.RobotPosition = new Point(2 * x, y);
                }
            }
        }

        return warehouse;
    }

    public void DebugPrint()
    {
        
            for (int y = 0; y < Dimension.Height; y++)
            {
            for (int x = 0; x < Dimension.Width; x++)
            {
                if (y == 0 || x == 0 || 
                    y == Dimension.Height - 1 || x == Dimension.Width - 1)
                { 
                    Console.Write(WALL);
                }
                else if (Boxes.Contains(new Point(x, y))) Console.Write(BOX);
                else if (Walls.Contains(new Point(x, y))) Console.Write(WALL);
                else if (RobotPosition == new Point(x, y)) Console.Write(ROBOT);
                else Console.Write('.');
            }

            Console.WriteLine();
        }
    }
}
