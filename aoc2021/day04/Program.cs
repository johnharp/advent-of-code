using System;
using System.Collections.Generic;

namespace day04
{
    class Program
    {
        static void Main(string[] args)
        {
            PuzzleInput input = new PuzzleInput("input.txt");

            Board firstWinner = null;
            Board mostRecentWinner = null;

            foreach(var num in input.CalledNumbers)
            {
                foreach(var board in input.Boards)
                {
                    if (board.MarkNumber(num))
                    {
                        if (firstWinner == null) firstWinner = board;
                        mostRecentWinner = board;
                    }
                }
            }

            Console.Out.WriteLine(
                $"First board to win has score: {firstWinner.score}");
            Console.Out.WriteLine(
                $"Last board to win has score: {mostRecentWinner.score}");
        }
    }
}
