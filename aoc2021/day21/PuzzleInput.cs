using System;
using System.IO;

namespace day21
{
    public class PuzzleInput
    {
        public string[] Lines;
        public PuzzleInput(string filename)
        {
            ReadInputFile($"../../../{filename}");
        }

        public void ReadInputFile(string fullpath)
        {
            Lines = File.ReadAllLines(fullpath);
        }
    }
}
