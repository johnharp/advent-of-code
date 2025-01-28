using System.Collections;

namespace day02;

class Program
{
    static Dictionary<(int, int), char> keypad = new Dictionary<(int, int), char>
    {
        { (0, 0), '1' }, { (1, 0), '2' }, { (2, 0), '3' },
        { (0, 1), '4' }, { (1, 1), '5' }, { (2, 1), '6' },
        { (0, 2), '7' }, { (1, 2), '8' }, { (2, 2), '9' },
    };

    static Dictionary<(int, int), char> part2keypad = new Dictionary<(int, int), char>
    {
                                          { (2, 0), '1' },
                         { (1, 1), '2' }, { (2, 1), '3' }, { (3, 1), '4' },
        { (0, 2), '5' }, { (1, 2), '6' }, { (2, 2), '7' }, { (3, 2), '8' }, { (4, 2), '9' },
                         { (1, 3), 'A' }, { (2, 3), 'B' }, { (3, 3), 'C' },
                                          { (2, 4), 'D' }
    };

    static void Main(string[] args)
    {
        string basedir = AppDomain.CurrentDomain.BaseDirectory;
        Directory.SetCurrentDirectory(basedir);
        string prefix = "../../../";
        string filename = "input.txt";
        string[] lines = File.ReadAllLines($"{prefix}{filename}");

        List<char> combo = new List<char>();
        List<char> part2combo = new List<char>();

        (int, int) pos = (1, 1);
        (int, int) part2pos = (0, 2);

        foreach (var line in lines)
        {
            foreach (var c in line)
            {
                (int, int) newPos;
                (int, int) newPart2Pos;

                newPos = computeNewPos(pos, c);
                newPart2Pos = computeNewPos(part2pos, c);

                if (keypad.ContainsKey(newPos)) pos = newPos;
                if (part2keypad.ContainsKey(newPart2Pos)) part2pos = newPart2Pos;
            }

            combo.Add(keypad[pos]);
            part2combo.Add(part2keypad[part2pos]);
        }

        string part1 = String.Join("", combo);
        string part2 = String.Join("", part2combo);

        Console.WriteLine($"Part 1: {part1}");
        Console.WriteLine($"Part 2: {part2}");
    }

    static (int, int) computeNewPos((int, int) pos, char c)
    {
        (int, int) newPos;

        switch (c)
        {
            case 'U':
                newPos = (pos.Item1, pos.Item2 - 1);
                break;
            case 'R':
                newPos = (pos.Item1 + 1, pos.Item2);
                break;
            case 'D':
                newPos = (pos.Item1, pos.Item2 + 1);
                break;
            case 'L':
                newPos = (pos.Item1 - 1, pos.Item2);
                break;
            default:
                throw new Exception($"Unknown input char: {c}");
        }

        return newPos;
    }
}
