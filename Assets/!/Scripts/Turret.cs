using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private Transform target;
    public float range = 15f;
    string enemyTag = "Enemy";
    public Transform turretHead;
    float rotationSpeed = 5f;

    private void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }
    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            float distanceFromEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if(distanceFromEnemy < shortestDistance)
            {
                shortestDistance = distanceFromEnemy;
                nearestEnemy = enemy;
            }
            if(nearestEnemy != null && shortestDistance <= range) 
            {
                target = nearestEnemy.transform;
            }
            else
            {
                target = null;
            }
        }
    }
    private void Update()
    {
        if (target == null) return;

        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(turretHead.rotation, lookRotation, Time.deltaTime * rotationSpeed).eulerAngles;
        turretHead.rotation = Quaternion.Euler(0f, rotation.y,  0f);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
