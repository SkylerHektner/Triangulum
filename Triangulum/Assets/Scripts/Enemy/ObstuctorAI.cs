using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstuctorAI : MonoBehaviour {

    // the max amount of time a spider will spend moving in a given direction
    public float maxPathfindingTime = 10f;
    // the min amount of time a spider will spend moving in a given direction
    public float minPathfindingTime = 3f;
    // a pointer to the web prefab
    public GameObject web;
    // how fast the spider can move
    public float speed = 30f;
    // how long the web will last for before it decays and dissapears
    public float webDecayDuration = 20f;
    // the multiplier the web will apply to the speed
    public float slowMultiplier = .5f;
    // how long the web will apply the slow effect for
    public float webSlowDuration = 5f;

    private float pathingTime = 0f;
    private Vector3 previousPos;

    void Start()
    {
        // assign the initial previous pos
        previousPos = transform.localPosition;
        // assign the speed to the follow vec script
        gameObject.GetComponent<FollowVector>().speed = speed;
    }

    // we need to make the script movemement safe, so we disable and enable followVec on disable/enable
    void OnDisable()
    {
        gameObject.GetComponent<FollowVector>().enabled = false;
    }

    void OnEnable()
    {
        gameObject.GetComponent<FollowVector>().enabled = true;
    }

    void Update()
    {
        // subtract the time passed from pathing time
        pathingTime -= Time.deltaTime;

        // if pathing time = 0, time to find a new destination and lay a web
        if (pathingTime <= 0)
        {
            layWeb();
            setNewPath();
            pathingTime = Random.Range(minPathfindingTime, maxPathfindingTime);
        }

        // if our position has not changed since the last frame, we are likely stuck. Set a new path
        if ((previousPos - transform.localPosition).magnitude / Time.deltaTime < speed * .95)
        {
            setNewPath();
        }

        // record our current position
        previousPos = transform.localPosition;
    }

    void layWeb()
    {
        // create and configure the web then lay it down
        GameObject w = GameObject.Instantiate(web);
        w.GetComponent<RemoveSelf>().timeTillRemove = webDecayDuration;
        w.transform.localPosition = transform.localPosition;
        Web webScript = w.GetComponent<Web>();
        webScript.slowDuration = webSlowDuration;
        webScript.slowMultiplier = slowMultiplier;
    }

    void setNewPath()
    {
        // set a new random path for the spider to follow
        float x = Random.Range(-1f, 1f);
        float y = Random.Range(-1f, 1f);
        gameObject.GetComponent<FollowVector>().vec = new Vector3(x, y, 0);
    }
    
}
