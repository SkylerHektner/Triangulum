using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPowerUp : Powerup {

    public float speedMultiplier;

    public bool train;

    public float trainRadius;

    private GameObject trainKillField;

    /// <summary>
    /// Get a pointer to the movement script since we will use it a lot
    /// </summary>
    PlayerMovement movementScript;
    void Start()
    {
        movementScript = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
    }

    public override void OnUse()
    {
        // if for any reason the pointer to movement script is null, get it again.
        if (movementScript == null)
        {
            movementScript = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
        }

        // only allow use of the powerup if the player has not used a speed power up already
        if (!movementScript.accelerated)
        {
            movementScript.speed *= speedMultiplier;
            movementScript.acceleration *= speedMultiplier;
            movementScript.accelerated = true;

            if (train)
            {
                GameObject p = GameObject.FindGameObjectWithTag("Player");
                p.GetComponent<PlayerDeath>().invincible = true;

                GameObject k = new GameObject();
                k.AddComponent<RemoveSelf>().timeTillRemove = duration;
                k.AddComponent<CircleCollider2D>().radius = trainRadius;
                k.GetComponent<CircleCollider2D>().isTrigger = true;
                k.AddComponent<killCollidedEnemy>();
                k.transform.parent = p.transform;
                k.transform.localPosition = Vector3.zero;

                trainKillField = k;
            }

            setHUDTimer();
            base.OnUse();
        }
    }

    public override void OnEnd()
    {
        // if for any reason the pointer to movement script is null, get it again.
        if (movementScript == null)
        {
            movementScript = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
        }

        // undo the speed power up
        movementScript.speed /= speedMultiplier;
        movementScript.acceleration /= speedMultiplier;
        movementScript.accelerated = false;

        if (train)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerDeath>().invincible = false;
            Destroy(trainKillField);
        }

        base.OnEnd();
    }
}
