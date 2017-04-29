using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopPowerUp : Powerup {

    public float duration;

    public override void OnUse()
    {
        GameObject.Find("Survival Board").GetComponent<EdgeCollider2D>().enabled = false;
        curDelayCycle = delay(duration);
        StartCoroutine(curDelayCycle);
    }

    public override void OnEnd()
    {
        GameObject.Find("Survival Board").GetComponent<EdgeCollider2D>().enabled = true;
        Destroy(gameObject);
    }
}
