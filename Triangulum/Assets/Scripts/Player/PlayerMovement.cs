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
    /// The cooldown for the players teleport ability
    /// </summary>
    public float teleportCooldown = 5f;

    /// <summary>
    /// used to disabled or enable teleporting
    /// </summary>
    public bool teleportAllowed = true;


    /// <summary>
    /// used to detect if the teleport is currently on cooldown
    /// </summary>
    private bool canTeleport = true;


    // privates
    private Rigidbody2D body;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }
	
	void Update ()
    {
        if (canTeleport && teleportAllowed && Input.GetButtonDown("Teleport")) // teleport
        {
            Vector2 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            body.MovePosition(point);
            StartCoroutine(teleCooldownTimer());
        }

        // tell the rigidBody to update the position based on the movement detected
        if (body.velocity.magnitude < speed)
            body.AddForce(new Vector2(Input.GetAxis("Horizontal") * Time.deltaTime * acceleration, 
                Input.GetAxis("Vertical") * Time.deltaTime * acceleration));
    }

    /// <summary>
    /// Used to control the cooldown on the teleporter
    /// </summary>
    /// <returns></returns>
    IEnumerator teleCooldownTimer()
    {
        canTeleport = false;
        yield return new WaitForSeconds(teleportCooldown);
        canTeleport = true;
    }
}
