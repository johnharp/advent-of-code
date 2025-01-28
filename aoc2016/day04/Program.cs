using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

namespace day04;

class Program
{
    static long part1 = 0;
    static long part2 = 0;
    
    static Regex LINE_REGEX = new Regex(@"^(\D+)-(\d+)\[([a-z]+)]");
    static void Main(string[] args)
    {
        string basedir = AppDomain.CurrentDomain.BaseDirectory;
        Directory.SetCurrentDirectory(basedir);
        string prefix = "../../../";
        string filename = "input.txt";
        // string filename = "input-sample.txt";
        string[] lines = File.ReadAllLines($"{prefix}{filename}");

        foreach (var line in lines)
        {
            handleLine(line);
        }

        Console.WriteLine($"Part 1: {part1}");
        Console.WriteLine($"Part 2: {part2}");
    }

    static void handleLine(string l)
    {
        Match match = LINE_REGEX.Match(l);
        Debug.Assert(match.Success, $"Bad input line format: {l}");

        string encryptedName = match.Groups[1].Value;
        long sectorId = long.Parse(match.Groups[2].Value);
        string checksum = match.Groups[3].Value;

        string computedChecksum = computeChecksum(encryptedName);
        if (checksum == computedChecksum)
        {
            part1 += sectorId;
            string name = decryptName(encryptedName, sectorId);

            if (name.Contains("northpole")) part2 = sectorId;
            Console.WriteLine($"Sector {sectorId}: {name}");
        }
    }

    static string computeChecksum(string encryptedName)
    {
        var charCounts = encryptedName.Replace("-", "")
            .GroupBy(c => c)
            .Select(g => new {character = g.Key, count = g.Count() })
            .ToList();

        var sortedCounts = charCounts
            .OrderByDescending(c => c.count)
            .ThenBy(c => c.character)
            .ToList();

        string checksum = string.Concat(sortedCounts
            .Take(5)
            .Select(c => c.character));

        return checksum;
    }

    static string decryptName(string encryptedName, long sectorId)
    {
        var sb = new StringBuilder();
        foreach(var c in encryptedName)
        {
            sb.Append(rotChar(c, (int) sectorId));
        }
        
        return sb.ToString();
    }

    static char rotChar(char c, int num)
    {
        int a = (int)'a';
        int size = 26;

        if (c == '-') return ' ';

        int val  = (((((int)c) - a) + num) % size) + a;
        return (char) val;
    }
}
