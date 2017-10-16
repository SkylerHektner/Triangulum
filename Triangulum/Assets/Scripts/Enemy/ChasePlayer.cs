using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasePlayer : MonoBehaviour {

    /// <summary>
    /// The speed of the enemy chasing the player in units per second
    /// </summary>
    public float speed = 30f;

    private Transform playerTransform;
    private Rigidbody2D body;

	void Start () {
        playerTransform = GameObject.FindWithTag("Player").transform;
        body = GetComponent<Rigidbody2D>();
	}
	
	void Update () {
        if (playerTransform != null)
        {
            body.MovePosition(Vector2.MoveTowards(transform.localPosition, playerTransform.localPosition, speed * Time.deltaTime));
            transform.up = playerTransform.position - transform.position;
        }
	}
}
