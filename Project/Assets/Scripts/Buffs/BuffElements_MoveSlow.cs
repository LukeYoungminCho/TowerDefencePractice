using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffElement_MoveSlow : BuffElements
{
    public float slowPercent;

    public override void OnActive(Enemy target)
    {
        base.OnActive(target);
        target.currentSpeed -= target.baseSpeed * slowPercent;
    }
    public override void OnDeactive(Enemy target)
    {
        base.OnDeactive(target);
        target.currentSpeed = target.baseSpeed;
    }
    public override void OnDuration(Enemy target)
    {
        base.OnDuration(target);
    }
}
