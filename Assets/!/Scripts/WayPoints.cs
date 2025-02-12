using System.Collections.Generic;
using UnityEngine;

public class WayPoints : MonoBehaviour
{
    private static Transform[] points;
    private static Graph waypointGraph;
    private static bool isInitialized = false;

    [System.Serializable]
    public class WaypointConnection
    {
        public int fromWaypoint;
        public int[] toWaypoints;
    }

    [SerializeField]
    private List<WaypointConnection> connections = new List<WaypointConnection>();

    private void Awake()
    {
        if (!isInitialized)
        {
            InitializeGraph();
            isInitialized = true;
        }
    }

    private void OnDestroy()
    {
        points = null;
        waypointGraph = null;
        isInitialized = false;
    }

    private void InitializeGraph()
    {
        points = new Transform[transform.childCount];
        waypointGraph = new Graph(transform.childCount);

        for (int i = 0; i < transform.childCount; i++)
        {
            points[i] = transform.GetChild(i);
        }

        foreach (var connection in connections)
        {
            if (connection.fromWaypoint < points.Length)
            {
                foreach (int toIndex in connection.toWaypoints)
                {
                    if (toIndex < points.Length)
                    {
                        waypointGraph.AddEdge(connection.fromWaypoint, toIndex);
                    }
                }
            }
        }
    }

    public static List<Transform> GetPath(int startIndex, int endIndex)
    {
        if (!isInitialized || points == null || startIndex < 0 || endIndex >= points.Length)
        {
            Debug.LogError($"Invalid path request: initialized={isInitialized}, start={startIndex}, end={endIndex}");
            return new List<Transform>();
        }

        var indices = waypointGraph.BFS(startIndex, endIndex);
        if (indices.Count == 0)
        {
            Debug.LogWarning($"No path found from {startIndex} to {endIndex}");
            return new List<Transform>();
        }

        return indices.ConvertAll(i => points[i]);
    }

    private void OnDrawGizmos()
    {
        if (transform.childCount == 0) return;

        for (int i = 0; i < transform.childCount; i++)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.GetChild(i).position, 0.5f);
            UnityEditor.Handles.Label(transform.GetChild(i).position + Vector3.up, i.ToString());
        }

        foreach (var connection in connections)
        {
            if (connection.fromWaypoint < transform.childCount)
            {
                Vector3 fromPos = transform.GetChild(connection.fromWaypoint).position;
                foreach (int toIndex in connection.toWaypoints)
                {
                    if (toIndex < transform.childCount)
                    {
                        Gizmos.color = Color.blue;
                        Vector3 toPos = transform.GetChild(toIndex).position;
                        Gizmos.DrawLine(fromPos, toPos);
                    }
                }
            }
        }
    }
}