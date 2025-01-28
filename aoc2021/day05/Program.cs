using System;

namespace day05
{
    class Program
    {
        static void Main(string[] args)
        {
            PuzzleInput input = new PuzzleInput("input.txt");

            Map m = new Map(input.MaxX, input.MaxY);
            foreach (var seg in input.Segments)
            {
                m.AddSegment(seg);
            }

            Console.Out.WriteLine(
                $"Number greater than 1 = {m.CountValuesGreaterThan(1)}");

        }
    }
}
