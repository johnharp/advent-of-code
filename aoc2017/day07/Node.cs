using System.Diagnostics.Contracts;

public class Node
{
    private static Dictionary<string, Node> _AllNodes = new Dictionary<string, Node>();
    public static string? RootNodeId = null;

    public static void AddNode(Node n)
    {
        _AllNodes[n.Id] = n;
    }

    public static List<Node> AllNodes
    {
        get
        {
            List<Node> all = new List<Node>();

            foreach(var id in _AllNodes.Keys)
            {
                Node n = _AllNodes[id];
                all.Add(n);
            }

            return all;
        }
    }


    string? _ParentId = null;
    string _Id;
    int _Weight;
    int _TotalWeight = -1;

    List<string> _ChildIds;

    public string Id
    {
        get { return _Id; }
    }

    public List<Node> Children
    {
        get
        {
            List<Node> children = new List<Node>();
            foreach(var childid in _ChildIds)
            {
                Node n = _AllNodes[childid];
                children.Add(n);
            }
            return children;
        }
    }

    public string? ParentId
    {
        get { return this._ParentId; }
        set { _ParentId = value; }
    }

    public int Weight
    {
        get { return _Weight; }
    }

    public int TotalWeight
    {
        get
        {
            if (_TotalWeight == -1)
            {
                int childrenWeight = 0;
                foreach(string childid in _ChildIds)
                {
                    Node child = _AllNodes[childid];
                    childrenWeight += child.TotalWeight;
                }
                _TotalWeight = childrenWeight + _Weight;
            }
            return _TotalWeight;
        }
    }

    public List<int> ChildWeights
    {
        get
        {
            List<int> childWeights = new List<int>();
            foreach(Node child in Children)
            {
                childWeights.Add(child.Weight);
            }

            return childWeights;
        }
    }

    public List<int> ChildTotalWeights
    {
        get
        {
            List<int> totalWeights = new List<int>();

            foreach(Node child in Children)
            {
                totalWeights.Add(child.TotalWeight);
            }

            return totalWeights;
        }
    }

    public Node(string id, int weight, List<string> childIds)
    {
        _Id = id;
        _Weight = weight;
        _ChildIds = new List<string>(childIds);
    }

    public static void LinkParents()
    {
        foreach (var node in AllNodes)
        {
            foreach (var child in node.Children)
            {
                child.ParentId = node.Id;
            }
        }

        foreach (var node in AllNodes)
        {
            if (node.ParentId == null)
            {
                RootNodeId = node.Id;
            }
        }
    }
}