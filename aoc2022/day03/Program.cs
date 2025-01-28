using System;
using System.Collections.Generic;
using System.IO;

namespace day03
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] lines = File.ReadAllLines("../../../input.txt");
            Solve(lines);
        }

        static void Solve(String[] lines)
        {
            long totalOfPriorities = 0;
            long totalOfGroupPriorities = 0;

            foreach(var line in lines)
            {
                totalOfPriorities += SolveLine(line);
            }

            for (int i = 0; i < lines.Length/3; i++)
            {
                totalOfGroupPriorities += SolveGroup(lines[i*3], lines[i*3 + 1], lines[i*3 + 2]);
            }

            Console.WriteLine($"Total priority of matching items: {totalOfPriorities}");
            Console.WriteLine($"Total priority of groups: {totalOfGroupPriorities}");

        }

        static long SolveLine(String line)
        {
            int numItems = line.Length;
            int itemsPerCompartment = numItems / 2;

            String leftItems = line.Substring(0, itemsPerCompartment);
            String rightItems = line.Substring(itemsPerCompartment, itemsPerCompartment);

            String matches = FindMatchingItems(leftItems, rightItems);
            char match = matches[0];

            long priority = priorityOfMatchingItem(match);

            return priority;
        }

        public static long SolveGroup(String first, String second, String third)
        {
            String firstAndSecondMatches = FindMatchingItems(first, second);
            String matches = FindMatchingItems(firstAndSecondMatches, third);

            char match = matches[0];
            long priority = priorityOfMatchingItem(match);

            return priority;
        }


        static String FindMatchingItems(String first, String second)
        {
            HashSet<char> matches = new HashSet<char>(); ;

            HashSet<char> hash = new HashSet<char>(first);
            foreach(char c in second.ToCharArray())
            {
                if (hash.Contains(c)) matches.Add(c);
            }

            return String.Join("", matches);
        }

        static long priorityOfMatchingItem(char c)
        {
            long value = 0;

            if (Char.IsUpper(c))
            {
                value = c - 'A' + 27;
            }
            else
            {
                value = c - 'a' + 1;
            }

            return value;
        }
    }
}
