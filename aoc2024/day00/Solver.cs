class Solver
{
    public long part1(string[] lines)
    {
        (List<long> list1, List<long> list2) = parse(lines);

        return list1.Length + list2.Length;
    }

    public long part2(string[] lines)
    {
        (List<long> list1, List<long> list2) = parse(lines);

        return list1.Length + list2.Length;
    }

    private (List<long>, List<long>) parse(string[] lines)
    {
        List<long> list1 = new List<long>();
        List<long> list2 = new List<long>();

        return (list1, list2);
    }
}