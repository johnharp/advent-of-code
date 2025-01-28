using System;
namespace day11
{
    public class Map
    {
        public int[][] values;

        public Map(string[] Lines)
        {
            values = new int[10][];
            for (int i = 0; i<10; i++)
            {
                values[i] = new int[10];
            }

            for(int col = 0; col < 10; col++)
            {
                for(int row = 0; row<10; row++)
                {
                    char character = Lines[row][col];
                    int value = int.Parse(new string(character, 1));
                    values[col][row] = value;
                }
            }
        }

        public int Step()
        {
            int totalNumFlashes = 0;

            IncAll();
            int numFlashesThisCheck;

            numFlashesThisCheck = CheckAllForFlash();
            totalNumFlashes += numFlashesThisCheck;

            while(numFlashesThisCheck > 0)
            {
                numFlashesThisCheck = CheckAllForFlash();
                totalNumFlashes += numFlashesThisCheck;
            }

            return totalNumFlashes;
        }

        public int CheckAllForFlash()
        {
            int numFlash = 0;

            for (int col = 0; col < 10; col++)
            {
                for (int row = 0; row < 10; row++)
                {
                    if (values[col][row] > 9)
                    {
                        Flash(col, row);
                        numFlash++;
                    }
                }
            }

            return numFlash;
        }

        public void Flash(int col, int row)
        {
            // Increment all neighbors
            for(int dc = -1; dc <= 1; dc++)
            {
                for (int dr = -1; dr <=1; dr++)
                {
                    IncIfNotZero(col + dc, row + dr);
                }
            }

            values[col][row] = 0;
        }

        public void IncIfNotZero(int col, int row)
        {
            if (col >= 0 && col <= 9 &&
                row >= 0 && row <= 9 &&
                values[col][row] != 0)
            {
                values[col][row]++;
            }
        }

        public void Inc(int col, int row)
        {
            if (col >= 0 && col <= 9 &&
                row >= 0 && row <= 9)
            {
                values[col][row]++;
            }
        }

        public void IncAll()
        {
            for (int col = 0; col < 10; col++)
            {
                for (int row = 0; row < 10; row++)
                {
                    Inc(col, row);
                }
            }
        }

        public void Dump()
        {
            for(int row=0; row<10; row++)
            {
                for (int col=0; col<10; col++)
                {
                    Console.Write(string.Format("{0,2}", values[col][row]));
                }
                Console.WriteLine();
                Console.WriteLine();
            }
        }
    }
}
