using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportAbility : MonoBehaviour {

    /// <summary>
    /// The cooldown for the players teleport ability
    /// </summary>
    public float teleportCooldown = 5f;

    /// <summary>
    /// used to detect if the teleport is currently on cooldown
    /// </summary>
    private bool canTeleport = true;

   // pointer to the characters rigid body
    private Rigidbody2D body;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
        if (canTeleport && Input.GetButtonDown("Teleport")) // teleport
        {
            Vector2 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            body.MovePosition(point);
            StartCoroutine(teleCooldownTimer());
        }
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
