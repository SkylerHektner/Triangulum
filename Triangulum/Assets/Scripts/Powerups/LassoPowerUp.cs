using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LassoPowerUp : Powerup
{
    // get a pointer to the Beacon Manager since we will be using it a lot
    BeaconManager man;

    public bool InstantLoop = false;
    public float InstantLoopRange = 60f;
    public float InstantLoopRadialDeviance = 30f;

    void Start()
    {
        man = GameObject.Find("Player").GetComponentInChildren<BeaconManager>();
        StartCoroutine(expirationAnimation());
    }

    public override void OnUse()
    {
        // check if a lasso powerup has already been used. If it has then don't let the player consume the power
        if (man.lassoModeActive)
        {
            return;
        }
        else
        {
            man.lassoModeActive = true;
            man.lassoInstantLoop = InstantLoop;
            man.instantLoopRange = InstantLoopRange;
            man.instantLoopRadialDeviance = InstantLoopRadialDeviance;
        }
        setHUDTimer();
        base.OnUse();
    }

    public override void OnEnd()
    {
        man.lassoModeActive = false;
        base.OnEnd();
    }
}
