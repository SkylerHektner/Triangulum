using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Powerup : MonoBehaviour {

    public static IEnumerator curDelayCycle;

    public float duration;

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
        if (curDelayCycle != null)
            StopCoroutine(curDelayCycle);
        curDelayCycle = delay(duration);
        StartCoroutine(curDelayCycle);
        GetComponent<CircleCollider2D>().enabled = false;
    }

    public virtual void OnEnd()
    {
        Destroy(gameObject);
    }

    public IEnumerator delay(float time)
    {
        yield return new WaitForSeconds(time);
        curDelayCycle = null;
        OnEnd();
    }
}
