using System;

namespace day19_2
{
    class Program
    {
        static void Main(string[] args)
        {
            PuzzleInput input = new PuzzleInput("input.txt");

            Rule r = new Rule("23: 25 1 | 22 14");
            Console.Out.WriteLine(r);
        }
    }
}
