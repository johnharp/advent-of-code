using System;
using System.IO;

namespace day01
{
    public class Input
    {
        public int[] Example()
        {
            return ReadInputFile("../../../example-input.txt");
        }

        public int[] PartA()
        {
            return ReadInputFile("../../../parta-input.txt");
        }

        public int[] PartB()
        {
            return ReadInputFile("../../../partb-input.txt");
        }

        private int[] ReadInputFile(string filename)
        {
            string[] lines = File.ReadAllLines(filename);
            int[] intlines = new int[lines.Length];

            for(int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                int intval = int.Parse(line);
                intlines[i] = intval;
            }

            return intlines;
        }
    }
}
