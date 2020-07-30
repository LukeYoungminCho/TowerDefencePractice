using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBall : MonoBehaviour
{
    public float speed = 10f;
    
    private int wavePointIndex = 0;
    public int hp;
    public int moneyValue;
    public GameObject emenyDeathEffect;
    private Transform targetWayPoint;
    private WaveSpawner waveSpawner;

    private void Start()
    {
        targetWayPoint = WayPoints.points[0];
        waveSpawner = WaveSpawner.instance;
    }

    private void Update()
    {
        Vector3 dir = targetWayPoint.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
        
        if (Vector3.Distance(transform.position, targetWayPoint.position) <= 0.2f)
        {
            GetNextWayPoint();
        }

    }

    void GetNextWayPoint()
    {
        if (wavePointIndex >= WayPoints.points.Length - 1)
        {
            EndPath();
            return;
        }
        wavePointIndex++;
        targetWayPoint = WayPoints.points[wavePointIndex];

        
    }

    void EndPath()
    {
        PlayerStats.lives--;
        Destroy(gameObject);

        if (PlayerStats.lives <= 0)
        {
            waveSpawner.GameOver();
        }
    }

    public void TakeDamage(int DamageAmount)
    {
        hp -= DamageAmount;
        Debug.Log("hp:" + hp);
        if (hp <= 0)
        {
            Die();
        }
            
    }

    private void Die()
    {
        Debug.Log("Enemy ball dead, you got " + moneyValue);
   
        GameObject emenyDeathEffectInctance = (GameObject)Instantiate(emenyDeathEffect, transform.position, Quaternion.identity);
        Destroy(emenyDeathEffectInctance, 5f);
        PlayerStats.money += moneyValue;
        Destroy(gameObject);
    }
}
