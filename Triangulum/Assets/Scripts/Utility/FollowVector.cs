using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowVector : MonoBehaviour {

    public Vector3 vec;

    public float speed = 10;
	
	// Update is called once per frame
	void Update () {
        transform.localPosition = transform.localPosition + (vec.normalized * speed * Time.deltaTime);
	}
}
