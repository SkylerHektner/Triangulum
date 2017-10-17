using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportAbility : MonoBehaviour {

    /// <summary>
    /// The cooldown for the players teleport ability
    /// </summary>
    public float cooldown = 5f;

    public bool lethal = false;

    public float lethalRadius = 100;

    public int charges = 1;

    public AudioClip TeleportSound;

    /// <summary>
    /// used to detect if the teleport is currently on cooldown
    /// </summary>
    private bool canTeleport = true;
    private float cooldownCharge = 0;

   // pointer to the characters rigid body
    private Rigidbody2D body;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        cooldown = upgradeLoader.data.Teleport_Cooldown;
        lethal = upgradeLoader.data.Teleport_Lethal;
        lethalRadius = upgradeLoader.data.Teleport_LethalRadius;
        charges = upgradeLoader.data.Teleport_Charges;
        cooldownCharge = cooldown * charges;
    }

    public void Update()
    {
        if (cooldownCharge < cooldown * charges) // charge their teleport
        {
            cooldownCharge += Time.deltaTime;
        }

        if (cooldownCharge >= cooldown && upgradeLoader.data.Teleport_CanTeleport && Input.GetButtonDown("Teleport")) // teleport
        {
            cooldownCharge -= cooldown;
            Vector2 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            body.MovePosition(point);
            if (lethal)
            {
                spawnKillPad(point);
            }
            gameObject.GetComponent<AudioSource>().PlayOneShot(TeleportSound);
        }

    }

    // Spawns in a gameobject to kill enemies in the lethal radius and automatically removes itself after .1 seconds
    private void spawnKillPad(Vector2 point)
    {
        GameObject k = new GameObject();
        k.AddComponent<RemoveSelf>().timeTillRemove = .1f;
        k.AddComponent<CircleCollider2D>().radius = lethalRadius;
        k.GetComponent<CircleCollider2D>().isTrigger = true;
        k.AddComponent<killCollidedEnemy>();
        k.transform.localPosition = point;
    }
}
