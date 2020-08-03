using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    public float hp;
    public float maxHp;
    public float baseSpeed = 10f;
    [HideInInspector]
    public float currentSpeed;
    public int worth;
    public GameObject emenyDeathEffect;
    public List<BuffElements> buffList = new List<BuffElements>();

    public void Start()
    {
        maxHp = hp;
        currentSpeed = baseSpeed;
        
    }
    private void Update()
    {
    }
    
    public void TakeDamage(float Damage)
    {
        hp -= Damage;
        if (hp <= 0f)
        {
            Die();
        }            
    }

    public void TakeDamageByPercent(float PercentDamage)
    {
        hp -= maxHp * PercentDamage / 100f;
        if (hp <= 0)
        {
            Die();
        }
    }

    public void TakeDamageWithPercent(float Damage, float PercentDamage)
    {
        hp -= Damage;
        hp -= maxHp * PercentDamage / 100f;
        if (hp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Enemy ball dead, you got " + worth);
   
        GameObject emenyDeathEffectInctance = (GameObject)Instantiate(emenyDeathEffect, transform.position, Quaternion.identity);
        Destroy(emenyDeathEffectInctance, 5f);
        PlayerStats.money += worth;
        Destroy(gameObject);
    }

    public void Slow(float slowPercent)
    {
        currentSpeed = baseSpeed * (1f - slowPercent);
    }

    
}
