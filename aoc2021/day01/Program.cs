using System;

namespace day01
{
    class Program
    {
        static void Main(string[] args)
        {
            Input input = new Input();
            int[] depthValues = input.PartA();
            int numIncreases;

            SonarHelper helper = new SonarHelper();
            numIncreases = helper.CountDepthIncreases(depthValues);

            Console.Out.WriteLine($"There were {numIncreases} increases.");

            int numSlidingWindowIncreases;
            numSlidingWindowIncreases = helper.CountDepthIncreasesSlidingWindow(depthValues);

            Console.Out.WriteLine($"There were {numSlidingWindowIncreases} sliding window increases.");

        }
    }
}
