﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killCollidedEnemy : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Enemy")
        {
            collider.gameObject.GetComponent<EnemyDeath>().Die();
        }
    }
}
