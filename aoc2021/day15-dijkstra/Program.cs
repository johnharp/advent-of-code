using System;

namespace day15_dijkstra
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = new PuzzleInput("input.txt");

            // Part 2 involves "tiling" the input across and down
            // 5 times.  The risk value of each node increases by 1
            // for each position change right or down.
            input.CreateNodes(numTiles: 5);

            //Node.DumpAllRiskValues();
            //Node.DumpAllRiskSums();

            Router r = new Router();
            r.InitializeNonFinalNodesList();
            r.Route();
        }
    }
}
