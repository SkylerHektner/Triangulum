using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireLaserAtEnemy : MonoBehaviour {

    //MUST BE SET TO LASER PREFAB
    GameObject laserPrefab;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {

        }
    }
}
