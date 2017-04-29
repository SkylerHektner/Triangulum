using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveSelf : MonoBehaviour
{

    /// <summary>
    /// The time this object will wait after instantiantion before it destroys itself
    /// </summary>
    public float timeTillRemove = 1;

    void Start()
    {
        StartCoroutine(destoryDelayer());
    }

    IEnumerator destoryDelayer()
    {
        yield return new WaitForSeconds(timeTillRemove);
        Destroy(gameObject);
    }
}
