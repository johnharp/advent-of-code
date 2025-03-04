using System.Data;
using System.Runtime.CompilerServices;

namespace day10;

class Program
{
  // data is a dictionary keyed with (x, y) position and value of map height
  // at that position.
  static Dictionary<(int, int), int> data = new Dictionary<(int, int), int>();

  // neighbors is a dictionary keyed with (x, y) position and a value that is
  // a list of (x, y) positions that are valid neighbors (valid means that it
  // is a move left, right, up, or down with an increase of 1 in height for
  // uphill and decrease of 1 for downhill).
  static Dictionary<(int, int), List<(int, int)>> upHillNeighbors = new Dictionary<(int, int), List<(int, int)>>();
  static Dictionary<(int, int), List<(int, int)>> downHillNeighbors = new Dictionary<(int, int), List<(int, int)>>();


  // The heights dictionary has key values of 0 - 9, each value is a list of the
  // (x, y) positions of the map that have that height.
  static Dictionary<int, List<(int, int)>> heights = new Dictionary<int, List<(int, int)>>();

  static Dictionary<(int, int), List<List<(int, int)>>> paths = new Dictionary<(int, int), List<List<(int, int)>>>();



  static int WIDTH = -1;
  static int HEIGHT = -1;

  static void Main(string[] args)
  {
    var lines = File.ReadAllLines("./input.txt");
    WIDTH = lines[0].Length;
    HEIGHT = lines.Length;

    for (int y = 0; y < HEIGHT; y++)
    {
      string line = lines[y];
      for (int x = 0; x < WIDTH; x++)
      {
        char c = line[x];
        int v = int.Parse(c.ToString());

        if (!heights.ContainsKey(v)) heights[v] = new List<(int, int)>();
        heights[v].Add((x, y));

        data[(x, y)] = v;
      }
    }


    // neighbors is a dictionary of (x, y) where each value is a list
    // of the neighbors of that position that increase (step upward)
    // by one value.
    for (int y = 0; y < HEIGHT; y++)
    {
      for (int x = 0; x < WIDTH; x++)
      {
        upHillNeighbors[(x, y)] = new List<(int, int)>();
        downHillNeighbors[(x, y)] = new List<(int, int)>();

        var pos = (x, y);
        var value = get(pos);

        var tryNeighbor = ((int, int) p) =>
        {
          if (get(p) == value + 1)
          {
            upHillNeighbors[(x, y)].Add(p);
          } else if (get(p) == value - 1)
          {
            downHillNeighbors[(x, y)].Add(p);
          }
        };

        tryNeighbor((x, y - 1));
        tryNeighbor((x, y + 1));
        tryNeighbor((x - 1, y));
        tryNeighbor((x + 1, y));
      }
    }

    for (int h = 9; h >=0; h--)
    {
      foreach (var pos in heights[h])
      {
        paths[pos] = new List<List<(int, int)>>();

        if (upHillNeighbors[pos].Count == 0)
        {
          paths[pos].Add(new List<(int, int)>() { pos });
          continue;
        }

        foreach (var neighbor in upHillNeighbors[pos])
        {
          // Because we are starting at highest points, and going backward,
          // we are guaranteed that any position in the uphill neighbor list
          // will already have it's paths calculated.
          foreach (var path in paths[neighbor])
          {
            var newPath = new List<(int, int)>(path);
            newPath.Add(pos);
            paths[pos].Add(newPath);
          }
        }
      }
    }

    long sum = 0;
    foreach(var p in heights[0])
    {
      HashSet<(int, int)> peaks = new HashSet<(int, int)>();
      foreach (var path in paths[p])
      {
        if (path.Count == 10) {
          peaks.Add(path[0]);
        }
      }

      sum += peaks.Count;
      Console.WriteLine($"Trailhead at {p} has {peaks.Count} peaks");
    }

    Console.WriteLine($"Sum: {sum}");
  }

  static int get((int, int) pos)
  {
    if (pos.Item1 < 0 || pos.Item1 >= WIDTH || pos.Item2 < 0 || pos.Item2 >= HEIGHT)
    {
      return int.MinValue;
    }
    else
    {
      return data[(pos.Item1, pos.Item2)];
    }
  }
}
