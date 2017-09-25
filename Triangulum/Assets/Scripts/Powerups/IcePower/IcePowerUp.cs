using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcePowerUp : Powerup 
{
    // pointer to the freeze ring prefab
    public GameObject freezeRing;

    public float freezeDuration;
    public float freezeRadius;
    public float freezeExpansionTime;


    public override void OnUse()
    {
        GameObject f = GameObject.Instantiate(freezeRing);
        f.transform.localPosition = transform.localPosition;
        FreezeRing r = f.GetComponent<FreezeRing>();
        r.freezeDuration = freezeDuration;
        r.maxRadius = freezeRadius;
        r.timeToMaxRadius = freezeExpansionTime;
        setHUDTimer(freezeDuration);
        base.OnUse();
    }

    public override void OnEnd()
    {
        base.OnEnd();
    }
}
