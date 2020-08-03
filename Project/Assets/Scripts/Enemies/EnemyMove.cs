using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMove : MonoBehaviour
{
    private Enemy enemy;
    private WaveSpawner waveSpawner;
    private int wavePointIndex = 0;
    private Transform targetWayPoint;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
        waveSpawner = WaveSpawner.instance;
        targetWayPoint = WayPoints.points[0];        
    }

    private void Update()
    {
        Vector3 dir = targetWayPoint.position - transform.position;
        transform.Translate(dir.normalized * enemy.currentSpeed * Time.deltaTime, Space.World);

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
}
