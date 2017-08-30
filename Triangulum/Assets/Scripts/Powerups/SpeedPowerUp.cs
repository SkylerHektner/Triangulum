using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPowerUp : Powerup {

    public float speedMultiplier;

    public override void OnUse()
    {
        GameObject c = GameObject.FindWithTag("Player");
        c.GetComponent<PlayerMovement>().speed *= speedMultiplier;
        c.GetComponent<PlayerMovement>().acceleration *= speedMultiplier;
        base.OnUse();
    }

    public override void OnEnd()
    {
        GameObject c = GameObject.FindWithTag("Player");
        if (c != null)
        {
            c.GetComponent<PlayerMovement>().speed /= speedMultiplier;
            c.GetComponent<PlayerMovement>().acceleration /= speedMultiplier;
        }
        base.OnEnd();
    }
}
