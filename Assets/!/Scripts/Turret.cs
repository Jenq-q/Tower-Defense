using System.IO;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private Transform target;
    [Header("Atttributes")]
    public float range = 15f;
    float rotationSpeed = 5f;
    public float fireRate = 1f;
    float fireCountdown = 0f;

    [Header("UnityFields")]
    string enemyTag = "Enemy";
    public Transform turretHead;
    public GameObject bulletPrefab;
    public Transform firePosition;


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

        if(fireCountdown <= 0)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab,firePosition.position,firePosition.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();
        if (bullet != null) { bullet.Seek(target); }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
