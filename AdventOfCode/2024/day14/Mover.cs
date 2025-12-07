using System.Drawing;

namespace AOC2024.Day14;

internal class Mover
{
    public enum Movement
    {
        UP = '^',
        DOWN = 'v',
        LEFT = '<',
        RIGHT = '>',
    };

    public Queue<Movement> Movements = new Queue<Movement>();

    public static Mover Parse(string input)
    {
        var mover = new Mover();
        for (int i = 0; i < input.Length; i++)
        {
            mover.Movements.Enqueue((Movement)input[i]);
        }

        return mover;
    }

    public void Move(Warehouse warehouse)
    {
        if (Movements.Count == 0)
        {
            return;
        }

        var move = Movements.Dequeue();
        switch(move)
        {
            case Movement.UP:
                MoveUp(warehouse);
                break;
            case Movement.DOWN:
                MoveDown(warehouse);
                break;
            case Movement.LEFT:
                MoveLeft(warehouse);
                break;
            case Movement.RIGHT:
                MoveRight(warehouse);
                break;
        }
    }

    private void MoveDown(Warehouse warehouse)
    {
        var pos = new Point(warehouse.RobotPosition.X, warehouse.RobotPosition.Y + 1);
        if (pos.Y >= warehouse.Dimension.Height || ContainsWall(warehouse, pos))
        {
            return;
        }


        if (!ContainsBox(warehouse, pos))
        {
            warehouse.RobotPosition = pos;
            return;
        }

        int y = pos.Y;
        while (ContainsBox(warehouse, pos.X, y))
        {
            y++;
            if (ContainsWall(warehouse, pos.X, y)) return;
        }

        warehouse.Boxes.Remove(pos);
        warehouse.Boxes.Add(new Point(pos.X, y));
        warehouse.RobotPosition = pos;
    }

    private void MoveUp(Warehouse warehouse)
    {
        var pos = new Point(warehouse.RobotPosition.X, warehouse.RobotPosition.Y - 1);
        if (pos.Y < 0 || ContainsWall(warehouse, pos))
        {
            return;
        }


        if (!ContainsBox(warehouse, pos))
        {
            warehouse.RobotPosition = pos;
            return;
        }

        int y = pos.Y;
        while (ContainsBox(warehouse, pos.X, y))
        {
            y--;
            if (ContainsWall(warehouse, pos.X, y)) return;
        }

        warehouse.Boxes.Remove(pos);
        warehouse.Boxes.Add(new Point(pos.X, y));
        warehouse.RobotPosition = pos;
    }

    private void MoveLeft(Warehouse warehouse)
    {
        var pos = new Point(warehouse.RobotPosition.X - 1, warehouse.RobotPosition.Y);
        if (pos.X < 0 || ContainsWall(warehouse, pos))
        {
            return;
        }


        if (!ContainsBox(warehouse, pos))
        {
            warehouse.RobotPosition = pos;
            return;
        }

        int x = pos.X;
        while (ContainsBox(warehouse, x, pos.Y))
        {
            x--;
            if (ContainsWall(warehouse, x, pos.Y)) return;
        }

        warehouse.Boxes.Remove(pos);
        warehouse.Boxes.Add(new Point(x, pos.Y));
        warehouse.RobotPosition = pos;
    }

    private void MoveRight(Warehouse warehouse)
    {
        var pos = new Point(warehouse.RobotPosition.X + 1, warehouse.RobotPosition.Y);
        if (pos.X >= warehouse.Dimension.Width || ContainsWall(warehouse, pos))
        {
            return;
        }

        if (!ContainsBox(warehouse, pos))
        {
            warehouse.RobotPosition = pos;
            return;
        }

        int x = pos.X;
        while (ContainsBox(warehouse, x, pos.Y))
        {
            x++;
            if (ContainsWall(warehouse, x, pos.Y)) return;
        }

        warehouse.Boxes.Remove(pos);
        warehouse.Boxes.Add(new Point(x, pos.Y));
        warehouse.RobotPosition = pos;
    }

    private bool ContainsWall(Warehouse w, int x, int y)
    {
        return w.Walls.Contains(new Point(x, y));
    }

    private bool ContainsWall(Warehouse w, Point p)
    {
        return w.Walls.Contains(p);
    }

    private bool ContainsBox(Warehouse w, int x, int y)
    {
        return w.Boxes.Contains(new Point(x, y));
    }

    private bool ContainsBox(Warehouse w, Point p)
    {
        return w.Boxes.Contains(p);
    }
}
