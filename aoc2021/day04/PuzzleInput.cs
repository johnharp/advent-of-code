using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day04
{
    public class PuzzleInput
    {
        public String[] CalledNumbers;
        public List<Board> Boards;

        public PuzzleInput(string filename)
        {
            string[] lines = File.ReadLines($"../../../{filename}").ToArray();
            CalledNumbers = lines[0].Split(",");

            int i = 2;

            Boards = new List<Board>();

            while (i < lines.Length)
            {
                Board board = new Board(
                    lines[i],
                    lines[i + 1],
                    lines[i + 2],
                    lines[i + 3],
                    lines[i + 4]);
                Boards.Add(board);
                
                i += 6;
            }

        }

        public bool AnyBoardWin()
        {
            foreach (var board in Boards)
            {
                if (board.win)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
