using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledObject : MonoBehaviour {

    public ObjectPool parentPool;

	void OnDisable()
    {
        parentPool.returnObject(gameObject);
    }
}
