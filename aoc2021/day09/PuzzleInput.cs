using System;
using System.Collections.Generic;
using System.IO;

namespace day09
{
    public class PuzzleInput
    {
        public string[] Lines;

        public PuzzleInput(string filename)
        {
            ReadInputFile($"../../../{filename}");
        }

        public void ReadInputFile(string filename)
        {
            Lines = File.ReadAllLines(filename);
        }
    }
}
