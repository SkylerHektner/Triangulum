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

        right = worldPos.y + (size.y / 2f);
        left = worldPos.y - (size.y / 2f);
        bottom = worldPos.x - (size.x / 2f);
        top = worldPos.x + (size.x / 2f);


        LineRenderer l = gameObject.GetComponent<LineRenderer>();
        l.SetPosition(0, new Vector3(right, top, -.1f));
        l.SetPosition(1, new Vector3(left, top, -.1f));
        l.SetPosition(2, new Vector3(left, bottom, -.1f));
        l.SetPosition(3, new Vector3(right, bottom, -.1f));
        l.SetPosition(4, new Vector3(right, top, -.1f));

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
