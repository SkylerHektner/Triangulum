using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveSelf : MonoBehaviour
{
    public bool onlyDisable = false;
    /// <summary>
    /// The time this object will wait after instantiantion before it destroys itself
    /// </summary>
    public float timeTillRemove
    {
        get { return time_till_remove; }
        set { StopAllCoroutines(); time_till_remove = value; StartCoroutine(destoryDelayer()); }
    }

    private float time_till_remove = 10f;

    void Start()
    {
        if (!onlyDisable)
        {
            StartCoroutine(destoryDelayer());
        }
    }

    void OnDisable()
    {
        StopAllCoroutines();
    }

    void OnEnable()
    {
        StopAllCoroutines();
        StartCoroutine(destoryDelayer());
    }

    IEnumerator destoryDelayer()
    {
        yield return new WaitForSeconds(time_till_remove);
        if (onlyDisable)
        {
            gameObject.SetActive(false);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
