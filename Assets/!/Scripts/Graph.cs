using System.Collections.Generic;

public class Graph
{
    private int V;
    private List<int>[] adj;
    private System.Random random;

    public Graph(int v)
    {
        V = v;
        adj = new List<int>[v];
        random = new System.Random();
        for (int i = 0; i < v; i++)
        {
            adj[i] = new List<int>();
        }
    }

    public void AddEdge(int v, int w)
    {
        adj[v].Add(w);
    }

    public List<int> BFS(int start, int end)
    {
        bool[] visited = new bool[V];
        int[] parent = new int[V];
        Queue<int> queue = new Queue<int>();

        visited[start] = true;
        queue.Enqueue(start);

        while (queue.Count != 0)
        {
            int v = queue.Dequeue();
            if (v == end) break;

            List<int> shuffledNeighbors = new List<int>(adj[v]);
            for (int i = shuffledNeighbors.Count - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);
                int temp = shuffledNeighbors[i];
                shuffledNeighbors[i] = shuffledNeighbors[j];
                shuffledNeighbors[j] = temp;
            }

            foreach (int next in shuffledNeighbors)
            {
                if (!visited[next])
                {
                    visited[next] = true;
                    parent[next] = v;
                    queue.Enqueue(next);
                }
            }
        }

        return ReconstructPath(parent, start, end);
    }

    private List<int> ReconstructPath(int[] parent, int start, int end)
    {
        List<int> path = new List<int>();
        int current = end;

        while (current != start)
        {
            path.Add(current);
            current = parent[current];
        }
        path.Add(start);
        path.Reverse();
        return path;
    }
}