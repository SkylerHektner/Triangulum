using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPowerUp : Powerup
{
    public int durability;
    public float radius;
    public bool unbreakable;
    public float flickerDuration;

    /// <summary>
    /// must be set to the instance of the player shield prefab
    /// </summary>
    public GameObject playerShield;

    private GameObject shield;

    public override void OnUse()
    {
        // create the shield and modify it's settings 
        shield = GameObject.Instantiate(playerShield);
        Shield s = shield.GetComponent<Shield>();
        s.durability = durability;
        s.flickerDuration = flickerDuration;
        s.caller = this;
        shield.GetComponent<CircleCollider2D>().radius = radius;
        shield.GetComponent<CircleCollider2D>().isTrigger = !unbreakable;
        //shield.GetComponent<Rigidbody2D>().simulated = unbreakable;
        shield.transform.parent = GameObject.FindGameObjectWithTag("Player").transform;
        shield.transform.localPosition = Vector3.zero;

        // ensure the player collider and shield collider ignore eachother
        CircleCollider2D col1 = GameObject.FindGameObjectWithTag("Player").GetComponent<CircleCollider2D>();
        CircleCollider2D col2 = shield.GetComponent<CircleCollider2D>();
        Physics2D.IgnoreCollision(col1, col2, true);

        // set HUD timer and call base onUse
        setHUDTimer();
        base.OnUse();
    }

    public override void OnEnd()
    {
        shield.GetComponent<Shield>().RequestEnd();
        removeHUDTimer();
        base.OnEnd();
    }
}
