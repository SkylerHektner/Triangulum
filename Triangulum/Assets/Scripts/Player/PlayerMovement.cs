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

    /// <summary>
    /// used to know if the character has been accelerated by the Speed Power Up
    /// </summary>
    public bool accelerated = false;

    // privates
    private Rigidbody2D body;

    private Vector3 prevPosition;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        prevPosition = transform.localPosition;
    }
	
	void Update ()
    {
        // tell the rigidBody to update the position based on the movement detected
        if (body.velocity.magnitude < speed)
            body.AddForce(new Vector2(Input.GetAxis("Horizontal") * Time.deltaTime * acceleration, 
                Input.GetAxis("Vertical") * Time.deltaTime * acceleration));

        // rotate the character to face their direction of movement
        Vector3 facing = transform.localPosition - prevPosition;
        transform.up = Vector3.Lerp(facing, transform.up, Time.deltaTime);
        prevPosition = transform.localPosition;
    }

    public void applyTempSpeedChange(float multiplier, float duration)
    {
        speed *= multiplier;
        StartCoroutine(slowTimer(duration, multiplier));
    }

    IEnumerator slowTimer(float delay, float multiplier)
    {
        yield return new WaitForSeconds(delay);
        speed /= multiplier;
    }
}
