using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour {

    // speed of the death animation in seconds
    public float deathAnimSpeed = .3f;
    // Pointer to the sprite images used in the death animation
    public Sprite[] deathFrames;
    // The base score value of the enemy
    public float baseScoreValue = 1;
    // Sound played when the enemy dies
    public AudioClip deathSound;
    // public bool to know if the enemy is a projetile throwing
    public bool projectileThrower = false;
    // public bool to know if the enemy is an obstructor
    public bool obstructor = false;


    // pointers to all powerup prefabs
    public GameObject SpeedPower;
    public GameObject LassoPower;
    public GameObject LaserPower;
    public GameObject DronePower;
    public GameObject IcePower;
    public GameObject ShieldPower;

    // used to keep track of if the enemy has already died once, ensuring no double loot drops
    private bool dead = false;

    public void Die()
    {
        if (!dead)
        {
            // set dead = true so we know Die() has already been called and can't be called again
            dead = true;
            // disable the collider and follow script so the player cannot still die to the mob
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            if (!projectileThrower && !obstructor)
            {
                gameObject.GetComponent<ChasePlayer>().enabled = false;
            }
            else if(projectileThrower)
            {
                gameObject.GetComponent<ChaseAndThrow>().enabled = false;
            }
            else if (obstructor)
            {
                gameObject.GetComponent<ObstuctorAI>().enabled = false;
            }
            // Tell the score manager to add score
            int scoreGained = ScoreManager.Instance.addScore(baseScoreValue);
            // start the death animation
            StartCoroutine(DeathCoRoutine());
            //find and stop the animator
            gameObject.GetComponent<Animate>().animating = false;
            //play death sound
            gameObject.GetComponent<AudioSource>().PlayOneShot(deathSound);

            // show the score popup animation
            GameObject scorePop;
            Vector3 position = transform.localPosition;
            position.z = -1;
            ObjectPoolsAccessor.instance.ScoreNotifierPool.requestObject(position,
                Quaternion.Euler(new Vector3(0, 0, 0)), out scorePop);
            scorePop.GetComponent<EnemyScoreNotifier>().setScoreValue(scoreGained);
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
