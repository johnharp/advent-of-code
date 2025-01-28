using System;

namespace day14
{
    class Program
    {
        static void Main(string[] args)
        {
            //string filename = "input-example.txt";
            string filename = "input.txt";

            PuzzleInput input = new PuzzleInput(filename);

            //Part1(input);
            Part2(input);

 
        }

        public static void Part1(PuzzleInput input)
        {
            ElementTracker tracker = new ElementTracker();
            tracker.InitializeRules(input.InsertionRules);
            tracker.InitializeElements(input.InitialElements);

            for (int i = 1; i<=10; i++)
            {
                tracker.ApplyRulesToElements();
                Console.WriteLine($"After iteration {i}");
            }
            tracker.DumpStatistics();
        }

        public static void Part2(PuzzleInput input)
        {
            var tracker = new ElementTrackerV2();
            tracker.InitializeRules(input.InsertionRules);
            tracker.InitializeElements(input.InitialElements);

            for (int i = 1; i <= 40; i++)
            {
                tracker.ApplyRulesToElements();
                Console.WriteLine($"After iteration {i}");
            }
            tracker.DumpStatistics();
        }
    }
}
