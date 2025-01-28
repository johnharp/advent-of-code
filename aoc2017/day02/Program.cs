string[] lines = File.ReadAllLines("input.txt");

long part1 = 0;
long part2 = 0;

foreach(string line in lines)
{
    var values = line.Split(new [] {' ', '\t' }, StringSplitOptions.RemoveEmptyEntries)
        .ToList()
        .Select(s => long.Parse(s))
        .ToList();

    long max = values.Max();
    long min = values.Min();

    part1 += max - min;

    bool found  = false;
    for (int i = 0; !found && (i < values.Count - 1); i++)
    {
        for (int j = i + 1; !found && (j < values.Count); j++)
        {
            long v1 = values[i];
            long v2 = values[j];

            if (isEvenlyDivisible(v1, v2))
            {
                part2 += Math.Max(v1, v2) /
                         Math.Min(v1, v2);
                found = true;
            }
        }
    }
}

Console.WriteLine($"Part 1: {part1}");
Console.WriteLine($"Part 2: {part2}");

bool isEvenlyDivisible(long a, long b)
{
    return (Math.Max(a, b) % Math.Min(a, b)) == 0;
}