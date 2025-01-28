using System;

namespace day07
{
    class Program
    {
        static void Main(string[] args)
        {
            String filename = "input.txt";
            PuzzleInput input = new PuzzleInput(filename);

            Tally t = new Tally(input.Lines);
            long minFuel = long.MaxValue;

            for (long i = 0; i<= t.MaxValue; i++)
            {
                long fuel = t.FuelToAlignToPositionPart2(i);
                if (fuel < minFuel) minFuel = fuel;
                Console.Out.WriteLine($"{i} => {fuel}");
            }

            Console.Out.WriteLine($"Min Fuel: {minFuel}");
        }
    }
}
