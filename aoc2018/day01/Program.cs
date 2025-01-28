List<long> values = File.ReadAllLines("input.txt")
    .Select(l => long.Parse(l))
    .ToList();

long part1 = values
    .Aggregate((long) 0, (acc, v) => acc + v);

var frequencies = new HashSet<long>();

long frequency = 0;
frequencies.Add(frequency);
bool foundDup = false;
while (!foundDup)
{
    foreach(long v in values)
    {
        frequency += v;
        if (frequencies.Contains(frequency))
        {
            foundDup = true;
            break;
        }
        else
        {
            frequencies.Add(frequency);
        }
    }
}

Console.WriteLine($"Part 1: {part1}");
Console.WriteLine($"Part 2: {frequency}");