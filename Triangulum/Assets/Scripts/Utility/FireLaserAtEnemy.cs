using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireLaserAtEnemy : MonoBehaviour {

    //MUST BE SET TO LASER PREFAB
    public GameObject laserPrefab;

    // cooldown before the drone can fire another laser
    public float coolDown = 5;

    private bool onCooldown = false;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (!onCooldown && col.gameObject.tag == "Enemy")
        {
            // create a laser and send it after the enemy
            GameObject laser = Instantiate(laserPrefab);
            laser.transform.localPosition = transform.localPosition;
            laser.GetComponent<Chase>().target = col.transform;

            // go on cooldown
            StartCoroutine(delay());
            onCooldown = true;
        }
    }


    IEnumerator delay()
    {
        yield return new WaitForSeconds(coolDown);
        onCooldown = false;
    }
}
