using System;
using System.Collections;

namespace day20
{
    class Program
    {
        static void Main(string[] args)
        {
            string filename = "input.txt";
            //string filename = "input-example.txt";

            var input = new PuzzleInput(filename);

            BitArray a = input.Algorithm;

            Image i1 = input.InitialImage;
            Image i2;

            for (int i = 0; i < 50; i++)
            {
                i2 = i1.Step(a);
                //i2.Dump();
                Console.WriteLine($"There are {i2.CountLitPixels()} pixels lit");
                i1 = i2;
            }
            
        }
    }
}
