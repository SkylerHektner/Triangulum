using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopPowerUp : Powerup {
    
    /// <summary>
    /// MUST BE ASSIGNED TO THE NEW SPRITE BOARD
    /// </summary>
    public Sprite newBoardSprite;

    private Sprite oldBoardSprite;

    private GameObject survivalBoard;

    void Start()
    {
        survivalBoard = GameObject.Find("Survival Board");
    }

    public override void OnUse()
    {
        // if for any reason survival board is null, get a new pointer to it.
        if (survivalBoard == null)
        {
            survivalBoard = GameObject.Find("Survival Board");
        }

        // only allow use of the power if another loop power hasn't already been used
        if (survivalBoard.GetComponent<EdgeCollider2D>().enabled)
        {
            // switch out the board with the alternative graphic, enable looping on the boarder controller
            survivalBoard.GetComponent<EdgeCollider2D>().enabled = false;
            oldBoardSprite = survivalBoard.GetComponent<SpriteRenderer>().sprite;
            survivalBoard.GetComponent<SpriteRenderer>().sprite = newBoardSprite;
            base.OnUse();
        }
    }

    public override void OnEnd()
    {
        // if for any reason survival board is null, get a new pointer to it.
        if (survivalBoard == null)
        {
            survivalBoard = GameObject.Find("Survival Board");
        }

        // revert changes in OnUse
        survivalBoard.GetComponent<EdgeCollider2D>().enabled = true;
        survivalBoard.GetComponent<SpriteRenderer>().sprite = oldBoardSprite;
        base.OnEnd();
    }
}
