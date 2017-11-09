using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour {

    public GameObject template;

    public int poolSize;

    private Stack<GameObject> pool = new Stack<GameObject>(2000);

    void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject g = GameObject.Instantiate(template);
            g.transform.parent = transform;
            g.SetActive(false);
            pool.Push(g);
        }
    }

    public void requestObject(Vector3 startPosition, Quaternion rotation, out GameObject g)
    {
        Debug.Log("An object was requested");
        g = pool.Pop();
        g.SetActive(true);
        g.transform.localPosition = startPosition;
        g.transform.rotation = rotation;
        if (g.GetComponent<PooledObject>() == null)
        {
            g.AddComponent<PooledObject>().parentPool = this;
        }
    }

    public void returnObject(GameObject g)
    {
        pool.Push(g);
    }
}
