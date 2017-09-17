using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour {

    public GameObject deathCanvas;

    /// <summary>
    /// determins if you disable the wave manager when the player dies
    /// </summary>
    public bool disableWaveManagerOnDeath = true;

	void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.tag == "Enemy")
        {
            Die();
        }
    }

    private void Die()
    {
        // find an kill all drones just for thorougness sake
        GameObject[] drones;
        drones = GameObject.FindGameObjectsWithTag("Drone");
        for (int i = 0; i < drones.Length; i++)
        {
            Destroy(drones[i]);
        }

        // Destroy the player 
        Destroy(gameObject);
        Instantiate(deathCanvas);
        if (disableWaveManagerOnDeath)
        {
            GameObject.Find("WaveManager").SetActive(false);
        }

        
    }
}
