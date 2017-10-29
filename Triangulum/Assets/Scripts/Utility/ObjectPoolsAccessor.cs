using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolsAccessor : MonoBehaviour {

    public static ObjectPoolsAccessor instance;

    public EZObjectPools.EZObjectPool laserPool; 

	// Use this for initialization
	void Start () {
        instance = this;
	}
}
