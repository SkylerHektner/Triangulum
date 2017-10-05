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

    // adjust how we draw the lines
    public float updateInterval = .1f;
    public float zDisp = -.1f;
    public float distBetweenDeviance = 1f;
    public float devianceRange = 1f;

    // privates
    // list used to keep track of beacons on map
    List<Transform> beacons = new List<Transform>();

    // the line renderer used to draw the triangle lines
    LineRenderer lineRenderer;



    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        StartCoroutine(updateLine());
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
	}

    IEnumerator updateLine()
    {
        while (true)
        {
            updateLineRenderer();
            yield return new WaitForSeconds(updateInterval);
        }
    }

    private void updateLineRenderer()
    {
        // update the line renderer
        if (beacons.Count == 1)
        {
            // draw line from player to first beacon
            List<Vector3> points = addDeviance(transform.parent.localPosition, beacons[0].localPosition);
            points.Add(beacons[0].localPosition);
            // set position count and add positions
            lineRenderer.positionCount = points.Count;
            for (int i = 0; i < points.Count; i++)
            {
                lineRenderer.SetPosition(i, points[i]);
            }
        }
        else if (beacons.Count == 2)
        {
            // draw line from player to first beacon
            List<Vector3> points = addDeviance(transform.parent.localPosition, beacons[0].localPosition);
            points.Add(beacons[0].localPosition);
            // draw line from first beacon to second beacon
            points.AddRange(addDeviance(beacons[0].localPosition, beacons[1].localPosition));
            points.Add(beacons[1].localPosition);
            // draw line from second beacon back to player
            points.AddRange(addDeviance(beacons[1].localPosition, transform.parent.localPosition));
            points.Add(transform.parent.localPosition);
            // set position count and add positions
            lineRenderer.positionCount = points.Count;
            for (int i = 0; i < points.Count; i++)
            {
                lineRenderer.SetPosition(i, points[i]);
            }
        }
    }

    // creates a list of points with the appropriate deviance. Includes the start, but NOT THE END
    private List<Vector3> addDeviance(Vector3 start, Vector3 end)
    {
        List<Vector3> result = new List<Vector3>();
        result.Add(start);
        Vector3 path = end - start;
        for (int i = 0; i*distBetweenDeviance < path.magnitude; i++)
        {
            Vector3 point = new Vector3();
            point.x = path.normalized.x * i * distBetweenDeviance + UnityEngine.Random.Range(-devianceRange, devianceRange);
            point.y = path.normalized.y * i * distBetweenDeviance + UnityEngine.Random.Range(-devianceRange, devianceRange);
            point.x += start.x;
            point.y += start.y;
            point.z = zDisp;
            result.Add(point);
        }

        return result;
    }

    private void placeFirstBeacon(Vector3 pos)
    {
        // instantiate the beacon at the desired position
        GameObject b = Instantiate(beacon);
        b.transform.localPosition = pos;

        // add beacon to list
        beacons.Add(b.transform);
    }

    private void placeSecondBeacon(Vector3 pos)
    {
        // instantiate the beacon at the desired position
        GameObject b = Instantiate(beacon);
        b.transform.localPosition = pos;

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
