using System;

namespace day03
{
    class Program
    {
        static void Main(string[] args)
        {
            RunExample();
        }

        static void RunExample()
        {
            PuzzleInput input = new PuzzleInput();
            var lines = input.Input();

            Tally t = new Tally(lines);

            Console.Out.WriteLine($"Gamma = {t.gamma}");
            Console.Out.WriteLine($"Epsilon = {t.epsilon}");
            Console.Out.WriteLine($"Their product = {t.gamma * t.epsilon}");

            Console.Out.WriteLine();

            for(int i = 0; i < t.lineLength; i++)
            {
                t.RemoveInvalidOxGenRatingLinesAtBitPos(i);
                if (t.numLines == 1) break;
            }

            var oxGenValue = t.TallyLines[0].intValue;
            Console.Out.WriteLine($"Oxygen Gen Rating = {oxGenValue}");

            t = new Tally(lines);
            for (int i = 0; i < t.lineLength; i++)
            {
                t.RemoveInvalidCo2ScrubRatingLinesAtBitPos(i);
                if (t.numLines == 1) break;
            }
            var co2ScrubValue = t.TallyLines[0].intValue;
            Console.Out.WriteLine($"CO2 Scrubber Rating = {co2ScrubValue}");
            Console.Out.WriteLine($"Their product = {oxGenValue * co2ScrubValue}");


        }
    }
}
