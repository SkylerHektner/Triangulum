using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAnimation : MonoBehaviour {

    public Sprite[] animationFrames;
    public float spawnDuration = 1;
    public bool thrower = false;
    public bool obstructor = false;

	void Start ()
    {
        // disable all movement, freeze, death, etc... scripts
		if (thrower)
        {
            gameObject.GetComponent<ChaseAndThrow>().enabled = false;
        }
        else if (obstructor)
        {
            gameObject.GetComponent<ObstuctorAI>().enabled = false;
        }
        else
        {
            gameObject.GetComponent<ChasePlayer>().enabled = false;
        }
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
        gameObject.GetComponent<Freeze>().enabled = false;
        gameObject.GetComponent<EnemyDeath>().enabled = false;
        gameObject.GetComponent<Animate>().enabled = false;
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);

        // create an object to hold the animation and configure
        GameObject anim = new GameObject();
        anim.AddComponent<SpriteRenderer>();
        anim.AddComponent<Animate>();
        anim.AddComponent<RemoveSelf>();
        anim.transform.SetParent(transform);

        anim.GetComponent<RemoveSelf>().timeTillRemove = spawnDuration;
        anim.GetComponent<Animate>().frames = animationFrames;
        anim.GetComponent<Animate>().delayBetweenFrames = spawnDuration / animationFrames.Length;
        anim.GetComponent<SpriteRenderer>().sortingLayerName = "Powerups";
        anim.transform.localPosition = Vector3.zero;

        // start fading in the enemy sprite
        StartCoroutine(FadeIn());
	}
	
    IEnumerator FadeIn()
    {
        SpriteRenderer enemySprite = gameObject.GetComponent<SpriteRenderer>();

        Color color = enemySprite.color;
        color.a = 0;
        enemySprite.color = color;

        float delayTime = spawnDuration / 50;

        for (float i = 0; i < 50; i++)
        {
            color.a = i / 50;
            enemySprite.color = color;
            yield return new WaitForSeconds(delayTime);
        }

        color.a = 1;
        enemySprite.color = color;

        if (thrower)
        {
            gameObject.GetComponent<ChaseAndThrow>().enabled = true;
        }
        else if (obstructor)
        {
            gameObject.GetComponent<ObstuctorAI>().enabled = true;
        }
        else
        {
            gameObject.GetComponent<ChasePlayer>().enabled = true;
        }
        gameObject.GetComponent<CircleCollider2D>().enabled = true;
        gameObject.GetComponent<Freeze>().enabled = true;
        gameObject.GetComponent<EnemyDeath>().enabled = true;
        gameObject.GetComponent<Animate>().enabled = true;

    }
}
