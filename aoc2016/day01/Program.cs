using System.Diagnostics;
using System.Collections.Generic;
using System.Dynamic;

namespace day01;

class Program
{
    // All tuples are (X, Y), with X as the horizontal axiss and Y vertical
    // Therefore start facing direction is (0, 1) straight up.
    // Starting position is (0, 0)
    static (int, int) facing = (0, 1);
    static (int, int) position = (0, 0);

    static int part1 = -1;
    static int part2 = -1;


    static HashSet<(int, int)> visitedPositions = new HashSet<(int, int)>();

    static void Main(string[] args)
    {
        string basedir = AppDomain.CurrentDomain.BaseDirectory;
        Directory.SetCurrentDirectory(basedir);
        string prefix = "../../../";
        string filename = "input.txt";
        string[] lines = File.ReadAllLines($"{prefix}{filename}");
        Debug.Assert(lines.Length == 1, "Bad input -- only one line expected");

        var steps = lines[0]
            .Split(", ")
            .Select(step => new
            {
                turn = step.Substring(0, 1),
                dist = int.Parse(step.Substring(1))
            });

        visitedPositions.Add(position);
        foreach (var step in steps)
        {
            facing = rotate(facing, step.turn);
            position = move(position, facing, step.dist);
        }

        part1 = distanceFromStart(position);
        Console.WriteLine($"Part 1: {part1}");
        Console.WriteLine($"Part 2: {part2}");
    }

    private static int distanceFromStart((int, int) pos)
    {
        return Math.Abs(pos.Item1) + Math.Abs(pos.Item2);
    }

    private static (int, int) rotate((int, int) dir, string leftOrRight)
    {
        switch (leftOrRight)
        {
            case "L":
                // counter-clockwise
                return (-dir.Item2, dir.Item1);
            case "R":
                // clockwise
                return (dir.Item2, -dir.Item1);
            default:
                throw new Exception($"Unsupported direction: {leftOrRight}");
        }
    }

    private static (int, int) move((int, int) pos, (int, int) facing, int dist)
    {
        (int, int) p = pos;
        for (int i = 0; i < dist; i++)
        {
            p = (p.Item1 + facing.Item1, p.Item2 + facing.Item2);
            if (visitedPositions.Contains(p))
            {
                if (part2 == -1) part2 = distanceFromStart(p);
            }
            else
            {
                visitedPositions.Add(p);
            }
        }
        return p;
    }
}
