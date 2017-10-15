using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardInfo : MonoBehaviour {

    /// <summary>
    /// Information about the dimensions of the board
    /// </summary>
    public static float top;
    public static float bottom;
    public static float left;
    public static float right;

	// Use this for initialization
	void Start () {
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        Vector2 size = new Vector2(collider.size.x * transform.localScale.x, collider.size.y * transform.localScale.y);
        Vector2 worldPos = transform.localPosition;

        top = worldPos.y + (size.y / 2f);
        bottom = worldPos.y - (size.y / 2f);
        left = worldPos.x - (size.x / 2f);
        right = worldPos.x + (size.x / 2f);

        Debug.Log(top);
        Debug.Log(bottom);
        Debug.Log(left);
        Debug.Log(right);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
