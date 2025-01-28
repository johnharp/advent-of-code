using System;
using System.IO;

namespace day01
{
    class Program
    {
        static int runningSum = 0;

        static int firstPlaceSum = 0;
        static int secondPlaceSum = 0;
        static int thirdPlaceSum = 0;

        static void Main(string[] args)
        {
            string[] Lines = File.ReadAllLines("../../../input.txt");

            

            foreach(var line in Lines)
            {
                if (line == "") finishGroup();
                else
                {
                    int value = int.Parse(line);
                    accumulateValue(value);
                }
            }
            finishGroup();

            Console.WriteLine($"First Place Sum = {firstPlaceSum}");
            Console.WriteLine($"Second Place Sum = {secondPlaceSum}");
            Console.WriteLine($"Third Place Sum = {thirdPlaceSum}");

            Console.WriteLine($"Sum of top 3: {firstPlaceSum + secondPlaceSum + thirdPlaceSum}");
        }

        static void accumulateValue(int v)
        {
            runningSum += v;
        }

        static void finishGroup()
        {
            if (runningSum >= firstPlaceSum)
            {
                thirdPlaceSum = secondPlaceSum;
                secondPlaceSum = firstPlaceSum;
                firstPlaceSum = runningSum;
            }
            else if (runningSum >= secondPlaceSum)
            {
                thirdPlaceSum = secondPlaceSum;
                secondPlaceSum = runningSum;
            }
            else if (runningSum >= thirdPlaceSum)
            {
                thirdPlaceSum = runningSum;
            }



            runningSum = 0;
        }
    }
}
