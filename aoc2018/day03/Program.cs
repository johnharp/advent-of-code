using System.Text.RegularExpressions;

var lines = File.ReadAllLines("input.txt");
var regex = new Regex(@"^#(\d+) @ (\d+),(\d+): (\d+)x(\d+)$");

Dictionary<(int, int), List<int>> fabric = new Dictionary<(int, int), List<int>>();
HashSet<int> noDups = new HashSet<int>();

foreach (var line in lines)
{
    var matches = regex.Match(line);
    if (!matches.Success) throw new Exception($"Invalid input: {line}");

    var id = int.Parse(matches.Groups[1].Value);
    var x = int.Parse(matches.Groups[2].Value);
    var y = int.Parse(matches.Groups[3].Value);
    var width = int.Parse(matches.Groups[4].Value);
    var height = int.Parse(matches.Groups[5].Value);

    noDups.Add(id);

    for (var i = x; i < x + width; i++)
    {
        for (var j = y; j < y + height; j++)
        {
            if (fabric.ContainsKey((i, j)))
            {
                noDups.RemoveWhere(fabric[(i, j)].Contains);
                noDups.RemoveWhere(id.Equals);
            }
            else
            {
                fabric[(i, j)] = new List<int> { id };
            }

            fabric[(i, j)].Add(id);
        }
    }
}
int part1 = fabric.Values.Where(v => v.Count > 1).Count();
Console.WriteLine($"Part 1: {part1}");
Console.WriteLine($"Part 2: {noDups.Single()}");

void Dump()
{
    // for each of the dictionary entries in "fabric", print out the
    // key and the list of id's at that key
    foreach (var (key, value) in fabric)
    {
        Console.WriteLine($"{key}: {string.Join(", ", value)}");
    }
}