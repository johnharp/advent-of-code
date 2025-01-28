using System;
using System.Collections.Generic;
using System.IO;

namespace day13
{
    public class PuzzleInput
    {
        public List<String> DotLines;
        public List<String> FoldLines;

        public PuzzleInput(string filename)
        {
            ReadInputFile($"../../../{filename}");
        }

        private void ReadInputFile(string fullpath)
        {
            Console.WriteLine($"Reading {fullpath}");
            DotLines = new List<string>();
            FoldLines = new List<string>();

            var lines = File.ReadLines(fullpath);
            int section = 1;
            foreach (string line in lines)
            {
                if (line == "")
                {
                    section = 2;
                    continue;
                }

                if (section == 1)
                {
                    DotLines.Add(line);
                }
                else
                {
                    FoldLines.Add(line);
                }
            }
        }
    }
}
