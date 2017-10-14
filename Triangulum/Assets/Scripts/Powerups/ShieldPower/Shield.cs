using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour {

    public int durability;
    public float flickerDelay = .2f;
    public float flickerDuration;

    public bool unbreakable;

    public ShieldPowerUp caller;

    private bool flickering = false;
    private SpriteRenderer playerSprite;
    private SpriteRenderer shieldSprite;

    private bool endRequested = false;

    public void Start()
    {
        // get a couple pointers to the sprite renderes that we will use a lot later
        playerSprite = transform.parent.gameObject.GetComponentInChildren<SpriteRenderer>();
        shieldSprite = gameObject.GetComponentInChildren<SpriteRenderer>();

        if (unbreakable)
        {
            gameObject.GetComponent<CircleCollider2D>().isTrigger = false;
            transform.parent.gameObject.GetComponent<PlayerDeath>().invincible = true;
        }
    }

    // called when an enemy collides with our shield
	public void OnTriggerEnter2D(Collider2D col)
    {
        if (!flickering && col.tag == "Enemy")
        {
            durability -= 1;
            flickering = true;
            StartCoroutine(flicker());
        }       
    }

    // called by the powerUp that created us if it's time to end
    public void RequestEnd()
    {
        endRequested = true;
        if (!flickering)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator flicker()
    {

        // disable the players ability to die and collide with enemies
        transform.parent.gameObject.GetComponent<PlayerDeath>().enabled = false;
        transform.parent.gameObject.GetComponent<CircleCollider2D>().enabled = false;

        // flicker the sprites of the player on and off
        for (float i = flickerDuration; i > 0; i -= flickerDelay)
        {
            playerSprite.enabled = !playerSprite.enabled;
            shieldSprite.enabled = !shieldSprite.enabled;
            yield return new WaitForSeconds(flickerDelay);
        }
        playerSprite.enabled = true;
        shieldSprite.enabled = true;

        // re-enable the players ability to die and collide with enemies
        transform.parent.gameObject.GetComponent<PlayerDeath>().enabled = true;
        transform.parent.gameObject.GetComponent<CircleCollider2D>().enabled = true;

        flickering = false;

        // now check if that was our last durability and if it was call the end of the powerup
        if (durability == 0)
        {
            caller.OnEnd();
        }

        // if it was requested during this time that we end then Destroy ourselves now
        if (endRequested)
        {
            Destroy(gameObject);
        }
    }
}
