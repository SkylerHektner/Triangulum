using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour {

    public Transform followThis;

    private Vector3 offset;
	// Use this for initialization
	void Start () {
        offset = transform.localPosition - followThis.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
        transform.localPosition = Vector3.Lerp(transform.localPosition, followThis.localPosition + offset, 300);
	}
}
