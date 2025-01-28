using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace day03;

class Program
{
    static void Main(string[] args)
    {
        string basedir = AppDomain.CurrentDomain.BaseDirectory;
        Directory.SetCurrentDirectory(basedir);
        string prefix = "../../../";
        string filename = "input.txt";
        string[] lines = File.ReadAllLines($"{prefix}{filename}");

        Regex regex = new Regex(@"^\s*(\d+)\s+(\d+)\s+(\d+)\s*$");

        int part1 = 0;
        int part2 = 0;
        List<int> byRow = new List<int>();
        List<List<int>> byColLists = new List<List<int>>();

        for (int i = 0; i < 3; i++) {
            byColLists.Add(new List<int>());
        }

        for (int i = 0; i < lines.Length; i++)
        {
            string line = lines[i];

            Match match = regex.Match(line);
            Debug.Assert(match.Success, $"Bad input line format: {line}");
            int num1 = int.Parse(match.Groups[1].Value);
            int num2 = int.Parse(match.Groups[2].Value);
            int num3 = int.Parse(match.Groups[3].Value);

            byRow.Add(num1);
            byRow.Add(num2);
            byRow.Add(num3);

            byColLists[0].Add(num1);
            byColLists[1].Add(num2);
            byColLists[2].Add(num3);
        }

        part1 = countValidTris(byRow);
        part2 += countValidTris(byColLists[0]);
        part2 += countValidTris(byColLists[1]);
        part2 += countValidTris(byColLists[2]);

        Console.WriteLine($"Part 1: {part1}");
        Console.WriteLine($"Part 2: {part2}");
    }

    private static int countValidTris(List<int> nums)
    {
        int count = 0;
        Debug.Assert(nums.Count % 3 == 0, "List of triangle sides should be divisible by 3");
        for (int i = 0; i < nums.Count/3; i++)
        {
            if (validTri(nums[3*i + 0], nums[3*i + 1], nums[3*i + 2])) count++;
        }
        return count;
    }

    private static bool validTri(int num1, int num2, int num3)
    {
        return num1 < num2 + num3 &&
               num2 < num1 + num3 &&
               num3 < num1 + num2;
    }
}
