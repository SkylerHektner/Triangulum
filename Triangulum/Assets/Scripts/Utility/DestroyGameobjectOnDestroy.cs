using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyGameobjectOnDestroy : MonoBehaviour {

    public GameObject GameobjectToDestroy;

    void OnDestroy()
    {
        Destroy(GameobjectToDestroy);
    }
}
