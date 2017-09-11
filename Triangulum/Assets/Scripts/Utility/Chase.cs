using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : MonoBehaviour {

    /// <summary>
    /// The speed of the enemy chasing the target in units per second
    /// </summary>
    public float speed = 30f;

    public Transform target;

    public bool destroySelfIfTargetNull = false;

    void Update()
    {
        if (target != null)
        {
            transform.position = Vector2.MoveTowards(transform.localPosition, target.localPosition, speed * Time.deltaTime);
            transform.up = target.position - transform.position;
            
        }
        else if (destroySelfIfTargetNull)
        {
            Destroy(gameObject);
        }
    }
}
