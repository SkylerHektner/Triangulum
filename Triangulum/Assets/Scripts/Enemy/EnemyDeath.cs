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
        // destroy the gameobject when done and end the CoRoutine
        Destroy(gameObject);
        yield return null;
    }
}
