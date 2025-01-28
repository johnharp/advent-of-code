namespace day02;

using System.Text.RegularExpressions;
using System.Linq;

public class Solver
{
    public long part1(string[] lines)
    {
        var input = parse(lines);
        long numSafeReports = 0;

        foreach (var report in input)
        {
            if (reportIsSafe(report)) numSafeReports++;
        }

        return numSafeReports;
    }

    public long part2(string[] lines)
    {
        var input = parse(lines);
        long numSafeReports = 0;

        foreach (var report in input)
        {
            if (reportIsSafe(report))
            {
                numSafeReports++;
            }
            else
            {
                for (int i = 0; i < report.Count; i++)
                {
                    var reportCopy = new List<long>(report);
                    reportCopy.RemoveAt(i);

                    if (reportIsSafe(reportCopy))
                    {
                        numSafeReports ++;
                        break;
                    }
                }
            }
        }

        return numSafeReports;
    }

    bool reportIsSafe(List<long> values)
    {
        (long firstDelta, long firstAbsDelta, long firstSign) = compare(values[1], values[0]);
        if (!isSafe(firstAbsDelta, firstSign, firstSign)) return false;

        for (int i = 2; i < values.Count; i++)
        {
            (long delta, long absDelta, long sign) = compare(values[i], values[i-1]);
            if (!isSafe(absDelta, sign, firstSign)) return false;
        }
        return true;
    }

    (long, long, long) compare(long a, long b) {
        long delta = b - a;
        long absDelta = long.Abs(delta);
        long sign = long.Sign(delta);

        return (delta, absDelta, sign);
    }

    bool isSafe(long absDelta, long sign, long targetSign) {
        return absDelta >= 1 && absDelta <= 3 && sign == targetSign;
    }

    private List<List<long>> parse(string[] lines)
    {
        string pattern = "\\s+";
        List<List<long>> result = new List<List<long>>();

        foreach (string line in lines)
        {
            var parts = Regex.Split(line, pattern);
            var numbers = parts.Select(long.Parse).ToList();

            result.Add(numbers);
        }
        return result;
    }
}