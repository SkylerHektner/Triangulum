using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animate : MonoBehaviour {

    public Sprite[] frames;
    public float delayBetweenFrames;
    public bool animating = true;

    private SpriteRenderer r;

	void Start () {
        r = gameObject.GetComponent<SpriteRenderer>();
        StartCoroutine(animate());
	}
	
	IEnumerator animate()
    {
        while (animating)
        {
            for (int i = 0; i < frames.Length; i++)
            {
                yield return new WaitForSeconds(delayBetweenFrames);
                r.sprite = frames[i];
            }
        }
    }
}
