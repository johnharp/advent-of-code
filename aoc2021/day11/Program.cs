using System;

namespace day11
{
    class Program
    {
        static void Main(string[] args)
        {
            string filename = "input.txt";
            PuzzleInput input = new PuzzleInput(filename);
            Map map = new Map(input.Lines);

            int numStepsToSim = 10000;
            int numFlashes = 0;
            for (int i = 0; i<numStepsToSim; i++)
            {
                int flashesThisStep = map.Step();
                numFlashes += flashesThisStep;
                if (flashesThisStep == 100)
                {
                    Console.Out.WriteLine($"All flashed on step {i + 1}");
                    break;
                }
            }

            Console.Out.WriteLine($"Total flashes after {numStepsToSim} = {numFlashes}");
        }
    }
}
