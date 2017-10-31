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

    public bool lassoInstantLoop = false;
    public float instantLoopRange = 60f;
    public float instantLoopRadialDeviance = 30f;

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

    GameObject lightningParticleSystem;



    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        StartCoroutine(updateLine());
        lightningParticleSystem = transform.Find("Lightning Particle System").gameObject;
        lightningParticleSystem.transform.parent = null;
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

            if (lassoInstantLoop)
            {
                instantLoop();
            }
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

        // ACTIVATE THE PARTICLE SYSTEM
        // disable incase it has not been disabled from the previous cycle
        lightningParticleSystem.SetActive(false);

        Mesh mesh = new Mesh();
        Vector3[] verts = new Vector3[3]
        {
            beacons[0].transform.localPosition, beacons[1].transform.localPosition, transform.parent.localPosition
        };
        int[] tri = new int[3] { 2, 1, 0 };
        mesh.vertices = verts;
        mesh.triangles = tri;
        ParticleSystem.ShapeModule s = lightningParticleSystem.GetComponent<ParticleSystem>().shape;
        s.mesh = mesh;


        // re-enable the system
        lightningParticleSystem.SetActive(true);


        // remove all beacons from game
        for (int i = 0; i < beacons.Count; i++)
        {
            Destroy(beacons[i].gameObject);
        }
        beacons.Clear();

        // reset the lineRenderer
        lineRenderer.positionCount = 1;
    }

    // used by the Instant Loop Powerup
    private void instantLoop()
    {
        // remove all beacons from game
        for (int i = 0; i < beacons.Count; i++)
        {
            Destroy(beacons[i].gameObject);
        }
        beacons.Clear();

        // calculate a vector from the player to the click point
        Vector3 clickDirection = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.parent.localPosition);
        clickDirection.z = 0;
        // use tan to derive the angle of that vector
        float clickAngle = Mathf.Rad2Deg * Mathf.Atan(clickDirection.y / clickDirection.x);
        // account for the fact that tan messes up past 180 degrees
        if (clickDirection.x < 0)
            clickAngle += 180;

        // INSTANTIATE FIRST BEACON
        // calc desired position
        Vector3 DirectionVector = new Vector3(
                Mathf.Cos(Mathf.Deg2Rad * (clickAngle + instantLoopRadialDeviance)),
                Mathf.Sin(Mathf.Deg2Rad * (clickAngle + instantLoopRadialDeviance)),
                0).normalized * instantLoopRange;
        // instantiate the beacon at the desired position
        GameObject b = Instantiate(beacon);
        b.transform.localPosition = transform.parent.localPosition + DirectionVector;

        // add beacon to list
        beacons.Add(b.transform);

        // INSTANTIATE SECOND BEACON
        // calc desired position
        DirectionVector = new Vector3(
                Mathf.Cos(Mathf.Deg2Rad * (clickAngle - instantLoopRadialDeviance)),
                Mathf.Sin(Mathf.Deg2Rad * (clickAngle - instantLoopRadialDeviance)),
                0).normalized * instantLoopRange;
        // instantiate the beacon at the desired position
        b = Instantiate(beacon);
        b.transform.localPosition = transform.parent.localPosition + DirectionVector;

        // add beacon to list
        beacons.Add(b.transform);


        //update line renderer
        updateLineRenderer();

    }
}
