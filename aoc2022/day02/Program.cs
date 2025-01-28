using System;
using System.IO;

namespace day02
{
    class Program
    {
        static void Main(string[] args)
        {
            String[] lines = File.ReadAllLines("../../../input.txt");
            Solve(lines);
        }

        static void Solve(String[] lines)
        {
            int totalScorePartA = 0;
            int totalScorePartB = 0;

            foreach(var l in lines)
            {
                int scoreA = scoreLine(l, isPartTwo: false);
                int scoreB = scoreLine(l, isPartTwo: true);
                totalScorePartA += scoreA;
                totalScorePartB += scoreB;
            }

            Console.WriteLine("Part A:");
            Console.WriteLine(totalScorePartA);
            Console.WriteLine("-----------------");
            Console.WriteLine("Part B:");
            Console.WriteLine(totalScorePartB);

        }



        static int scoreLine(string l, bool isPartTwo)
        {
            (int, int) choices = decodeLine(l);

            if (isPartTwo)
            {
                // in part two, the second item in the line is not
                // which hand to play, it instead means:
                // X or 1 == you should lose
                // Y or 2 == you should draw
                // Z or 3 == you should win
                // convert this back into what to play

                // Should lose
                if (choices.Item2 == 1)
                {
                    switch(choices.Item1)
                    {
                        case 1: // if rock, use scissors
                            choices.Item2 = 3;
                            break;
                        case 2: // if paper, use rock
                            choices.Item2 = 1;
                            break;
                        case 3: // if scissors, use paper
                            choices.Item2 = 2;
                            break;
                    }
                }

                // Should draw
                else if (choices.Item2 == 2)
                {
                    choices.Item2 = choices.Item1;
                }

                // Should win
                else if (choices.Item2 == 3)
                {
                    switch (choices.Item1)
                    {
                        case 1: // if rock, use paper
                            choices.Item2 = 2;
                            break;
                        case 2: // if paper, use scissors
                            choices.Item2 = 3;
                            break;
                        case 3: // if scissors, use rock
                            choices.Item2 = 1;
                            break;
                    }
                }


            }


            int score = 0;

            // add 1 for rock, 2 for paper, 3 for scissors
            score += choices.Item2;

            // if it's a draw get 3 more points
            if (choices.Item2 == choices.Item1) score += 3;

            // check for a win (that's 6 more points)
            // rock/scissors
            // paper/rock
            // scissors/paper
            if ((choices.Item2 == 1 && choices.Item1 == 3) ||
                (choices.Item2 == 2 && choices.Item1 == 1) ||
                (choices.Item2 == 3 && choices.Item1 == 2)) score += 6;

            return score;
        }

        static (int, int) decodeLine(string l)
        {
            String[] parts = l.Split(' ');

            return (decode(parts[0]), decode(parts[1]));
        }

        static int decode(string item)
        {
            switch(item)
            {
                case "A":
                    return 1;
                case "B":
                    return 2;
                case "C":
                    return 3;
                case "X":
                    return 1;
                case "Y":
                    return 2;
                case "Z":
                    return 3;
                default:
                    throw new Exception("bad input!");

            }
        }
    }
}
