using UnityEngine;
using System.Collections.Generic;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float waypointThreshold = 0.2f;
    [SerializeField] private int startWaypointIndex = 21;
    [SerializeField] private int endWaypointIndex = 0;

    private List<Transform> path;
    private int pathIndex = 0;

    private void OnDestroy()
    {
        if (path != null)
        {
            path.Clear();
            path = null;
        }
    }

    private void Start()
    {
        path = WayPoints.GetPath(startWaypointIndex, endWaypointIndex);
        if (path == null || path.Count == 0)
        {
            Debug.LogError($"No valid path found from {startWaypointIndex} to {endWaypointIndex}!");
            Destroy(gameObject);
            return;
        }
        transform.position = path[0].position;
    }

    private void Update()
    {
        if (path == null || pathIndex >= path.Count)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 targetPosition = path[pathIndex].position;
        Vector3 directionToTarget = targetPosition - transform.position;
        directionToTarget.y = 0;

        if (directionToTarget.magnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            transform.position += transform.forward * speed * Time.deltaTime;
        }

        if (Vector3.Distance(transform.position, targetPosition) < waypointThreshold)
        {
            pathIndex++;
        }
    }

    private void OnDrawGizmos()
    {
        if (path != null && path.Count > 0)
        {
            Gizmos.color = Color.red;
            for (int i = 0; i < path.Count - 1; i++)
            {
                if (path[i] != null && path[i + 1] != null)
                {
                    Gizmos.DrawLine(path[i].position, path[i + 1].position);
                }
            }
        }
    }
}