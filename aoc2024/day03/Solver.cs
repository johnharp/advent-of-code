namespace day03;

using System.Text.RegularExpressions;
using System.Linq;
using System.Xml.Schema;

public class Solver
{
    private bool mulEnabled = true;

    public long part1(string[] lines)
    {
        string pattern = @"mul\(\d{1,3},\d{1,3}\)";
        return withPattern(pattern, lines);
    }

    public long part2(string[] lines)
    {
        string pattern = @"do\(\)|don\'t\(\)|mul\(\d{1,3},\d{1,3}\)";
        return withPattern(pattern, lines);
    }

    public long withPattern(string pattern, string[] lines) {
        var input = parse(pattern, lines);
        long sum = 0;

        foreach(var instructions in input)
        {
            foreach(var instruction in instructions)
            {
                long value = execute(instruction);

                sum += value;
            }
        }
        return sum;
    }


    private List<List<string>> parse(string pattern, string[] lines)
    {

        Regex regex = new Regex(pattern);
        List<List<string>> result = new List<List<string>>();

        foreach (string line in lines)
        {
            List<string> instructions = new List<string>();
            MatchCollection matches = regex.Matches(line);

            for (int count = 0; count < matches.Count; count++)
            {
                string instruction = matches[count].Value;
                instructions.Add(instruction);
            }

            result.Add(instructions);
        }
        return result;
    }

    private long execute(string instruction)
    {
        if (instruction.StartsWith("mul(") && mulEnabled) return multiply(instruction);
        if (instruction.StartsWith("do(")) mulEnabled = true;
        if (instruction.StartsWith("don't(")) mulEnabled = false;

        return 0;
    }

    private long multiply(string instruction)
    {
        string pattern = @"mul\((\d{1,3}),(\d{1,3})\)";
        Regex regex = new Regex(pattern);
        MatchCollection matches = regex.Matches(instruction);
        long a = long.Parse(matches[0].Groups[1].Value);
        long b = long.Parse(matches[0].Groups[2].Value);
        return a*b;
    }
}