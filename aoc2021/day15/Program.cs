using System;

namespace day15
{
    class Program
    {
        static void Main(string[] args)
        {
            string filename = "input.txt";

            var input = new PuzzleInput(filename);
            var map = new Map(input.Lines);
            //Part1(map);
            Part2(map);
        }

        private static void Part1(Map map)
        {
            Console.WriteLine("Part1: ");
            map.DumpValues();
            map.ComputeLeastPathSums();
            map.DumpLeastPathSums();
            Console.WriteLine($"Least path sum: {map.LPS[0][0]}");
        }

        private static void Part2(Map map)
        {
            Console.WriteLine("Part2: ");
            Map m = new Map(map, 5);
            //tiledmap.ComputeLeastPathSums();
            //tiledmap.DumpValues();
            //tiledmap.DumpLeastPathSums();
            //Console.WriteLine($"Least path sum: {tiledmap.LPS[0][0]}");

            Console.WriteLine(m.LeastPathSumAtPoint(1, 2));
        }
    }
}
