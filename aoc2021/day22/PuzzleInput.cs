﻿using System;
using System.IO;

namespace day22
{
    public class PuzzleInput
    {
        public string[] Lines;

        public PuzzleInput(string filename)
        {
            ReadInputFile($"../../../{filename}");
        }

        private void ReadInputFile(string fullpath)
        {
            Lines = File.ReadAllLines(fullpath);
        }
    }
}
