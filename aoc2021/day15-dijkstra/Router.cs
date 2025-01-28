using System;
using System.Linq;
using System.Collections.Generic;

namespace day15_dijkstra
{
    public class Router
    {
        public List<Node> NonFinalNodes = new List<Node>();

        public Router()
        {
        }

        public void InitializeNonFinalNodesList()
        {
            NonFinalNodes = new List<Node>(Node.NodeIndex.Values);
            SortNonFinalNodesList();
        }

        public void SortNonFinalNodesList()
        {
            NonFinalNodes = NonFinalNodes
                .Where(x => !x.IsFinal)
                .OrderBy(x => x.RiskSum)
                .ToList();
        }

        public Node SelectOneNonFinalNode()
        {
            Node n = null;

            if (NonFinalNodes.Count > 0)
            {
                n = NonFinalNodes.First();
            }

            return n;
        }

        public void Eval(Node n)
        {         
            var neighbors = n.NonFinalNeighbors();
            foreach(var neighbor in neighbors)
            {
                int risk = n.RiskSum + neighbor.Risk;

                if (risk < neighbor.RiskSum)
                {
                    neighbor.RiskSum = risk;
                }

                NonFinalNodes.Remove(neighbor);
                InsertNonFinalNodeInOrder(neighbor);
            }

            n.IsFinal = true;
            NonFinalNodes.Remove(n);
        }


        // This is slow and needs to be optimized!
        // this is requireing a scan through the list each iteration
        private void InsertNonFinalNodeInOrder(Node n)
        {
            for (int i = 0; i < NonFinalNodes.Count; i++)
            {
                if (NonFinalNodes[i].RiskSum > n.RiskSum)
                {
                    NonFinalNodes.Insert(i, n);
                    return;
                }
            }

            NonFinalNodes.Add(n);
        }

        public void Route()
        {
            Node n = SelectOneNonFinalNode();

            while (n != null)
            {
                Eval(n);
                Console.WriteLine($"{NonFinalNodes.Count}");
                n = SelectOneNonFinalNode();
            }

            Console.WriteLine($"Risk sum: {Node.FinishNode.RiskSum}");
        }
    }
}
