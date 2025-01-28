using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace day03
{
    public class PuzzleInput
    {
        public List<String> ExampleInput()
        {
            return ReadInputFile("../../../input-example.txt");
        }

        public List<String> Input()
        {
            return ReadInputFile("../../../input.txt");
        }


        private List<String> ReadInputFile(string filename)
        {
            var lines = File.ReadLines(filename);

            return lines.ToList();
        }
    }
}
