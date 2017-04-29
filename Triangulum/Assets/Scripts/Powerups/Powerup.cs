using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Powerup : MonoBehaviour {

    public static IEnumerator curDelayCycle;

	void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            GetComponent<SpriteRenderer>().enabled = false;
            OnUse();
        }
    }

    public virtual void OnUse()
    {

    }

    public virtual void OnEnd()
    {
        
    }

    public IEnumerator delay(float time)
    {
        yield return new WaitForSeconds(time);
        curDelayCycle = null;
        OnEnd();
    }
}
