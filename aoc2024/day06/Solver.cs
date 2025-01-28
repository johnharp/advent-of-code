namespace day06;

using System.Text.RegularExpressions;
using System.Linq;
using System.Xml.Schema;
using System.Collections.Immutable;
using System.Data;

public class Solver
{
    AreaMap OriginalMap;

    public Solver(AreaMap map)
    {
        OriginalMap = map;
    }

    public long part1()
    {
        AreaMap map = new AreaMap(OriginalMap);
        while (map.IsOnTheMap())
        {
            map.Move();
        }

        return map.VisitedLocs.Count;
    }

    public long part2()
    {
        long loopsFound = 0;
        AreaMap map = new AreaMap(OriginalMap);

        // Solve it once to find the original path
        while (map.IsOnTheMap())
        {
            map.Move();
        }

        var locsToTry = map.VisitedLocs.ToList<(int, int)>();

        foreach (var loc in locsToTry)
        {
            AreaMap newmap = new AreaMap(OriginalMap);
            newmap.Obstructions.Add(loc);
            while (newmap.IsOnTheMap() && !newmap.LoopDetected) {
                newmap.Move();
            }

            if (newmap.LoopDetected) loopsFound++;
        }
        
        return loopsFound;
    }

}