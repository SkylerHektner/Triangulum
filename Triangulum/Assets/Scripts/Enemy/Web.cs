using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Web : MonoBehaviour {

    public float slowMultiplier;
    public float slowDuration;

    // the duration of time it takes for the web to expand to its full size
    public float growTime = 2f;
    // the target scale for the web
    public float endScale = 2f;
    // The number of frames in the growth animation
    public int growthIncrements = 100;

    void Start()
    {
        StartCoroutine(grow());
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerMovement>().applyTempSpeedChange(slowMultiplier, slowDuration);
            Destroy(gameObject);
        }
    }

    IEnumerator grow()
    {
        float scaleDif = endScale - transform.localScale.x;
        float scaleIncrement = scaleDif / growthIncrements;
        float delay = growTime / growthIncrements;

        for (int i = 0; i < growthIncrements; i++)
        {
            transform.localScale = new Vector3(transform.localScale.x + scaleIncrement, transform.localScale.y + scaleIncrement, 0);
            yield return new WaitForSeconds(delay);
        }
    }
}
