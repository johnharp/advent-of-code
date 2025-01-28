using System.Text.RegularExpressions;

class Solver
{
    public long part1(string[] lines)
    {
        (List<long> list1, List<long> list2) = parse(lines);
        long diffSum = 0;
        list1.Sort();
        list2.Sort();

        long[] numbers1 = list1.ToArray();
        long[] numbers2 = list2.ToArray();

        for (int i = 0; i < numbers1.Length; i++)
        {
            long diff = Math.Abs(numbers1[i] - numbers2[i]);
            diffSum += diff;
        }
        return diffSum;
    }

    public long part2(string[] lines)
    {
        (List<long> list1, List<long> list2) = parse(lines);
        long sum = 0;

        Dictionary<int, int> rightDictionary = new Dictionary<int, int>();
        foreach (int value in list2)
        {
            if (rightDictionary.Keys.Contains(value))
            {
                rightDictionary[value]++;
            }
            else
            {
                rightDictionary[value] = 1;
            }
        }

        foreach (int value in list1)
        {
            if (rightDictionary.Keys.Contains(value))
            {
                sum += value * rightDictionary[value];
            }
        }
        return sum;
    }

    private (List<long>, List<long>) parse(string[] lines)
    {
        string pattern = "\\s+";
        List<long> list1 = new List<long>();
        List<long> list2 = new List<long>();

        foreach (string line in lines)
        {
            var parts = Regex.Split(line, pattern);
            var first = parts[0];
            var second = parts[1];

            list1.Add(long.Parse(first));
            list2.Add(long.Parse(second));
        }
        return (list1, list2);
    }
}