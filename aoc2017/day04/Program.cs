string[] lines = File.ReadAllLines("input.txt");


// C# equivalent to Javascript .reduce() is .Aggregate()
int part1 = lines
    .Aggregate(0, (acc, x) => acc + (isPart1ValidPassphrase(x) ? 1 : 0));

int part2 = lines
    .Aggregate(0, (acc, x) => acc + (isPart2ValidPassphrase(x) ? 1 : 0));


Console.WriteLine($"Part 1: {part1}");
Console.WriteLine($"Part 2: {part2}");


bool isPart1ValidPassphrase(string line)
{
    string[] parts = line.Split(new [] { ' ', '\t'});
    HashSet<string> words = new HashSet<string>();

    foreach(var part in parts)
    {
        if (words.Contains(part))
        {
            return false;
        }
        else
        {
            words.Add(part);
        }
    }

    return true;
}

bool isPart2ValidPassphrase(string line)
{
    var counts = line.Split(new [] { ' ', '\t'})
        .Select(doCharCount)
        .ToList();

    for (int i = 0; i < counts.Count - 1; i++)
    {
        for (int j = i+1; j < counts.Count; j++)
        {
            if (countsAreSame(counts[i], counts[j])) return false;
        }
    }

    return true;
}

List<(char, int)> doCharCount(string word)
{
    return word
        .GroupBy(l => l)
        .Select(g => (g.Key, g.Count()))
        .OrderBy(i => i.Item1)
        .ToList();
}

bool countsAreSame(List<(char, int)> c1, List<(char, int)> c2)
{
    // For the counts to be the same, they must have the same number
    // of unique characters
    if (c1.Count() != c2.Count) return false;

    // Also, the list of characters must be the same
    string c1chars = String.Join("", c1.Select(i => i.Item1));
    string c2chars = String.Join("", c2.Select(i => i.Item1));
    if (c1chars != c2chars) return false;

    for (int i = 0; i < c1.Count; i++)
    {
        if (c1[i].Item2 != c2[i].Item2) return false;
    }

    return true;
}