using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day07
{
    public class PuzzleInput
    {
        public List<String> Lines;


        public PuzzleInput(String filename)
        {
            ReadInputFile($"../../../{filename}");
        }

        public void ReadInputFile(string filename)
        {
            Lines = File.ReadLines(filename).ToList();
        }


    }
}
