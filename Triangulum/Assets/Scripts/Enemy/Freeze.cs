using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freeze : MonoBehaviour {

    public Sprite freezeSprite;

    public bool freezable = true;

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

            s = new GameObject();
            s.name = "freezeSpriteHolder";
            s.transform.parent = transform;
            s.transform.localPosition = Vector3.zero;
            s.transform.localScale = transform.localScale / 3;
            SpriteRenderer r = s.AddComponent<SpriteRenderer>();
            r.sprite = freezeSprite;
            r.sortingLayerName = "Enemies";
            r.sortingOrder = 1;

            gameObject.GetComponent<ChasePlayer>().enabled = false;

            d = StartCoroutine(delay(duration));

            frozen = true;
        }
    }

    public void unFreezeEntity()
    {
        frozen = false;
        Destroy(s);
        gameObject.GetComponent<ChasePlayer>().enabled = true;
    }

    IEnumerator delay(float duration)
    {
        yield return new WaitForSeconds(duration);
        unFreezeEntity();
    }
}
