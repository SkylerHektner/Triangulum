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
            g.transform.SetParent(transform);
            g.AddComponent<PooledObject>().parentPool = this;
            g.SetActive(false);
        }
    }

    public void requestObject(Vector3 startPosition, Quaternion rotation, out GameObject g)
    {
        g = pool.Pop();
        g.SetActive(true);
        g.transform.localPosition = startPosition;
        g.transform.rotation = rotation;
    }

    public void returnObject(GameObject g)
    {
        pool.Push(g);
    }
}
