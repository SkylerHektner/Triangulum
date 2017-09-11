using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DronePowerUp : Powerup {

    public int numDrones = 1;

    // MUST BE ASSIGNED TO DRONE PREFAB
    public GameObject drone;


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
        }

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

        base.OnEnd();
    }
}
