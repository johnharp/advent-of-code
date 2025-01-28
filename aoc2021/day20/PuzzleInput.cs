using System;
using System.Collections;
using System.IO;

namespace day20
{
    public class PuzzleInput
    {
        public BitArray Algorithm;
        public Image InitialImage;

        public PuzzleInput(string filename)
        {
            ReadInputFile($"../../../{filename}");
        }

        public void ReadInputFile(string fullpath)
        {
            string[] Lines = File.ReadAllLines(fullpath);



            string algorithmString = Lines[0];
            int algorithmLength = algorithmString.Length;
            Algorithm = new BitArray(algorithmLength, false);
            for(int i = 0; i<algorithmLength; i++)
            {
                if (algorithmString[i] == '.')
                {
                    // No-Op
                }
                else if (algorithmString[i] == '#')
                {
                    Algorithm[i] = true;
                }
                else
                {
                    throw new Exception($"Invalid format for algorithm, character: {algorithmString[i]}");
                }
            }

            if (!string.IsNullOrEmpty(Lines[1]))
                throw new Exception($"Invalid input file -- expected a blank line at position 1.  Found: {Lines[1]}");

            int width = Lines[2].Length;
            int height = Lines.Length - 2;

            InitialImage = new Image(width, height);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x ++)
                {
                    char c = Lines[y + 2][x];
                    bool v = c == '#' ? true : false;

                    InitialImage.Set(x, y, v);

                }
            }
        }
    }
}
