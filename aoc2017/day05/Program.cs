var memory = File.ReadAllLines("input.txt")
    .Select(l => int.Parse(l))
    .ToList();

Console.WriteLine($"Part 1: {part1(memory)}");
Console.WriteLine($"Part 2: {part2(memory)}");


long part1(List<int> startingMemory)
{
    int pc = 0;
    var mem = new List<int>(startingMemory);
    int maxAddress = mem.Count - 1;

    long numSteps = 0;

    while (pc <= maxAddress)
    {
        numSteps++;
        int v = mem[pc];
        mem[pc]++;

        pc = pc += v;
    }

    return numSteps;
}

long part2(List<int> startingMemory)
{
    int pc = 0;
    var mem = new List<int>(startingMemory);
    int maxAddress = mem.Count - 1;

    long numSteps = 0;

    while (pc <= maxAddress)
    {
        numSteps++;
        int v = mem[pc];
        if (v >= 3) mem[pc]--;
        else mem[pc]++;

        pc = pc += v;
    }

    return numSteps;
}

// void Dump(List<int> mem, int pc)
// {
//     for (int i = 0; i < mem.Count; i++)
//     {
//         if (i == pc) Console.Write($"({mem[i]})");
//         else Console.Write($"{mem[i]}");

//         if (i != mem.Count - 1) Console.Write("  ");
//     }
//     Console.WriteLine();
// }