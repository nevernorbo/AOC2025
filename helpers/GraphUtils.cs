namespace Helpers;

class Edge
{
    public int Src { get; set; }
    public int Dest { get; set; }
    public float Weight { get; set; }

    public Edge(int src, int dest, float weight)
    {
        Src = src;
        Dest = dest;
        Weight = weight;
    }

    public override string ToString()
    {
        return $"{Src} - {Dest}  ({Weight})";
    }
}


class Graph
{
    public int Vertices { get; set; }
    public List<Edge> Edges { get; set; }
    public DisjointSet DisjointSet { get; set; }

    public Graph(int vertices)
    {
        Vertices = vertices;
        Edges = new List<Edge>();
        DisjointSet = new DisjointSet(Vertices);
    }

    public void AddEdge(Edge newEdge)
    {
        Edges.Add(newEdge);
    }

    public IEnumerable<Edge> KruskalMST()
    {
        var result = new List<Edge>();

        foreach (var edge in Edges)
        {
            if (DisjointSet.Find(edge.Src) != DisjointSet.Find(edge.Dest))
            {
                result.Add(edge);
                DisjointSet.Union(edge.Src, edge.Dest);
            }
        }

        return result;
    }
}
