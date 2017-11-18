using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseAndThrow : MonoBehaviour
{


    // The speed of the enemy chasing the player in units per second
    public float speed = 30f;
    // the speed of the projectile thrown at the player
    public float projectileSpeed = 30f;
    // the duration the projectile lasts for
    public float projectileDuration = 4f;
    // the range the mob has to be at before it throws
    public float throwRange = 60f;
    // the duration of time it takes the mob to throw
    public float throwDuration = .5f;
    // the frames used in the throw animation
    public Sprite[] throwFrames;
    // the projectile prefab thrown by the monster
    public GameObject projectile;


    private Transform playerTransform;
    private Rigidbody2D body;
    private bool throwing = false;

    void Start()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;
        body = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (playerTransform != null)
        {
            transform.up = playerTransform.position - transform.position;

            if ((transform.localPosition - playerTransform.localPosition).magnitude > throwRange && !throwing)
            {
                body.MovePosition(Vector2.MoveTowards(transform.localPosition, playerTransform.localPosition, speed * Time.deltaTime));
            }
            else if (!throwing)
            {
                body.velocity = Vector2.zero;
                ThrowProjectile();
            }
        }
    }

    void ThrowProjectile()
    {
        throwing = true;
        StartCoroutine(throwAnimation());
    }

    IEnumerator throwAnimation()
    {
        // disable the main animation to allow for the throw animation
        gameObject.GetComponent<Animate>().animating = false;

        // find the delay and animation through the throw animation
        float delay = throwDuration / throwFrames.Length;
        SpriteRenderer r = gameObject.GetComponent<SpriteRenderer>();
        for (int i = 0; i < throwFrames.Length; i++)
        {
            r.sprite = throwFrames[i];
            yield return new WaitForSeconds(delay);
        }

        // throw the projectile
        GameObject proj = GameObject.Instantiate(projectile);
        proj.transform.localPosition = transform.localPosition;
        FollowVector v = proj.GetComponent<FollowVector>();
        v.speed = projectileSpeed;
        v.vec = playerTransform.localPosition - transform.localPosition;
        proj.GetComponent<RemoveSelf>().timeTillRemove = projectileDuration;

        // reset the throwing bool
        throwing = false;
        // enable regular animation again
        gameObject.GetComponent<Animate>().animating = true;
    }
}
