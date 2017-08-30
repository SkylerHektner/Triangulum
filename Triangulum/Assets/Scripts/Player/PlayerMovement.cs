using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Controls all movement for the player
/// </summary>
public class PlayerMovement : MonoBehaviour {

    /// <summary>
    /// The speed of the player in units per second
    /// </summary>
    public float speed = 10f;

    /// <summary>
    /// The rate the player accelerates
    /// </summary>
    public float acceleration = 100f;

    // privates
    private Rigidbody2D body;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }
	
	void Update ()
    {
        // tell the rigidBody to update the position based on the movement detected
        if (body.velocity.magnitude < speed)
            body.AddForce(new Vector2(Input.GetAxis("Horizontal") * Time.deltaTime * acceleration, 
                Input.GetAxis("Vertical") * Time.deltaTime * acceleration));
    }
}
