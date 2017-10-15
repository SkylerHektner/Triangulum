using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour {

    public Transform followThis;

    public Vector3 offset;

    public bool calcOffset = true;


	// Use this for initialization
	void Start () {
        if (calcOffset)
        {
            offset = transform.localPosition - followThis.localPosition;
        }
        
	}
	
	// Update is called once per frame
	void Update () {
        if (followThis != null)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, followThis.localPosition + offset, 300);
        }
        
	}
}
