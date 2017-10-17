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

        // Destroy the player 
        Destroy(gameObject);
        Instantiate(deathCanvas);
        if (disableWaveManagerOnDeath)
        {
            GameObject.Find("WaveManager").SetActive(false);
        }
        
    }
}
