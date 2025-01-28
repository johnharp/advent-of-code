using System;
using System.Collections.Generic;
using System.Linq;

namespace day10
{
    class Program
    {
        static void Main(string[] args)
        {
            string filename = "input.txt";

            PuzzleInput input = new PuzzleInput(filename);
            Solve(input);
        }

        static void Solve(PuzzleInput input)
        {
            long errorScore = 0;
            List<long> AutocorrectScores = new List<long>();

            foreach (string line in input.Lines)
            {
                Stack<char> s = new Stack<char>();
                bool isCorrupted = false;

                char[] characters = line.ToCharArray();
                for (int i = 0; i< characters.Length && !isCorrupted; i++)
                {
                    char c = characters[i];

                    if (IsOpeningBracket(c))
                    {
                        s.Push(c);
                    }
                    else if (IsClosingBracket(c))
                    {
                        char openingChar;
                        if (s.TryPop(out openingChar))
                        {
                            if (!IsMatch(openingChar, c))
                            {
                                // corrupted chunk
                                errorScore += ErrorScore(c);
                                isCorrupted = true;
                                break;
                            }
                        }
                    }
                }

                if (s.Count > 0 && !isCorrupted)
                {
                    long totalAutocorrectScore = 0;

                    // Incomplete line
                    Console.Out.Write("Incomplete: ");
                    while (s.Count > 0)
                    {
                        char c = s.Pop();
                        totalAutocorrectScore *= 5;
                        totalAutocorrectScore += AutocorrectScore(c);
                        Console.Out.Write(c);
                    }
                    Console.Out.Write(
                        $"  autocorrect score: {totalAutocorrectScore}");
                    AutocorrectScores.Add(totalAutocorrectScore);
                    Console.Out.WriteLine();
                }
            }

            Console.Out.WriteLine($"Total error score = {errorScore}");
            AutocorrectScores = AutocorrectScores.OrderBy(x => x).ToList();
            int totalNumScores = AutocorrectScores.Count();
            int middleScoreIndex = totalNumScores / 2;
            Console.Out.WriteLine($"Middle score index = {middleScoreIndex}");
            long middleScore = AutocorrectScores[middleScoreIndex];
            Console.Out.WriteLine($"Autocorrect middle score = {middleScore}");
        }

        static bool IsOpeningBracket(char c)
        {
            return c == '(' || c == '[' || c == '{' || c == '<';
        }

        static bool IsClosingBracket(char c)
        {
            return c == ')' || c == ']' || c == '}' || c == '>';
        }

        static bool IsMatch(char openChar, char closeChar)
        {
            return
                (openChar == '(' && closeChar == ')') ||
                (openChar == '[' && closeChar == ']') ||
                (openChar == '{' && closeChar == '}') ||
                (openChar == '<' && closeChar == '>');
        }

        static int ErrorScore(char c)
        {
            int score = 0;

            if (c == ')') score = 3;
            else if (c == ']') score = 57;
            else if (c == '}') score = 1197;
            else if (c == '>') score = 25137;

            return score;
        }

        static int AutocorrectScore(char c)
        {
            int score = 0;
            if (c == '(') score = 1;
            else if (c == '[') score = 2;
            else if (c == '{') score = 3;
            else if (c == '<') score = 4;

            return score;
        }
    }
}
