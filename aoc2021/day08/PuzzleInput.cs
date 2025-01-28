using System;
using System.IO;
using System.Collections.Generic;
namespace day08
{
    public class PuzzleInput
    {
        public string[] Lines;

        public PuzzleInput(String filename)
        {
            ReadInputFile($"../../../{filename}");
        }

        public void ReadInputFile(string filename)
        {
            Lines = File.ReadAllLines(filename);
        }
    }
}
