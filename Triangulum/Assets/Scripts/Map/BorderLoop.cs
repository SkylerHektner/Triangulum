using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderLoop : MonoBehaviour {

	void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        { 
            loopPlayer(col.gameObject);
        }
    }

    private void loopPlayer(GameObject player)
    {
        float top = BoardInfo.top;
        float btm = BoardInfo.bottom;
        float left = BoardInfo.left;
        float right = BoardInfo.right;

        if (player.transform.localPosition.y < btm)
        {
            Vector2 newPosition = new Vector2(player.transform.localPosition.x, top);
            player.GetComponent<Rigidbody2D>().position = newPosition;
        }
        else if (player.transform.localPosition.y > top)
        {
            Vector2 newPosition = new Vector2(player.transform.localPosition.x, btm);
            player.GetComponent<Rigidbody2D>().position = newPosition;
        }

        if (player.transform.localPosition.x < left)
        {
            Vector2 newPosition = new Vector2(right, player.transform.localPosition.y);
            player.GetComponent<Rigidbody2D>().position = newPosition;
        }
        else if (player.transform.localPosition.x > right)
        {
            Vector2 newPosition = new Vector2(left, player.transform.localPosition.y);
            player.GetComponent<Rigidbody2D>().position = newPosition;
        }
    }
}
