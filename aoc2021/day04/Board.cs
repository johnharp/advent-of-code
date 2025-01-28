using System;
using System.Collections.Generic;

namespace day04
{
    public class Board
    {
        public string[][] values;
        public bool[][] marks;

        public int score;
        public bool win;

        public Board(
            string l1,
            string l2,
            string l3,
            string l4,
            string l5
            )
        {
            values = new string[5][];
            values[0] = line(l1);
            values[1] = line(l2);
            values[2] = line(l3);
            values[3] = line(l4);
            values[4] = line(l5);

            marks = new bool[5][];
            for (int i = 0; i<5; i++)
            {
                marks[i] = new bool[5];
            }
        }

        public bool MarkNumber(string num)
        {
            for (int r = 0; r<5; r++)
            {
                for (int c = 0; c<5; c++)
                {
                    if (values[r][c] == num)
                    {
                        marks[r][c] = true;
                    }
                }
            }

            // only compute the score if this board hasn't already won
            if (!win && IsWin())
            {
                int numValue = int.Parse(num);
                win = true;
                ComputeScore(numValue);
                return true;
            }

            return false;
        }

        public void ComputeScore(int justCalledNum)
        {
            score = 0;
            for (int r = 0; r < 5; r++)
            {
                for (int c = 0; c < 5; c++)
                {
                    if (!marks[r][c])
                    {
                        int value = int.Parse(values[r][c]);

                        score += value;
                    }
                }
            }
            score *= justCalledNum;
        }

        public bool IsWin()
        {
            bool win = false;
            // test rows
            for (int r = 0; r<5 && !win; r++)
            {
                for (int c = 0; c<5 && !win; c++)
                {
                    if (!marks[r][c]) break;
                    if (c == 4) win = true;
                }
            }
            // test columns
            for (int c = 0; c<5 &&!win; c++)
            {
                for (int r = 0; r<5 && !win; r++)
                {
                    if (!marks[r][c]) break;
                    if (r == 4) win = true;
                }
            }

            return win;
        }

        private string[] line(string t)
        {
            string[] values = t.Split(" ",
                StringSplitOptions.RemoveEmptyEntries);
            return values;
        }
    }
}
