using System.Text;

namespace day06;

class Program
{
    static void Main(string[] args)
    {
        string basedir = AppDomain.CurrentDomain.BaseDirectory;
        Directory.SetCurrentDirectory(basedir);
        string prefix = "../../../";
        string filename = "input.txt";
        //filename = "input-sample.txt";

        string[] lines = File.ReadAllLines($"{prefix}{filename}");
        List<string> linesList = lines.ToList();

        StringBuilder sb = new StringBuilder();
        StringBuilder pt2sb = new StringBuilder();
        for (int i = 0; i<lines[0].Length; i++)
        {
            char c = mostFrequent(linesList, i);
            sb.Append(c);

            c = leastFrequent(linesList, i);
            pt2sb.Append(c);
        }

        string part1 = sb.ToString();
        string part2 = pt2sb.ToString();

        Console.WriteLine($"Part 1: {part1}");
        Console.WriteLine($"Part 2: {part2}");
    }

    static char mostFrequent(List<string> lines, int index)
    {
        char mostFreqChar = lines
            .Select(l => l[index])
            .GroupBy(g => g)
            .Select(g => new { character = g.Key, count = g.Count() })
            .OrderByDescending(c => c.count)
            .Select(c => c.character)
            .First();

        return mostFreqChar;
    }

    static char leastFrequent(List<string> lines, int index)
    {
        char leastFreqChar = lines
            .Select(l => l[index])
            .GroupBy(g => g)
            .Select(g => new { character = g.Key, count = g.Count() })
            .OrderBy(c => c.count)
            .Select(c => c.character)
            .First();

        return leastFreqChar;
    }
}
