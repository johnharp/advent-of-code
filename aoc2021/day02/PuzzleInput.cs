using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day02
{
    public class PuzzleInput
    {
        public NavStep[] Example()
        {
            return ReadInputFile("../../../example-input.txt");
        }

        public NavStep[] PartA()
        {
            return ReadInputFile("../../../parta-input.txt");
        }


        private NavStep[] ReadInputFile(string filename)
        {
            List<NavStep> steps = new List<NavStep>();

            var lines = File.ReadLines(filename);

            foreach(string line in lines)
            {
                steps.Add(new NavStep(line));
            }
            return steps.ToArray();
        }
    }
}
