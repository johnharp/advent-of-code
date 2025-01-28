namespace day04;
class Program
{
    static long NumFullyContained = 0;
    static long NumIntersecting = 0;

    static void Main(string[] args)
    {
        String[] lines = File.ReadAllLines("../../../input.txt");

        foreach(var line in lines)
        {
            HandleLine(line);
        }

        Console.WriteLine($"Fully contained count: {NumFullyContained}");
        Console.WriteLine($"Any overlap count: {NumIntersecting}");
    }

    static void HandleLine(String line)
    {
        // Given something line this:
        //   2-4,6-8
        // split into two strings:
        // "2-4", and "6-8"
        string[] parts = line.Split(",");

        (long, long) range1 = Range(parts[0]);
        (long, long) range2 = Range(parts[1]);

        (long, long) overlap = Intersect(range1, range2);

        if (Length(overlap) > 0)
        {
            NumIntersecting += 1;

            if (Length(overlap) == Length(range1) ||
             Length(overlap) == Length(range2))
            {
                NumFullyContained += 1;
            }
        }
    }

    /// <summary>
    /// Represent ranges as a tuple of (start: long, end: long)
    /// if start is <= end it is a valid range
    /// if start > end it is not a valid range
    /// </summary>
    /// <param name="s"></param>
    /// <returns>a tuple long,long</returns>
    static (long, long) Range(String s)
    {
        string[] parts = s.Split("-");
        long start = long.Parse(parts[0]);
        long end = long.Parse(parts[1]);

        return (start, end);
    }

    static long Length((long, long) r)
    {
        return r.Item2 - r.Item1 + 1;
    }

    static (long, long) Intersect((long, long) r1, (long, long) r2)
    {
        long maxOfStarts = r1.Item1 > r2.Item1 ? r1.Item1 : r2.Item1;
        long minOfEnds = r1.Item2 < r2.Item2 ? r1.Item2 : r2.Item2;

        return (maxOfStarts, minOfEnds);
    }
}

