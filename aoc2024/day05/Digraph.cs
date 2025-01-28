using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;

namespace day05;

public class Digraph
{
    private int _V; // number of verts
    private int _E; // number of edges
    private readonly Dictionary<int, HashSet<int>>_adj;

    public Digraph()
    {
        _V = 0;
        _E = 0;

        _adj = new Dictionary<int, HashSet<int>>();
    }

    public int V()
    {
        return _V;
    }

    public int E()
    {
        return _E;
    }

    public void AddEdge(int v, int w)
    {
        if (!_adj.Keys.Contains(v))
        {
            _adj.Add(v, new HashSet<int>());
            _V++;
        }

        if (!_adj.ContainsKey(w))
        {
            _adj.Add(w, new HashSet<int>());
            _V++;
        }

        _adj[v].Add(w);
        _E++;
    }

    public HashSet<int> Adj(int v)
    {
        return _adj[v];
    }

    public Digraph Reverse()
    {
        Digraph R = new Digraph();
        for (int v = 0; v < _V; v++)
        {
            foreach(int w in _adj[v])
            {
                R.AddEdge(w, v);
            }
        }
        return R;
    }

    public bool TestPath(List<int> path)
    {
        for (int i = 0; i < path.Count - 1; i++)
        {
            int v = path[i];
            int w = path[i + 1];
            
            if (!_adj[v].Contains(w)) return false;
        }
        return true;
    }

    private void TopologicalSortUtil(int v, HashSet<int> visited, Stack<int> stack)
    {
        visited.Add(v);
        foreach (var neightbor in _adj[v])
        {
            if (!visited.Contains(neightbor))
            {
                TopologicalSortUtil(neightbor, visited, stack);
            }
        }
        stack.Push(v);
    }

    public List<int> TopologicalSort()
    {
        Stack<int> stack = new Stack<int>();
        HashSet<int> visited = new HashSet<int>();

        foreach (int v in _adj.Keys)
        {
            if (!visited.Contains(v))
            {
                TopologicalSortUtil(v, visited, stack);
            }
        }

        List<int> sortedNodeOrder = new List<int>();
        while (stack.Count > 0)
        {
            sortedNodeOrder.Add(stack.Pop());
        }

        return sortedNodeOrder;
    }
}