using System.Diagnostics;

string[] lines = File.ReadAllLines("input.txt");
Debug.Assert(lines.Length == 1, "Only one line of input expected");

string line = lines[0];

long part1 = 0;
long part2 = 0;

for (int i = 0; i<line.Length; i++)
{
    char curr = line[i];
    char next = line[(i+1) % line.Length];
    char halfway = line[(i + line.Length/2) % line.Length];

    int value = int.Parse(curr.ToString());

    if (curr == next)
    {
        part1 += value;
    }

    if (curr == halfway)
    {
        part2 += value;
    }
}

Console.WriteLine($"Part 1: {part1}");
Console.WriteLine($"Part 2: {part2}");