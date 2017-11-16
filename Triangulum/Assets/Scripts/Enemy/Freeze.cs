using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freeze : MonoBehaviour {

    public Sprite[] freezeSprites;

    public bool freezable = true;

    public bool LethalFreeze = false;

    private GameObject s;
    private Coroutine d;
    private bool frozen = false;

	public void FreezeEntity(float duration)
    {
        if (freezable)
        {
            // check if we have already been frozen. If we have, end that freeze and start the new one
            if (frozen)
            {
                unFreezeEntity();
                StopCoroutine(d);
            }

            // create as freeze overlay object
            s = new GameObject();
            s.name = "freezeSpriteHolder";
            s.transform.parent = transform;
            s.transform.localPosition = Vector3.zero;
            s.transform.localScale = transform.localScale;

            // add and configure its sprite renderer
            SpriteRenderer r = s.AddComponent<SpriteRenderer>();
            r.sprite = freezeSprites[0];
            r.sortingLayerName = "Enemies";
            r.sortingOrder = 1;

            // add and configure its animation
            Animate a = s.AddComponent<Animate>();
            a.frames = freezeSprites;
            a.delayBetweenFrames = duration / freezeSprites.Length;

            // ensure we stop chasing the player while frozen
            gameObject.GetComponent<ChasePlayer>().enabled = false;

            // start a delay before we thaw
            d = StartCoroutine(delay(duration));

            // make sure we know we are frozen incase we are frozen again
            frozen = true;

            // stop our regular animation so we don't spin in the ice block
            gameObject.GetComponent<Animate>().animating = false;
        }
    }

    public void unFreezeEntity()
    {
        frozen = false;
        Destroy(s);
        gameObject.GetComponent<ChasePlayer>().enabled = true;
        gameObject.GetComponent<Animate>().animating = true;
        if (LethalFreeze)
        {
            gameObject.GetComponent<EnemyDeath>().Die();
        }
    }

    IEnumerator delay(float duration)
    {
        yield return new WaitForSeconds(duration);
        unFreezeEntity();
    }
}
