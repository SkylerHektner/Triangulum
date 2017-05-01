using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopPowerUp : Powerup {
    
    /// <summary>
    /// MUST BE ASSIGNED TO THE NEW SPRITE BOARD
    /// </summary>
    public Sprite newBoardSprite;

    private Sprite oldBoardSprite;

    public override void OnUse()
    {
        GameObject c = GameObject.Find("Survival Board");
        c.GetComponent<EdgeCollider2D>().enabled = false;
        oldBoardSprite = c.GetComponent<SpriteRenderer>().sprite;
        c.GetComponent<SpriteRenderer>().sprite = newBoardSprite;
        base.OnUse();
    }

    public override void OnEnd()
    {
        GameObject c = GameObject.Find("Survival Board");
        c.GetComponent<EdgeCollider2D>().enabled = true;
        c.GetComponent<SpriteRenderer>().sprite = oldBoardSprite;
        base.OnEnd();
    }
}
