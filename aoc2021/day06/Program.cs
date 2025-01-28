using System;
using System.Collections.Generic;

namespace day06
{
    class Program
    {
        static long[] tally = new long[9];

        static void Main(string[] args)
        {
            PuzzleInput input = new PuzzleInput("input.txt");
            List<int> Values = input.Values;

            DoInitialTally(Values);
            PrintTally();

            for (int i = 1; i<=256; i++)
            {
                UpdateTally();
                Console.Out.WriteLine($"After {i} days:");
                Console.Out.WriteLine("======================");
                PrintTally();
            }
        }

        static void PrintTally()
        {
            long total = 0;
            for (int i = 0; i<tally.Length; i++)
            {
                total += tally[i];
                Console.Out.WriteLine(
                    $"{i}: {tally[i]}");
            }
            Console.Out.WriteLine($"T: {total}");
        }

        static void DoInitialTally(List<int> population)
        {
            foreach(int value in population)
            {
                tally[value]++;
            }
        }

        static void UpdateTally()
        {
            long numToSpawn = 0;

            numToSpawn = tally[0];

            tally[0] = tally[1];
            tally[1] = tally[2];
            tally[2] = tally[3];
            tally[3] = tally[4];
            tally[4] = tally[5];
            tally[5] = tally[6];
            tally[6] = tally[7] + numToSpawn;
            tally[7] = tally[8];
            tally[8] = numToSpawn;
        }
    }
}
