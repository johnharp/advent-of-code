namespace day02;

using System.Collections.Immutable;

class Program
{

    static void Main(string[] args)
    {
        var input = puzzleInput();
        bool solved = false;

        var part1Result = solveWith(12, 2, input);
        Console.WriteLine("Part 1: " + part1Result[0]);

        for (int n = 0; n < 100 && !solved; n++)
        {
            for (int v = 0; v < 100 && !solved; v++)
            {
                var result = solveWith(n, v, input);
                if (result[0] == 19690720)
                {
                    Console.WriteLine("Part 2: " + (100 * n + v).ToString());
                    solved = true;
                }
            }
        }
    }

    static int[] solveWith(int noun, int verb, int[] input)
    {
         var modifiedInput = new int[input.Length];
        input.CopyTo(modifiedInput, 0);

        modifiedInput[1] = noun;
        modifiedInput[2] = verb;

        var result = solve(modifiedInput);
        return result;
    }

    static int[] solve(int[] input)
    {
        var values = new int[input.Length];
        input.CopyTo(values, 0);

        for (int i = 0; i < values.Length; i += 4)
        {
            //Console.WriteLine(String.Join(", ", values));
            var opcode = values[i];
            if (opcode == 99)
            {
                break;
            }
            var a = values[i + 1];
            var b = values[i + 2];
            var c = values[i + 3];
            if (opcode == 1)
            {
                values[c] = values[a] + values[b];
            }
            else if (opcode == 2)
            {
                values[c] = values[a] * values[b];
            }
        }
        return values;
    }


    static int[] sampleInput()
    {
        return new int[] { 1, 9, 10, 3, 2, 3, 11, 0, 99, 30, 40, 50 };
    }

    static int[] puzzleInput()
    {
        return new int[] { 1, 0, 0, 3, 1, 1, 2, 3, 1, 3, 4, 3, 1, 5, 0, 3, 2, 1, 10, 19, 2, 9, 19, 23, 2, 23, 10, 27, 1, 6, 27, 31, 1, 31, 6, 35, 2, 35, 10, 39, 1, 39, 5, 43, 2, 6, 43, 47, 2, 47, 10, 51, 1, 51, 6, 55, 1, 55, 6, 59, 1, 9, 59, 63, 1, 63, 9, 67, 1, 67, 6, 71, 2, 71, 13, 75, 1, 75, 5, 79, 1, 79, 9, 83, 2, 6, 83, 87, 1, 87, 5, 91, 2, 6, 91, 95, 1, 95, 9, 99, 2, 6, 99, 103, 1, 5, 103, 107, 1, 6, 107, 111, 1, 111, 10, 115, 2, 115, 13, 119, 1, 119, 6, 123, 1, 123, 2, 127, 1, 127, 5, 0, 99, 2, 14, 0, 0 };
    }

    static int[] preprocessInput(int[] input)
    {
        var modifiedInput = new int[input.Length];
        input.CopyTo(modifiedInput, 0);

        modifiedInput[1] = 12;
        modifiedInput[2] = 2;

        return modifiedInput;
    }

}
