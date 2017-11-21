using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPowerUp : Powerup
{
    public int durability;
    public float radius;
    public bool unbreakable;
    public float flickerDuration;

    public AudioClip ShieldSound;

    /// <summary>
    /// must be set to the instance of the player shield prefab
    /// </summary>
    public GameObject playerShield;

    private GameObject shield;

    public override void OnUse()
    {
        // first figure out if the player currently has a shield
        // if they do, then don't let them use the power
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        for (int i = 0; i < p.transform.childCount; i++)
        {
            if (p.transform.GetChild(i).name == "Shield(Clone)")
            {
                return;
            }
        }


        // create the shield and modify it's settings 
        shield = GameObject.Instantiate(playerShield);
        Shield s = shield.GetComponent<Shield>();
        s.durability = durability;
        s.flickerDuration = flickerDuration;
        s.caller = this;
        s.unbreakable = unbreakable;
        shield.GetComponent<CircleCollider2D>().radius = radius;
        shield.transform.parent = GameObject.FindGameObjectWithTag("Player").transform;
        shield.transform.localPosition = Vector3.zero;

        // ensure the player collider and shield collider ignore eachother
        CircleCollider2D col1 = GameObject.FindGameObjectWithTag("Player").GetComponent<CircleCollider2D>();
        CircleCollider2D col2 = shield.GetComponent<CircleCollider2D>();
        Physics2D.IgnoreCollision(col1, col2, true);

        // make the player invincible
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerDeath>().invincible = true;

        // play the shield sound
        gameObject.GetComponent<AudioSource>().PlayOneShot(ShieldSound);

        // set HUD timer and call base onUse
        setHUDTimer();
        base.OnUse();
    }

    public override void OnEnd()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerDeath>().invincible = false;
        shield.GetComponent<Shield>().RequestEnd();
        removeHUDTimer();
        base.OnEnd();

        // make the player not invincible
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerDeath>().invincible = false;
    }
}
