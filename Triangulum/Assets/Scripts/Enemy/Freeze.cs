using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freeze : MonoBehaviour {

    // array of sprites for the freeze animation
    public Sprite[] freezeSprites;
    // whether or not the enable is freezable
    public bool freezable = true;
    // whether or not the freeze kills the enemy at the end of it's duration
    public bool LethalFreeze = false;
    // whether or not the enemy is of the thrower type
    public bool thrower = false;
    // whether or not the enemy is of the obstructer
    public bool obstructer = false;

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
            if (!thrower && !obstructer)
            {
                gameObject.GetComponent<ChasePlayer>().enabled = false;
            }
            else if (thrower)
            {
                gameObject.GetComponent<ChaseAndThrow>().enabled = false;
                s.transform.localScale = new Vector3(2, 2, 1);
            }
            else if (obstructer)
            {
                gameObject.GetComponent<ObstuctorAI>().enabled = false;
                s.transform.localScale = new Vector3(2, 2, 1);
            }

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
        // toggle frozen off and destroy the freeze overlay
        frozen = false;
        Destroy(s);
        // re-enable the relevant AI script
        if (!thrower && !obstructer)
        {
            gameObject.GetComponent<ChasePlayer>().enabled = true;
        }
        else if (thrower)
        {
            gameObject.GetComponent<ChaseAndThrow>().enabled = true;
        }
        else if (obstructer)
        {
            gameObject.GetComponent<ObstuctorAI>().enabled = true;
        }
        // start animation again
        gameObject.GetComponent<Animate>().animating = true;
        // if the freeze was lethal, kill the enemy
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
