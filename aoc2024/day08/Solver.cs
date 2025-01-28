using System.Numerics;

public class Solver
{
    Model model;

    public Solver(Model m)
    {
        model = m;
    }

    public BigInteger part1()
    {
        HashSet<(int, int)> AllAntinodes = new HashSet<(int, int)> ();

        foreach (char c in model.AntennaDictionary.Keys)
        {
            var pairs = GetPairs(model.AntennaDictionary[c]);
            foreach(var pair in pairs)
            {
                var antinodes = Antinodes(pair.Item1, pair.Item2);
                foreach(var antinode in antinodes)
                {
                    if (IsOnTheBoard(antinode)) AllAntinodes.Add(antinode);
                }
            }
        }

        return AllAntinodes.Count;
    }



    public BigInteger part2()
    {
        HashSet<(int, int)> AllAntinodes = new HashSet<(int, int)> ();

        foreach (char c in model.AntennaDictionary.Keys)
        {
            var pairs = GetPairs(model.AntennaDictionary[c]);
            foreach(var pair in pairs)
            {
                var antinodes = AntinodesPart2(pair.Item1, pair.Item2);
                foreach(var antinode in antinodes)
                {
                    if (IsOnTheBoard(antinode)) AllAntinodes.Add(antinode);
                }
            }
        }

        return AllAntinodes.Count;
    }

    public bool IsOnTheBoard((int, int) pt)
    {
        return (pt.Item1 >= 0 && pt.Item1 < model.Width && 
            pt.Item2 >=0 && pt.Item2 < model.Height);
    }

    public List<((int, int),(int,int))> GetPairs(List<(int,int)> items)
    {
        List<((int,int),(int,int))> ret = new List<((int,int),(int,int))>();

        for (int i = 0; i < items.Count - 1; i++)
        {
            for (int j = i+1; j < items.Count; j++)
            {
                ret.Add((items[i], items[j]));
            }
        }

        return ret;
    }

    public IEnumerable<IEnumerable<(int, int)>> GetPermutations(IEnumerable<(int, int)> input, int length)
    {
        IEnumerable<IEnumerable<(int, int)>> ret;

        if (length < 1)
        {
            ret = new List<List<(int, int)>>();
        }
        else if (length == 1)
        {
            ret = input.Select(x => new List<(int, int)> { x });
        }
        else
        {
            ret = GetPermutations(input, length - 1)
            .SelectMany(x => input,
                (x1, x2) => x1.Concat(new List<(int, int)> { x2 }));
        }

        return ret;
    }

    public List<(int, int)> Antinodes((int, int) pt1, (int, int) pt2) {
        (int, int) dist = Distance(pt1, pt2);
        List<(int,int)> antinodes = new List<(int, int)>();

        antinodes.Add((pt1.Item1 - dist.Item1, pt1.Item2 - dist.Item2));
        antinodes.Add((pt2.Item1 + dist.Item1, pt2.Item2 + dist.Item2));

        return antinodes;
    }

    public List<(int, int)> AntinodesPart2((int, int) pt1, (int, int) pt2)
    {
        (int, int) dist = Distance(pt1, pt2);
        List<(int,int)> antinodes = new List<(int, int)>();

        (int, int) point = pt1;
        int i = 1;
        while (IsOnTheBoard(point))
        {
            antinodes.Add(point);

            point = (pt1.Item1 - (i*dist.Item1), pt1.Item2 - (i*dist.Item2));
            i++;
        }

        point = pt1;
        i = 1;
        while (IsOnTheBoard(point))
        {
            antinodes.Add(point);

            point = (pt1.Item1 + (i*dist.Item1), pt1.Item2 + (i*dist.Item2));
            i++;
        }

        return antinodes;
    }

    public (int, int) Distance((int, int) pt1, (int, int) pt2)
    {
        return ((pt2.Item1 - pt1.Item1), (pt2.Item2 - pt1.Item2));
    }


}