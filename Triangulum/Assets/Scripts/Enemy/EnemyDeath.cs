using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour {

    /// <summary>
    /// speed of the death animation in seconds
    /// </summary>
    public float deathAnimSpeed = .3f;

    /// <summary>
    /// Pointer to the sprite images used in the death animation
    /// </summary>
    public Sprite[] deathFrames;

    /// <summary>
    /// Dropable Powerups List
    /// </summary>
    public GameObject[] Drops;

    /// <summary>
    /// Drop Chance 0%-100%
    /// </summary>
    public int dropChance = 10;

    public void Die()
    {
        // disable the collider and follow script so the player cannot still die to the mob
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
        gameObject.GetComponent<ChasePlayer>().enabled = false;
        // start the death animation
        StartCoroutine(DeathCoRoutine());
    }

    /// <summary>
    /// The death cycle followed by the mob
    /// </summary>
    /// <returns></returns>
    IEnumerator DeathCoRoutine()
    {
        // get a pointer to the sprite renderer
        SpriteRenderer rend = gameObject.GetComponent<SpriteRenderer>();
        // calculate the time per frame and store it in f
        float f = deathAnimSpeed / deathFrames.Length;
        // start a loop that updates the frame of the sprite
        for (int i = 0; i < deathFrames.Length; i++)
        {
            rend.sprite = deathFrames[i];
            yield return new WaitForSeconds(f);
        }

        // spawn a powerup if you have one
        if (Drops != null)
        {
            if (Random.Range(0, 100) < dropChance)
            {
                // select a random drop
                GameObject p = Drops[Random.Range(0, Drops.Length)];
                // instantiate that game object at the location of death
                p = GameObject.Instantiate(p);
                p.transform.localPosition = transform.localPosition;
            }
        }

        // destroy the gameobject when done and end the CoRoutine
        Destroy(gameObject);
        yield return null;
    }
}
