using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;
    public GameObject bulletImpactParticle;
    float speed = 70f;

    public void Seek(Transform _target)
    {
        target = _target;
    }
    private void Update()
    {
        if(target == null)
        {
            Destroy(gameObject);
            return;
        }
        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if(dir.magnitude <= distanceThisFrame )
        {
            HitTarget();
            return;
        }
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);

    }
    void HitTarget()
    {
        GameObject effbi = (GameObject) Instantiate(bulletImpactParticle, transform.position, transform.rotation);
        Destroy(effbi, 1f);
        Destroy(gameObject);
    }
}
