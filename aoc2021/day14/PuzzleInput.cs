using System;
using System.IO;
using System.Collections.Generic;

namespace day14
{
    public class PuzzleInput
    {
        public string InitialElements;
        public List<String> InsertionRules = new List<string>();

        public PuzzleInput(string filename)
        {
            ReadInputFile($"../../../{filename}");
        }

        private void ReadInputFile(string fullpath)
        {
            Console.WriteLine($"Reading file {fullpath}");
            string[] lines = File.ReadAllLines(fullpath);

            InitialElements = lines[0];
            for (int i = 2; i<lines.Length; i++)
            {
                InsertionRules.Add(lines[i]);
            }
        }
    }
}
