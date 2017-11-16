using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Powerup : MonoBehaviour {

    /// <summary>
    /// used to determine if the powerup has been used
    /// </summary>
    public bool used = false;

    /// <summary>
    /// used for the duration of the powerup
    /// </summary>
    public float duration;

    // the amount of time before the powerup expires
    public float expirationTime = 20;

    /// <summary>
    /// becomes a reference to the powerUpTimer created
    /// </summary>
    private GameObject powerUpTimer;

    void Start()
    {
        StartCoroutine(expirationAnimation());
    }

    /// <summary>
    /// Called when the player enters the active collider of a powerup
    /// </summary>
    /// <param name="collider"></param>
	void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player" || collider.gameObject.tag == "Shield")
        {
			// call OnUse 
            OnUse();
        }
    }

    /// <summary>
    /// called when the powerup is used
    /// </summary>
    public virtual void OnUse()
    {
        // Disable the expiration timer co-routine
        StopAllCoroutines();
        // Make the powerup invisible and have no collider
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<CircleCollider2D>().enabled = false;
        // start the delay coroutine
        StartCoroutine(delay(duration));
        used = true;
    }

    /// <summary>
    /// called at the end of the duration of the powerup
    /// </summary>
    public virtual void OnEnd()
    {
        Destroy(gameObject);
    }

    public void setHUDTimer()
    {
        powerUpTimer = HUDManager.Instance.createPowerUpTimer(duration, gameObject.GetComponent<SpriteRenderer>().sprite);
    }

    public void setHUDTimer(float duration)
    {
        powerUpTimer = HUDManager.Instance.createPowerUpTimer(duration, gameObject.GetComponent<SpriteRenderer>().sprite);
    }

    public void removeHUDTimer()
    {
        HUDManager.Instance.removePowerUpTimer(powerUpTimer);
    }

    /// <summary>
    /// the coroutine for the delay cycle on the powerup
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    public IEnumerator delay(float time)
    {
        yield return new WaitForSeconds(time);
        OnEnd();
    }

    public IEnumerator expirationAnimation()
    {
        // pass the first two thirds of the expiration time 
        float timeRemaining = expirationTime;
        yield return new WaitForSeconds(expirationTime * .66f);
        timeRemaining -= expirationTime * .66f;

        SpriteRenderer s = gameObject.GetComponent<SpriteRenderer>();

        // start flashing until the remaining time is up
        while(timeRemaining > 0)
        {
            s.enabled = false;
            yield return new WaitForSeconds(.5f);
            s.enabled = true;
            yield return new WaitForSeconds(.5f);
            timeRemaining -= 1;
        }

        // destroy the powerup
        Destroy(gameObject);
    }
}
