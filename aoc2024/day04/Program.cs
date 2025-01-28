using System.Linq.Expressions;

namespace day04;

class Program
{
    static void Main(string[] args)
    {
        string[] filenames = { "input-sample.txt", "input.txt" };

        foreach (string filename in filenames)
        {
            execute(filename);
        }
    }

    static void execute(string filename)
    {
        string prefix = "-- ";
        string suffix = " ";
        int dividerLength = 40 - filename.Length - prefix.Length - suffix.Length;
        string divider = new string('-', dividerLength);
        Console.WriteLine($"{prefix}{filename}{suffix}{divider}");

        string[] lines;

        try
        {
            lines = File.ReadAllLines(filename);
            Solver solver = new Solver(lines);

            var part1 = solver.part1();
            var part2 = solver.part2();

            Console.WriteLine($"part1: {part1}");
            Console.WriteLine($"part2: {part2}");
            Console.WriteLine();
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine(ex.Message);
        }

    }

}