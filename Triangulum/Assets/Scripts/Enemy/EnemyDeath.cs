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
    /// The base score value of the enemy
    /// </summary>
    public float baseScoreValue = 1;

    // pointers to all powerup prefabs
    public GameObject SpeedPower;
    public GameObject LassoPower;
    public GameObject LaserPower;
    public GameObject DronePower;
    public GameObject IcePower;
    public GameObject ShieldPower;

    /// <summary>
    /// used to keep track of if the enemy has already died once, ensuring no double loot drops
    /// </summary>
    private bool dead = false;

    public void Die()
    {
        if (!dead)
        {
            // set dead = true so we know Die() has already been called and can't be called again
            dead = true;
            // disable the collider and follow script so the player cannot still die to the mob
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            gameObject.GetComponent<ChasePlayer>().enabled = false;
            // Tell the score manager to add score
            ScoreManager.Instance.addScore(baseScoreValue);
            // start the death animation
            StartCoroutine(DeathCoRoutine());
            //find and stop the animator
            gameObject.GetComponent<Animate>().animating = false;
        }
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

        // try to spawn powerups
        if (upgradeLoader.data.SpeedPower_Unlocked)
        {
            if (Random.value < upgradeLoader.data.SpeedPower_DropChance)
            {
                GameObject p = GameObject.Instantiate(SpeedPower);
                p.transform.localPosition = transform.localPosition;
                p.transform.localPosition = p.transform.localPosition + new Vector3(Random.value, Random.value, Random.value);
                upgradeLoader.adjustPowerUp(p, "Speed");
            }
        }
        if (upgradeLoader.data.LassoPower_Unlocked)
        {
            if (Random.value < upgradeLoader.data.LassoPower_DropChance)
            {
                GameObject p = GameObject.Instantiate(LassoPower);
                p.transform.localPosition = transform.localPosition;
                p.transform.localPosition = p.transform.localPosition + new Vector3(Random.value, Random.value, Random.value);
                upgradeLoader.adjustPowerUp(p, "Lasso");
            }
        }
        if (upgradeLoader.data.LaserPower_Unlocked)
        {
            if (Random.value < upgradeLoader.data.LaserPower_DropChance)
            {
                GameObject p = GameObject.Instantiate(LaserPower);
                p.transform.localPosition = transform.localPosition;
                p.transform.localPosition = p.transform.localPosition + new Vector3(Random.value, Random.value, Random.value);
                upgradeLoader.adjustPowerUp(p, "Laser");
            }
        }
        if (upgradeLoader.data.DronePower_Unlocked)
        {
            if (Random.value < upgradeLoader.data.DronePower_DropChance)
            {
                GameObject p = GameObject.Instantiate(DronePower);
                p.transform.localPosition = transform.localPosition;
                p.transform.localPosition = p.transform.localPosition + new Vector3(Random.value, Random.value, Random.value);
                upgradeLoader.adjustPowerUp(p, "Drone");
            }
        }
        if (upgradeLoader.data.IcePower_Unlocked)
        {
            if (Random.value < upgradeLoader.data.IcePower_DropChance)
            {
                GameObject p = GameObject.Instantiate(IcePower);
                p.transform.localPosition = transform.localPosition;
                p.transform.localPosition = p.transform.localPosition + new Vector3(Random.value, Random.value, Random.value);
                upgradeLoader.adjustPowerUp(p, "Ice");
            }
        }
        if (upgradeLoader.data.ShieldPower_Unlocked)
        {
            if (Random.value < upgradeLoader.data.ShieldPower_DropChance)
            {
                GameObject p = GameObject.Instantiate(ShieldPower);
                p.transform.localPosition = transform.localPosition;
                p.transform.localPosition = p.transform.localPosition + new Vector3(Random.value, Random.value, Random.value);
                upgradeLoader.adjustPowerUp(p, "Shield");
            }
        }

        // destroy the gameobject when done and end the CoRoutine
        Destroy(gameObject);
        yield return null;
    }
}
