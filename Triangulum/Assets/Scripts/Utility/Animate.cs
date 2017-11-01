using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animate : MonoBehaviour {

    public Sprite[] frames;
    public float delayBetweenFrames;
    public bool animating = true;

    public bool loopCustomRange = false;
    public int[] customRange = new int[2];

    private SpriteRenderer r;
    private Coroutine AnimationCoroutine;

	void Start () {
        r = gameObject.GetComponent<SpriteRenderer>();
        AnimationCoroutine = StartCoroutine(animate());
	}

    void OnDisable()
    {
        StopCoroutine(AnimationCoroutine);
    }

    void OnEnable()
    {
        AnimationCoroutine = StartCoroutine(animate());
    }
	
	IEnumerator animate()
    {
        while (true)
        {
            int i = 0;
            int e = frames.Length;
            while (animating)
            {
                for (; i < e; i++)
                {
                    yield return new WaitForSeconds(delayBetweenFrames);
                    r.sprite = frames[i];

                    if (!animating)
                    {
                        break;
                    }
                }
                if (loopCustomRange)
                {
                    i = customRange[0];
                    e = customRange[1];
                }
                else
                    i = 0;
            }

            yield return new WaitForSeconds(.1f);
        }
    }
}
