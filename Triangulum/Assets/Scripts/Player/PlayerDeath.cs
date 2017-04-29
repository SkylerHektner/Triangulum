using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour {

    public GameObject deathCanvas;

	void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.tag == "Enemy")
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
        Instantiate(deathCanvas);
    }
}
