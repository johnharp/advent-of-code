int input = 265149;
//input = 1024;
(int, int) pos = (0, 0);

long part2 = -1;

Dictionary<(int, int), long> values = new Dictionary<(int, int), long>();

List<(int, int)> directions = new List<(int, int)>
{
    (1, 0), (0, 1), (-1, 0), (0, -1)
};

int leg = 0;
int address = 1;

values.Add(pos, 1);

while (address < input)
{
    (int, int) direction = directions[leg % 4];
    int numSteps = leg / 2 + 1;

    int step = 0;
    while (address < input && step < numSteps)
    {
        address++;
        pos = (pos.Item1 + direction.Item1, pos.Item2 + direction.Item2);
        step++;
        long value = sumNeighbors(pos);
        values[pos] = value;

        if (part2 == -1 && value > input)
        {
            part2 = value;
        }
    }
    leg++;
}



long sumNeighbors((int, int) p)
{
    return getValue((p.Item1 + 1, p.Item2)) +
        getValue((p.Item1 + 1, p.Item2 + 1)) +
        getValue((p.Item1, p.Item2 + 1)) +
        getValue((p.Item1 - 1, p.Item2 + 1)) +
        getValue((p.Item1 - 1, p.Item2)) +
        getValue((p.Item1 - 1, p.Item2 - 1)) +
        getValue((p.Item1, p.Item2 - 1)) + 
        getValue((p.Item1 + 1, p.Item2 - 1));
}

long getValue((int, int) p)
{
    if (values.ContainsKey(p)) return values[p];
    else return 0;
}

long part1 = Math.Abs(pos.Item1) + Math.Abs(pos.Item2);
Console.WriteLine($"Part 1: {part1}");

Console.WriteLine($"Part 2: {part2}");