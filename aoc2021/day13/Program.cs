using System;

namespace day13
{
    class Program
    {
        static void Main(string[] args)
        {
            //string filename = "input-example.txt";
            string filename = "input.txt";

            PuzzleInput input = new PuzzleInput(filename);
            DotTracker tracker = new DotTracker();
            tracker.CreateDots(input);


            // Part 1
            //tracker.DoFold(input.FoldLines[0]);
            //tracker.DumpDotCount();

            // Part 2
            tracker.DoFolds(input);
            tracker.Print();
        }
    }
}
