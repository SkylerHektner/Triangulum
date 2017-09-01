using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeaconManager : MonoBehaviour {

    /// <summary>
    /// must be assigned to the beacon prefab
    /// </summary>
    public GameObject beacon;

    /// <summary>
    /// must be assigned to the detonateCollision prefab
    /// </summary>
    public GameObject detonateCollision;

    /// <summary>
    /// activated by the lasso power up to allow right click to place beacons at the mouse click position
    /// </summary>
    public bool lassoModeActive = false;

    // privates
    // list used to keep track of beacons on map
    List<Transform> beacons = new List<Transform>();

    // the line renderer used to draw the triangle lines
    LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // check if the player has placed a beacon
        if (Input.GetButtonDown("Place Beacon"))
        {
            if (beacons.Count == 0) placeFirstBeacon(transform.parent.localPosition);

            else if (beacons.Count == 1) placeSecondBeacon(transform.parent.localPosition);

            else if (beacons.Count == 2) placeThirdBeacon();
        }

        // check if the player has placed a beacon using the Lasso Power Up
        if (lassoModeActive && Input.GetButtonDown("Lasso Beacon"))
        {
            // get the location the character clicked
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0;

            if (beacons.Count == 0) placeFirstBeacon(pos);

            else if (beacons.Count == 1) placeSecondBeacon(pos);

            else if (beacons.Count == 2) placeThirdBeacon();
        }

        // update the line renderer
        if (beacons.Count == 1)
        {
            Vector3 temp = new Vector3(transform.parent.localPosition.x, transform.parent.localPosition.y, -.1f);
            lineRenderer.SetPosition(0, temp);
        }
        else if (beacons.Count == 2)
        {
            Vector3 temp = new Vector3(transform.parent.localPosition.x, transform.parent.localPosition.y, -.1f);
            lineRenderer.SetPosition(0, temp);
            lineRenderer.SetPosition(3, temp);
        }
	}

    private void placeFirstBeacon(Vector3 pos)
    {
        // instantiate the beacon at the desired position
        GameObject b = Instantiate(beacon);
        b.transform.localPosition = pos;

        // adjust the line render settings
        lineRenderer.positionCount = 2;
        Vector3 temp = new Vector3(pos.x, pos.y, -.1f);
        lineRenderer.SetPosition(1, temp);

        // add beacon to list
        beacons.Add(b.transform);
    }

    private void placeSecondBeacon(Vector3 pos)
    {
        // instantiate the beacon at the desired position
        GameObject b = Instantiate(beacon);
        b.transform.localPosition = pos;

        // adjust the line render settings
        lineRenderer.positionCount = 4;
        Vector3 temp = new Vector3(pos.x, pos.y, -.1f);
        lineRenderer.SetPosition(2, temp);
        lineRenderer.SetPosition(3, temp);

        // add beacon to list
        beacons.Add(b.transform);
    }

    private void placeThirdBeacon()
    {
        // Kill all enemies somehow
        GameObject c = Instantiate(detonateCollision);
        PolygonCollider2D collider = c.GetComponent<PolygonCollider2D>();
        collider.SetPath(0, new Vector2[] { beacons[0].localPosition, beacons[1].localPosition, transform.parent.localPosition });


        // remove all beacons from game
        for (int i = 0; i < beacons.Count; i++)
        {
            Destroy(beacons[i].gameObject);
        }
        beacons.Clear();

        // reset the lineRenderer
        lineRenderer.positionCount = 1;
    }
}
