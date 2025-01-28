using System.Collections.Generic;

namespace day05
{
    internal class Program
    {
        static int configStart = 0;
        static int configEnd = 0;
        static int movesStart = 0;
        static int movesEnd = 0;

        static int numberOfStacks = 0;
        static List<String> stacks = new List<string>();

        static String[] lines = Array.Empty<string>();

        static void Main(string[] args)
        {
            //lines = File.ReadAllLines("../../../input-example.txt");
            lines = File.ReadAllLines("../../../input.txt");
            determineInputSections();
            parseInitialStackConfig();

            Console.WriteLine("---------------------------------------------");
            Console.WriteLine("Starting configuration:");
            printStacks();

            applyMoveSteps(moveAllAtOnce: false);
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine("Ending configuration for Part 1:");
            printStacks();
            Console.WriteLine();
            Console.WriteLine($"Top of all stacks: {topOfAllStacks()}");

            parseInitialStackConfig();
            applyMoveSteps(moveAllAtOnce: true);
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine("Ending configuration for Part 1:");
            printStacks();
            Console.WriteLine();
            Console.WriteLine($"Top of all stacks: {topOfAllStacks()}");
        }



        static void determineInputSections()
        {
            int i = 0;
            int stackNumbersIndex = 0;

            // Find the blank line -- this is the division between the starting stack
            // configuration and the moves
            while (lines[i] != "")
            {
                i++;
            }

            stackNumbersIndex = i - 1;
            configEnd = i - 2;

            movesStart = i + 1;
            movesEnd = lines.Length - 1;

            string[] stackNumbers = lines[stackNumbersIndex].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            numberOfStacks = stackNumbers.Length;

            Console.WriteLine($"# Stacks: {numberOfStacks}   numberConfig: [{configStart} - {configEnd}]    Moves: [{movesStart} - {movesEnd}]");
        }

        private static void parseInitialStackConfig()
        {
            stacks = new List<string>();
            for (int s = 0; s < numberOfStacks; s++)
            {
                stacks.Add("");
            }

            // get the starting stack config
            for (int j = configStart; j <= configEnd; j++)
            {
                for (int k = 0; k < numberOfStacks; k++)
                {
                    // 1 5 9 13 
                    char box = lines[j].ToCharArray()[k * 4 + 1];

                    if (box != ' ')
                    {
                        stacks[k] = stacks[k] + box;
                    }
                }

            }
        }

        static void applyMoveSteps(bool moveAllAtOnce)
        {
            for (int i = movesStart; i <= movesEnd; i++)
            {
                string moveLine = lines[i];
                string[] moveParts = moveLine.Split();
                int numberToMove = int.Parse(moveParts[1]);
                int sourceStack = int.Parse(moveParts[3]) - 1;
                int destStack = int.Parse(moveParts[5]) - 1;

                applyMoveStep(numberToMove, sourceStack, destStack, moveAllAtOnce);
            }

        }

        static void applyMoveStep(int num, int src, int dst, bool moveAllAtOnce)
        {
            //Console.WriteLine($"Move {num} from {src} -> {dst}");
            string boxesToMove = new String(stacks[src].Substring(0, num).ToArray());
            if (!moveAllAtOnce)
            {
                boxesToMove = new String(boxesToMove.Reverse().ToArray());
            }

            stacks[dst] = boxesToMove + stacks[dst];

            stacks[src] = stacks[src].Substring(num);
        }

        static void printStacks()
        {
            for (int i = 0; i < numberOfStacks; i++)
            {
                Console.WriteLine($"Stack {i + 1}: |{stacks[i]}|");
            }
        }

        static string topOfAllStacks()
        {
            string v = "";

            for(int i = 0; i < numberOfStacks; i++)
            {
                v = v + stacks[i][0];
            }

            return v;
        }
    }
}