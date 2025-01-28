using System;
namespace day01
{
    public class SonarHelper
    {
        public int CountDepthIncreases(int[] values)
        {
            int numIncreases = 0;

            for (int i = 1; i < values.Length; i++)
            {
                if (values[i - 1] < values[i])
                {
                    numIncreases++;
                }
            }

            return numIncreases;
        }

        // 199  A
        // 200  A B    
        // 208  A B C  
        // 210    B C D
        // 200  E C D
        // 207  E F   D
        // 240  E F G  
        // 269    F G H
        // 260      G H
        // 263        H

        // 199  A        <---
        // 200  A B    
        // 208  A B    
        // 210    B      <---

        // Since any sliding window and its predecessor will
        // overlap by two numbers, we can discard those and only
        // compare the first number in the preceeding window
        // with the last number in the new current window

        public int CountDepthIncreasesSlidingWindow(int[] values)
        {
            int numIncreases = 0;

            for (int i = 3; i < values.Length; i++)
            {
                if (values[i] > values[i-3])
                {
                    numIncreases++;
                }
            }

            return numIncreases;
        }
    }
}
