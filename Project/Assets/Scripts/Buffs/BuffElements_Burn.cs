using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffElements_Burn : BuffElements
{
    public float burnDamage;

    public override void OnActive(Enemy target)
    {
        base.OnActive(target);
        target.hp -= burnDamage * Time.deltaTime;
    }
    public override void OnDeactive(Enemy target)
    {
        base.OnDeactive(target);
        target.currentSpeed = target.baseSpeed;
    }
    public override void OnDuration(Enemy target)
    {
        base.OnDuration(target);
        target.hp -= burnDamage * Time.deltaTime;
    }
}
