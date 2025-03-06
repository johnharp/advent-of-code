namespace day11;

class Program
{

  // Improve performance by memoizing 
  static Dictionary<string, List<string>> memo = new Dictionary<string, List<string>>();



  static void Main(string[] args)
  {
    var input = System.IO.File.ReadAllText("input.txt").Split(' ');

    long part1 = 0;
    long part2 = 0;

    var stoneCounts = new Dictionary<string, long>();
    foreach (var s in input)
    {
      if (stoneCounts.ContainsKey(s))
      {
        stoneCounts[s]++;
      }
      else
      {
        stoneCounts[s] = 1;
      }
    }

    for (int i = 0; i<75; i++)
    {
      var newStoneCounts = new Dictionary<string, long>();
      foreach (var kvp in stoneCounts)
      {
        var results = Calc(kvp.Key);
        foreach (var r in results)
        {
          if (newStoneCounts.ContainsKey(r))
          {
            newStoneCounts[r] += kvp.Value;
          }
          else
          {
            newStoneCounts[r] = kvp.Value;
          }
        }
      }

      if (i == 24)
      {
        part1 = Count(newStoneCounts);
      }
      
      if (i==74)
      {
        part2 = Count(newStoneCounts);
      }

      stoneCounts = newStoneCounts;
    }

    Console.WriteLine($"****** Part 1: {part1}");
    Console.WriteLine($"****** Part 2: {part2}");
  }

  static long Count(Dictionary<string, long> stoneCounts)
  {
    long count = 0;
    foreach (var kvp in stoneCounts)
    {
      count += kvp.Value;
    }
    return count;
  }

  static List<string> Calc(string s)
  {
    List<string> results = new List<string>();

    if (s == "0")
    {
      results.Add("1");
    }
    else if (isEvenLength(s))
    {
      int splitstart = s.Length / 2;
      string v1 = s.Substring(0, splitstart);
      string v2 = s.Substring(splitstart);
      results.Add(normalize(v1));
      results.Add(normalize(v2));
    }
    else
    {
      long v = long.Parse(s) * 2024;
      results.Add(v.ToString());
    }

    return results;
  }


  static string normalize(string s)
  {
    return long.Parse(s).ToString();
  }

  static bool isEvenLength(string s)
  {
    return s.Length % 2 == 0;
  }
}
