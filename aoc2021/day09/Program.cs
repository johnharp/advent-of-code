using System;
using System.Collections.Generic;
using System.Linq;

namespace day09
{
    class Program
    {
        static void Main(string[] args)
        {
            PuzzleInput input = new PuzzleInput("input.txt");
            Map m = new Map(input.Lines);

            long sum = m.SumRiskLevels();
            Console.Out.WriteLine($"Total risk level: {sum}");

            m.FindBasins();
            m.MeasureBasinSizes();

            List<long> sizes = new List<long>();
            foreach(var basin in Basin.Basins)
            {
                sizes.Add(basin.size);
            }
            var sortedSizes = sizes.OrderByDescending(x => x).ToArray();

            long productOfThreeLargest =
                sortedSizes[0] *
                sortedSizes[1] *
                sortedSizes[2];

            Console.Out.WriteLine(
                $"Product of three largest basins: {productOfThreeLargest}");
        }
    }
}
