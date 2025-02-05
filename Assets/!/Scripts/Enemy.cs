using UnityEngine;

public class Enemy : MonoBehaviour
{
    float speed = 10f;
    private Transform target;
    private int wayPointIndex = 0;
    private void Start()
    {
        target = WayPoints.points[0];
    }
    private void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) < 0.2f)
        {
            GetNextWayPoint();
        }
    }

    private void GetNextWayPoint()
    {
        if (wayPointIndex >= WayPoints.points.Length - 1)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            wayPointIndex++;
            target = WayPoints.points[wayPointIndex];
        }
    }
}


