using System.Globalization;
using System.Text;

string[] lines = File.ReadAllLines("input.txt");

int has3 = 0;
int has2 = 0;

List<List<char>> columns = new List<List<char>>();
for (int i = 0; i < lines[0].Length; i++)
{
    columns.Add(new List<char>());
}

foreach(string line in lines)
{
    var counts = line.GroupBy(c => c);
    if (counts.Any(c => c.Count() == 3)) has3++;
    if (counts.Any(c => c.Count() == 2)) has2++;

    for (int i = 0; i < line.Length; i++)
    {
        columns[i].Add(line[i]);
    }
}

Console.WriteLine($"Part 1: {has3 * has2}");


StringBuilder sb = new StringBuilder();

for (int i = 0; i < lines[0].Length; i++)
{
    List<String> words = new List<string>();
    for (int j = 0; j < lines.Length; j++)
    {
        sb.Clear();

        for (int k = 0; k < lines[0].Length; k++)
        {
            if (k == i) continue;
            sb.Append(lines[j][k]);
        }
        words.Add(sb.ToString());
    }
    
    var dups = words
        .GroupBy(g => g)
        .Where(g => g.Count() > 1)
        .Select(g => new {
            Word = g.Key,
            Count = g.Count()
        });

    if (dups.Any())
    {
        Console.WriteLine($"Part 2: {dups.First().Word} appears {dups.First().Count} times");
        break;
    }
}