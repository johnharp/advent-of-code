using System;

namespace day02
{
    class Program
    {
        static void Main(string[] args)
        {
            PuzzleInput input = new PuzzleInput();
            var steps = input.PartA();

            Nav nav = new Nav();

            foreach (var step in steps)
            {
                nav.ApplyNavStep(step);
            }

            Console.Out.WriteLine($"Depth: {nav.Depth}");
            Console.Out.WriteLine($"Horizontal Position: {nav.HorizontalPosition}");
            Console.Out.WriteLine($"Product: {nav.Depth * nav.HorizontalPosition}");
        }
    }
}
