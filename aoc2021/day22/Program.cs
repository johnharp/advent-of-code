using System;
using System.Collections.Generic;
using System.Linq;

namespace day22
{
    class Program
    {
        static void Main(string[] args)
        {
            string filename =
                "input.txt";
               //"input-example.txt";
            var input = new PuzzleInput(filename);

            var uni = new Universe();

            foreach (var line in input.Lines)
            {
                var c1 = new Cuboid(line);
                uni.Cuboids.Add(c1);
            }

            //Part1(uni);
            Part2(uni);
        }

        public static void Part1(Universe uni)
        {
            int count = 0;
            int totalInspected = 0;

            for (int x = -50; x <= 50; x++)
            {
                for (int y = -50; y <= 50; y++)
                {
                    for (int z = -50; z <= 50; z++)
                    {
                        bool v = uni.Value(x, y, z);
                        if (v) count++;

                        totalInspected++;

                        if (totalInspected % 10_000 == 0)
                        {

                            Console.Out.Write(".");
                            Console.Out.Flush();
                        }
                    }
                }
            }
            Console.Out.WriteLine();
            Console.Out.WriteLine($"Found {count} cubes that are on.");
        }

        public static void Part2(Universe uni)
        {
            Console.WriteLine($"{uni.Cuboids.Count}");

            bool didSomething = true;
            Console.WriteLine($"{uni.Cuboids.Count}");

            uni.Reduce();
            Console.WriteLine($"{uni.Cuboids.Count}");

            while (didSomething)
            {
                didSomething = uni.Decimate();
                uni.RemoveContainedCuboids();
                uni.DetermineNonIntersectingCuboids();
                Console.WriteLine($"{uni.Cuboids.Count}   {uni.NonIntersectingCuboids.Count}");
            }

            Console.WriteLine($"Total cuboids: {uni.Cuboids.Count}");
            Console.WriteLine($"Non-intersecting cuboids: {uni.NonIntersectingCuboids.Count}");
            long Total = 0;
            foreach (var c in uni.NonIntersectingCuboids)
            {
                if (c.value)
                {
                    Total += c.Total;
                }
            }

            Console.WriteLine($"Total on bits: {Total}");
            //uni.Reduce();
            // Console.WriteLine($"{uni.Cuboids.Count}");


        }
    }
}
