using System.Globalization;
using Helpers;

static class Day08
{
    public struct LongVector3
    {
        public long X { get; set; }
        public long Y { get; set; }
        public long Z { get; set; }

        public LongVector3(long x, long y, long z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }

    public static void Run()
    {
        var points = File.ReadLines("./data/day08.txt")
            .Select(line =>
            {
                var p = line.Split(',');
                return new LongVector3(
                    long.Parse(p[0], CultureInfo.InvariantCulture),
                    long.Parse(p[1], CultureInfo.InvariantCulture),
                    long.Parse(p[2], CultureInfo.InvariantCulture)
                );
            })
            .ToList();

        // Console.WriteLine(SolvePart1(points, 1000));
        Console.WriteLine(SolvePart2(points, 1000));
    }

    public static long SolvePart1(List<LongVector3> points, int limit)
    {
        var graph = new Graph(points.Count);
        graph.Edges = GetEdges(points, limit);
        graph.KruskalMST();

        var networkSizes = new Dictionary<int, int>();
        for (int i = 0; i < points.Count; i++)
        {
            int root = graph.DisjointSet.Find(i);
            if (!networkSizes.ContainsKey(root))
                networkSizes[root] = 0;
            networkSizes[root]++;
        }

        var largestThree = networkSizes.Values
            .OrderByDescending(x => x)
            .Take(3)
            .ToList();

        return (long)largestThree[0] * largestThree[1] * largestThree[2];
    }

    public static long SolvePart2(List<LongVector3> points, int limit)
    {
        var graph = new Graph(points.Count);
        graph.Edges = GetEdges(points, limit);

        var edges = new List<Edge>();

        int componentCount = points.Count;

        foreach (var edge in graph.Edges)
        {
            if (graph.DisjointSet.Find(edge.Src) != graph.DisjointSet.Find(edge.Dest))
            {
                edges.Add(edge);
                graph.DisjointSet.Union(edge.Src, edge.Dest);
                componentCount--;

                if (componentCount == 1)
                {
                    var res = (long)points[edge.Src].X * points[edge.Dest].X;
                    return (long)res;
                }
            }
        }

        return 1;
    }

    private static List<Edge> GetEdges(List<LongVector3> points, int limit)
    {
        return Enumerable.Range(0, points.Count)
            .SelectMany(i => Enumerable.Range(i + 1, points.Count - (i + 1))
                .Select(j => new Edge(i, j, DistanceSquared(points[i], points[j]))))
            .OrderBy(x => x.Weight)
            // .Take(limit)
            .ToList();
    }

    private static long DistanceSquared(LongVector3 p, LongVector3 q)
    {
        long dx = p.X - q.X;
        long dy = p.Y - q.Y;
        long dz = p.Z - q.Z;
        return dx * dx + dy * dy + dz * dz;
    }
}
