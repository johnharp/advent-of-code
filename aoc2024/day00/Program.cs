namespace day01;

class Program
{
    static void Main(string[] args)
    {
        Solver solver = new Solver();
        string[] filenames = { "input-sample.txt", "input.txt" };

        foreach (string filename in filenames)
        {
            execute(solver, filename);
        }
    }

    static void execute(Solver solver, string filename)
    {
        string prefix = "-- ";
        string suffix = " ";
        int dividerLength = 40 - filename.Length - prefix.Length - suffix.Length;
        string divider = new string('-', dividerLength);
        Console.WriteLine($"{prefix}{filename}{suffix}{divider}");

        string[] lines = File.ReadAllLines(filename);

        var part1 = solver.part1(lines);
        var part2 = solver.part2(lines);

        Console.WriteLine($"part1: {part1}");
        Console.WriteLine($"part2: {part2}");
        Console.WriteLine();

    }

}