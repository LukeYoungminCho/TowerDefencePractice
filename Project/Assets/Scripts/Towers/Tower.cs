using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    //-----------------------------------------------------------
    // Inspectors are controlled by TurretEditor
    //-----------------------------------------------------------
    public enum ProjectileType
    {
        Bullet = 0,
        Laser = 1
    }

    private Transform targetTransform;
    private Enemy targetEnemy;

    //[Header("General")]
    public float range = 5f;

    //[Header("Projectile Type")]
    public ProjectileType projectileType;

    //[Header("Projectile Type : Bullet")]
    private float fireCountdown = 0;
    public GameObject bulletPrefab;
    public float fireRate = 0.2f;


    //[Header("Projectile Type : Laser")]
    public int damageOverTime = 30;
    public float slowPercent = 0.5f;
    public LineRenderer lineRenderer;
    public ParticleSystem laserBeamImpactEffect;
    public Light laserBeamImpactLight;
    public BuffElements buffElementsMoveSlow;
    private Coroutine laserDubffCoroutin;
    //[Header("Unity Setup fields")]
    public string enemyTag = "Enemy";
    public Transform partToRotate;
    public Transform firePoint;
    public float turnSpeed = 10f;
    
    ////////////////////////////////////////////////////////////////
    
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        float longestDistance = 0;
        GameObject nearestEnemy = null;
        GameObject farestEnemy = null;
        foreach (GameObject EnemyBall in enemies )
        {
            float distanceToEnemy = Vector3.Distance(transform.position, EnemyBall.transform.position);
            // to get nearest enemy object
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = EnemyBall;
            }
            //// to get farest enemy object
            //if (distanceToEnemy > longestDistance)
            //{
            //   longestDistance = distanceToEnemy;
            //    farestEnemy = EnemyBall;
            //}
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            targetTransform = nearestEnemy.transform;
            targetEnemy = nearestEnemy.GetComponent<Enemy>();
        }
        else
        {
            targetTransform = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (targetTransform == null)
        {
            switch (projectileType)
            {
                case ProjectileType.Bullet:
                    break;
                case ProjectileType.Laser:
                    if (lineRenderer.enabled)
                    {
                        laserBeamImpactLight.enabled = false;
                        laserBeamImpactEffect.Stop();
                        lineRenderer.enabled = false;
                        if (laserDubffCoroutin != null)
                            StopCoroutine(laserDubffCoroutin);
                    }
                    break;
            }

            return;
        }            

        LockOnTarget();

        switch (projectileType)
        {
            case ProjectileType.Bullet:
                if (fireCountdown <= 0f)
                {
                    Shoot();
                    fireCountdown = 1f / fireRate;
                }
                fireCountdown -= Time.deltaTime;
                break;

            case ProjectileType.Laser:
                Laser();
                break;
        }
        
    }

    private void LockOnTarget()
    {
        // Target lock on
        Vector3 dir = targetTransform.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;// lookRotation.eulerAngles;////interpolate.  그냥 lookRotation.eulerAngles를 사용하면 끊김;        
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;   
        Gizmos.DrawWireSphere(transform.position, range);
        
    }
    private void Shoot()
    {
       
        GameObject bulletGo = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        TowerProjectile bullet = bulletGo.GetComponent<TowerProjectile>();

        if (bullet != null)
            bullet.Seek(targetTransform);
        
    }
    private void Laser()
    {
        if (targetEnemy != null)
        {
            //targetEnemy.TakeDamage(damageOverTime * Time.deltaTime);
            targetEnemy.TakeDamageByPercent(damageOverTime * Time.deltaTime);
            //targetEnemy.Slow(slowPercent);
            if (laserDubffCoroutin != null)
                laserDubffCoroutin = StartCoroutine(BuffManager.ActiveBuff(targetEnemy, buffElementsMoveSlow));
        }   

        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
            laserBeamImpactEffect.Play();
            laserBeamImpactLight.enabled = true;
        }
            
        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, targetTransform.position);

        Vector3 dir = firePoint.position - targetTransform.position;

        laserBeamImpactEffect.transform.position = targetTransform.position+dir.normalized * 0.5f;
        laserBeamImpactEffect.transform.rotation = Quaternion.LookRotation(dir);
        
    }
}

