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
        // check of there is another loop running. If there is, end it. 
        GameObject[] otherPower = GameObject.FindGameObjectsWithTag("Loop Power Up");

        for (int i = 0; i < otherPower.Length; i++)
        {
            Powerup p = otherPower[i].GetComponent<Powerup>();
            if (p.used)
            {
                p.OnEnd();
                break;
            }
        }
        
        // switch out the board with the alternative graphic, enable looping on the boarder controller
        GameObject c = GameObject.Find("Survival Board");
        c.GetComponent<EdgeCollider2D>().enabled = false;
        oldBoardSprite = c.GetComponent<SpriteRenderer>().sprite;
        c.GetComponent<SpriteRenderer>().sprite = newBoardSprite;
        base.OnUse();
    }

    public override void OnEnd()
    {
        // revert changes in OnUse
        GameObject c = GameObject.Find("Survival Board");
        c.GetComponent<EdgeCollider2D>().enabled = true;
        c.GetComponent<SpriteRenderer>().sprite = oldBoardSprite;
        base.OnEnd();
    }
}
