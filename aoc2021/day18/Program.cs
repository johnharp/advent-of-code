using System;

namespace day18
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = new PuzzleInput("input.txt");
            Part2(input);
        }

        static void Part1(PuzzleInput input)
        {
            var calc = new Calculator();

            Element sum = Element.CreateElementFromString(input.Lines[0]);

            for (int i = 1; i < input.Lines.Length; i++)
            {
                Element a = Element.CreateElementFromString(input.Lines[i]);

                sum = calc.Add(sum, a);
                calc.Reduce(sum);
                Console.WriteLine(sum);
            }

            Console.Write("Magnitude: ");
            Console.Write(sum.Magnitude());
            Console.WriteLine();

        }

        static void Part2(PuzzleInput input)
        {
            var calc = new Calculator();
            int maxMagnitude = int.MinValue;

            for(int i = 0; i < input.Lines.Length; i++)
            {
                for (int j = 0; j < input.Lines.Length; j++)
                {
                    if (i == j) continue;

                    Element a = Element.CreateElementFromString(input.Lines[i]);
                    Element b = Element.CreateElementFromString(input.Lines[j]);

                    Element sum = calc.Add(a, b);
                    calc.Reduce(sum);
                    int magnitude = sum.Magnitude();

                    if (magnitude > maxMagnitude) maxMagnitude = magnitude;
                }
            }

            Console.WriteLine($"Max Magnitude = {maxMagnitude}");
        }

    }
}
