﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour {

    public Transform center;

    public float radius = 2;
    public float radiusSpeed = 2;
    public float rotationSpeed = 80;

    Vector3 lastPos;


	// Use this for initialization
	void Start () {
        // move is within rotation distance of the player
        transform.localPosition = (transform.position - center.position).normalized * radius + center.position;

        // mark our first initial position relative to the player
        lastPos = transform.localPosition - center.localPosition;
    }
	
	void LateUpdate () {
        // adjust the position relative to the player so that we are back where we were incase the player moved
        transform.position = lastPos + center.localPosition;

        // rotate around the player
        Vector3 axis = new Vector3(0, 0, -1);
        transform.RotateAround(center.position, axis, rotationSpeed * Time.deltaTime);

        // keep track of our current relative position so we can again get back to it
        lastPos = transform.localPosition - center.localPosition;

    }
}
