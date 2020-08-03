using UnityEngine;

public class TowerProjectile : MonoBehaviour
{
    private Transform target;
    public float speed = 30f;
    public GameObject turretBulletimpactEffectPrefab;
    public float explosionRadius;
    public int damage;
    
    public void Seek(Transform _target)
    {
        target = _target;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Destroy(this.gameObject);
            return;
        }

        Vector3 dir = target.position - this.transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if  (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target);
    }

    public virtual void HitTarget()
    {
        Debug.Log("we hit something");
        GameObject impactEffectObj = (GameObject)Instantiate(turretBulletimpactEffectPrefab, transform.position, transform.rotation);
        Destroy(impactEffectObj, 2f);

        if (explosionRadius > 0f)
        {
            Explode();
        }
        else
        {
            Damage(target);
        }

        Destroy(this.gameObject);

    }

    void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.tag == "Enemy")
            {
                Damage(collider.transform);
            }
        }
    }
    public virtual void Damage(Transform enemy)
    {
        Enemy e = enemy.GetComponent<Enemy>();
        if (e != null)
            e.TakeDamage(damage);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
