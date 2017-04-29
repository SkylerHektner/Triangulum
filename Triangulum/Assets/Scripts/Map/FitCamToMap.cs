using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FitCamToMap : MonoBehaviour {

	/// <summary>
    /// simply used to resize the main camera to fit the playing board
    /// </summary>
	void Start () {
        Vector2 dimensions = GetComponent<SpriteRenderer>().bounds.size;
        Camera.main.orthographicSize = Math.Max(dimensions.x, dimensions.y) / 2;
    }
	
}
