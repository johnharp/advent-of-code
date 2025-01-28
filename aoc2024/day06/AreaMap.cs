using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Transactions;
using System.Windows.Markup;

namespace day06;

public class AreaMap
{
    public HashSet<(int, int)> VisitedLocs = new HashSet<(int, int)> ();
    public HashSet<(int, int)> Obstructions = new HashSet<(int, int)>();
    public List<(int, int)> Path = new List<(int, int)>();
    public HashSet<string> LocDirSet = new HashSet<string> ();

    public int Width = 0;
    public int Height = 0;

    public (int, int) Loc;
    public (int, int) Dir;

    public bool LoopDetected = false;


    public AreaMap(string filename)
    {
        string[] lines = File.ReadAllLines(filename);
        char[] directionChars = { '^', '>', 'v', '<' };
        if (lines.Length > 0)
        {
            Width = lines[0].Length;
        }
        Height = lines.Length;

        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                char c = lines[y][x];
                if (c == '#') Obstructions.Add((x, y));

                if (directionChars.Contains(c)) Loc = (x, y);
                if (c == '^') Dir = (0, -1);
                if (c == '>') Dir = (1, 0);
                if (c == 'v') Dir = (0, 1);
                if (c == '<') Dir = (-1, 0);
            }
        }
    }

    public AreaMap(AreaMap d)
    {
        Obstructions = new HashSet<(int, int)>(d.Obstructions);
        VisitedLocs = new HashSet<(int, int)>(d.VisitedLocs);
        Path = new List<(int, int)>(d.Path);
        LocDirSet = new HashSet<string>(d.LocDirSet);
        Width = d.Width;
        Height = d.Height;
        Loc = d.Loc;
        Dir = d.Dir;
        LoopDetected = d.LoopDetected;
    }

    public bool IsBlocked()
    {
        (int, int) newLoc = (Loc.Item1 + Dir.Item1, Loc.Item2 + Dir.Item2);
        return Obstructions.Contains(newLoc);
    }

    public void TurnRight()
    {
        if (Dir.Item1 == 0 && Dir.Item2 == -1) Dir = (1, 0);
        else if (Dir.Item1 == 1 && Dir.Item2 == 0) Dir = (0, 1);
        else if (Dir.Item1 == 0 && Dir.Item2 == 1) Dir = (-1, 0);
        else if (Dir.Item1 == -1 && Dir.Item2 == 0) Dir = (0, -1);
    }

    public void Move()
    {
        while (IsBlocked())
        {
            TurnRight();
        }

        VisitedLocs.Add(Loc);
        Path.Add(Loc);
        string key = LocDirKey();
        if (LocDirSet.Contains(key)) LoopDetected = true;
        else LocDirSet.Add(key);

        (int, int) newLoc = (Loc.Item1 + Dir.Item1, Loc.Item2 + Dir.Item2);
        Loc = newLoc;
    }

    public bool IsOnTheMap()
    {
        return Loc.Item1 >= 0 && Loc.Item1 < Width &&
            Loc.Item2 >= 0 && Loc.Item2 < Height;
    }

    public string LocDirKey()
    {
        return $"{Loc}|{Dir}";
    }
}