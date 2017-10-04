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

    /// <summary>
    /// becomes a reference to the powerUpTimer created
    /// </summary>
    private GameObject powerUpTimer;

    /// <summary>
    /// Called when the player enters the active collider of a powerup
    /// </summary>
    /// <param name="collider"></param>
	void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
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
}
