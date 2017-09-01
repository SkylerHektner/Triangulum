﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LassoPowerUp : Powerup
{
    // get a pointer to the Beacon Manager since we will be using it a lot
    BeaconManager man;
    void Start()
    {
        man = GameObject.Find("Player").GetComponentInChildren<BeaconManager>();
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
        }
        
        base.OnUse();
    }

    public override void OnEnd()
    {
        GameObject.Find("Player").GetComponentInChildren<BeaconManager>().lassoModeActive = false;
        base.OnEnd();
    }
}