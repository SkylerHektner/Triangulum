using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeRing : MonoBehaviour {

    public float maxRadius;
    public float timeToMaxRadius;
    public float freezeDuration;

    private float timePassed;
    private float targetScale;
	// Use this for initialization
	void Start () {
        targetScale = maxRadius / gameObject.GetComponent<CircleCollider2D>().radius;
	}
	
	// Update is called once per frame
	void Update () {
        timePassed += Time.deltaTime;
        float scale = targetScale * timePassed / timeToMaxRadius;
        transform.localScale = new Vector3(scale, scale);
        if (timePassed >= timeToMaxRadius)
        {
            Destroy(gameObject);
        }
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            col.gameObject.GetComponent<Freeze>().FreezeEntity(freezeDuration);
        }
    }
}
