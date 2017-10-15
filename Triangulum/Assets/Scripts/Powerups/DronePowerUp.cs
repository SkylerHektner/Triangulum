using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DronePowerUp : Powerup {

    public int numDrones = 1;

    public float laserCoolDown = 2;

    public bool spikedBodies;

    public float spikedBodiesRadius = 4;

    public float orbitRadius = 10;

    // MUST BE ASSIGNED TO DRONE PREFAB
    public GameObject drone;


    private List<GameObject> spikedBodiesKillZones = new List<GameObject>();

    public override void OnUse()
    {
        // if there are any drones on the field, don't let the player use the power again
        if (GameObject.FindGameObjectsWithTag("Drone").Length != 0)
        {
            return;
        }

        float radialDeviance = 360 / numDrones;
        Transform playerTransform = GameObject.FindWithTag("Player").transform;

        for (int i = 0; i < numDrones; i++)
        {
            GameObject d = GameObject.Instantiate(drone);
            d.transform.localPosition = playerTransform.localPosition;
            Vector3 placementVec = new Vector3(Mathf.Sin(Mathf.Deg2Rad * i * radialDeviance), 
                Mathf.Cos(Mathf.Deg2Rad * i * radialDeviance), 0);
            d.transform.localPosition = d.transform.localPosition + placementVec.normalized * 3;
            d.GetComponent<Orbit>().center = playerTransform;
            d.GetComponent<Orbit>().radius = orbitRadius;
            d.GetComponent<FireLaserAtEnemy>().coolDown = laserCoolDown;

            if (spikedBodies)
            {
                GameObject s = new GameObject();
                s.AddComponent<CircleCollider2D>().isTrigger = true;
                s.GetComponent<CircleCollider2D>().radius = spikedBodiesRadius;
                s.AddComponent<killCollidedEnemy>();
                s.transform.localPosition = d.transform.localPosition;
                s.AddComponent<FollowObject>().followThis = d.transform;
                s.GetComponent<FollowObject>().offset = Vector3.zero;
                s.GetComponent<FollowObject>().calcOffset = false;
                s.name = "Spiked Body Kill Zone" + spikedBodiesKillZones.Count.ToString();

                spikedBodiesKillZones.Add(s);
            }
        }

        setHUDTimer();
        base.OnUse();
    }

    public override void OnEnd()
    {
        // get and destroy all existing drones
        GameObject[] drones = GameObject.FindGameObjectsWithTag("Drone");
        for (int i = 0; i < drones.Length; i++)
        {
            Destroy(drones[i]);
        }
        if (spikedBodies)
        {
            for (int i = 0; i < spikedBodiesKillZones.Count; i++)
            {
                Destroy(spikedBodiesKillZones[i]);
            }
        }
        base.OnEnd();
    }
}
