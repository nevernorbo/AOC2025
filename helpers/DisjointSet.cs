namespace Helpers;

public class DisjointSet
{
    public int[] Parent { get; set; }
    public int[] Rank { get; set; }

    public DisjointSet(int n)
    {
        Parent = new int[n];
        Rank = new int[n];

        // Initialize each element as its own set
        for (int i = 0; i < n; i++)
        {
            Parent[i] = i;
            Rank[i] = 0;
        }
    }

    /*
        Recursively loops through the parents of x
        Returns the representative element of the set containing x (x in our specific case is a vertex of a graph)
        Uses path compression, making all nodes in path point to root
        https://en.wikipedia.org/wiki/Disjoint-set_data_structure#Finding_set_representatives
    */
    public int Find(int x)
    {
        if (Parent[x] != x)
        {
            Parent[x] = Find(Parent[x]);
        }

        return Parent[x];
    }

    /* <summary>
        Merges the sets containing elements x and y
        Uses union by rank for optimization
        Visualization (https://en.wikipedia.org/wiki/Disjoint-set_data_structure#/media/File:UnionFindKruskalDemo.gif)
    */
    public void Union(int x, int y)
    {
        var xRoot = Find(x);
        var yRoot = Find(y);

        if (xRoot == yRoot) return;

        // Union by rank: attach smaller rank tree under root of higher rank tree
        if (Rank[xRoot] < Rank[yRoot])
        {
            Parent[xRoot] = yRoot;
        }
        else if (Rank[xRoot] > Rank[yRoot])
        {
            Parent[yRoot] = xRoot;
        }
        else
        {
            Parent[yRoot] = xRoot;
            Rank[xRoot]++;
        }
    }

    // Checks if x and y are in the same set
    public bool Connected(int x, int y)
    {
        return Find(x) == Find(y);
    }
}
