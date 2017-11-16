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

    public int maxCharges = 1;

    public AudioClip TeleportSound;

    /// <summary>
    /// used to detect if the teleport is currently on cooldown
    /// </summary>
    private bool canTeleport = true;
    private float cooldownCharge = 0;
    private int charges = 0;

   // pointer to the characters rigid body
    private Rigidbody2D body;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        cooldown = upgradeLoader.data.Teleport_Cooldown;
        lethal = upgradeLoader.data.Teleport_Lethal;
        lethalRadius = upgradeLoader.data.Teleport_LethalRadius;
        maxCharges = upgradeLoader.data.Teleport_Charges;
        cooldownCharge = 0;
        charges = maxCharges;
        HUDManager.Instance.setTeleportSliderValue(cooldownCharge / cooldown);
        HUDManager.Instance.setTeleportBatteryIcon(charges);
    }

    public void Update()
    {
        // increase the cooldown charge if the player is not at max charges and the charge isn't already maxed
        if (charges < maxCharges && cooldownCharge < cooldown)
        {
            cooldownCharge += Time.deltaTime;
            HUDManager.Instance.setTeleportSliderValue(cooldownCharge / cooldown);
        }

        // if we now have enough cooldown charge to add a charge, add a charge
        if (cooldownCharge >= cooldown && charges < maxCharges)
        {
            charges += 1;
            cooldownCharge = 0;
            HUDManager.Instance.setTeleportBatteryIcon(charges);
        }

        // If the player wants to teleport, has teleport unlocked, and has charges, then teleport
        if (charges > 0 && upgradeLoader.data.Teleport_CanTeleport && Input.GetButtonDown("Teleport")) 
        {
            charges--;
            HUDManager.Instance.setTeleportBatteryIcon(charges);
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
