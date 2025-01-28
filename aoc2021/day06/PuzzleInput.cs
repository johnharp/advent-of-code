using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day06
{
    public class PuzzleInput
    {
        public List<int> Values = new List<int>();

        public PuzzleInput(string filename)
        {
            ReadInputFile($"../../../{filename}");
        }

        private void ReadInputFile(string filename)
        {
            List<String> Lines = File.ReadLines(filename).ToList();
            string[] strings = Lines[0].Split(",");
            foreach(var s in strings) {
                int value = int.Parse(s);
                Values.Add(value);
            }
        }
    }
}
