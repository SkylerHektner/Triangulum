using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolsAccessor : MonoBehaviour {

    public static ObjectPoolsAccessor instance;

    public ObjectPool laserPool;

    public ObjectPool ScoreNotifierPool;

	// Use this for initialization
	void Start () {
        instance = this;
	}
}
