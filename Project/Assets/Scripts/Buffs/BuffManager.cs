using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager : MonoBehaviour
{   
    public static BuffManager instance;

    private void Awake()
    {
        if (instance != null)
            return;
        instance = this;
    }
    
    public static IEnumerator ActiveBuff(Enemy target, BuffElements buffElements)
    {
        target.buffList.Add(buffElements);
        buffElements.OnActive(target);

        float currentTime = Time.time;
        while(Time.time - currentTime <= buffElements.duration)
        {
            buffElements.OnDuration(target);
            yield return null;
        }
        buffElements.OnDeactive(target);
        target.buffList.Remove(buffElements);        
    }
}
