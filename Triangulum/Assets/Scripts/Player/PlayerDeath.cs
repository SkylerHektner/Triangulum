using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour {

    public GameObject deathCanvas;
    public GameObject deathAnim;

    /// <summary>
    /// determins if you disable the wave manager when the player dies
    /// </summary>
    public bool disableWaveManagerOnDeath = true;

    public bool invincible = false;

    public int Health;

    public AudioClip deathSound;

	void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.tag == "Enemy" && !invincible)
        {
            takeDamage();
        }
    }

    private void takeDamage()
    {
        Health -= 1;
        if (Health == 0)
        {
            die();
        }
    }

    private void die()
    {
        // find an kill all drones just for thorougness sake
        GameObject[] drones;
        drones = GameObject.FindGameObjectsWithTag("Drone");
        for (int i = 0; i < drones.Length; i++)
        {
            Destroy(drones[i]);
        }

        // spawn the death animation
        GameObject d = GameObject.Instantiate(deathAnim);
        d.transform.localPosition = transform.localPosition;

        // Add score to money
        upgradeLoader.data.Player_TaxPayerDollars += ScoreManager.Instance.score;
        upgradeLoader.Instance.SaveData();

        // Disable the player 
        gameObject.GetComponent<PlayerMovement>().enabled = false;
        gameObject.GetComponentInChildren<BeaconManager>().enabled = false;
        gameObject.GetComponent<TeleportAbility>().enabled = false;
        gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
        gameObject.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().enabled = false;

        // bring up death canvas
        Instantiate(deathCanvas);

        // disable wave manager
        if (disableWaveManagerOnDeath)
        {
            GameObject.Find("WaveManager").SetActive(false);
        }

        // play death sound and stop music
        try
        {
            GameObject.FindGameObjectWithTag("SoundTrack").GetComponent<AudioSource>().Stop();
            gameObject.GetComponent<AudioSource>().PlayOneShot(deathSound);
        }
        catch
        {
            Debug.Log("Do you have a soundtrack in the scene with \"SoundTrack\" tag?");
        }
        
        
    }
}
