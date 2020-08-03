using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerProjectileMissile : TowerProjectile
{
    public float burnDamage = 10f;
    public float burnTime = 5f;
    public BuffElements buffElements_Burn;

    public override void Damage(Transform enemy)
    {   
        base.Damage(enemy);
        // Dubuff_Burn
        StartCoroutine(BuffManager.ActiveBuff(enemy.gameObject.GetComponent<Enemy>(), buffElements_Burn)); 
        // Q. transform 에서 이렇게 enemy object를  가져오는게 괜찮은 방법인지..?
        
    }
}
