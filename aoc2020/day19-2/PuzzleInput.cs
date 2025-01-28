using System;
using System.IO;
using System.Collections.Generic;

namespace day19_2
{
    public class PuzzleInput
    {
        public List<String> RuleLines = new List<string>();
        public List<String> MessageLines = new List<string>();

        public PuzzleInput(string filename)
        {
            ReadInputFile($"../../../{filename}");
        }


        private void ReadInputFile(string filename)
        {
            var lines = File.ReadLines(filename);
            bool rules = true;

            foreach(var line in lines)
            {
                if (line == "")
                {
                    rules = false;
                }

                if (rules)
                {
                    RuleLines.Add(line);
                }
                else
                {
                    MessageLines.Add(line);
                }
            }

        }
    }
}
