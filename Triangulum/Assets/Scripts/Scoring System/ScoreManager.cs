using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour {

    // a public static reference to this instance of the score manager
    public static ScoreManager Instance;

    // the value added to the multiplier each kill
    public float multiplierGrowthRate = .1f;

    private float multiplier = 1;
    public int score = 0;


	void Start () {
        Instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public int addScore(float baseValue)
    {
        int scoreGained =  Convert.ToInt32(baseValue * multiplier);
        score += scoreGained;
        
        multiplier += multiplierGrowthRate;
        HUDManager.Instance.setScore(score);
        HUDManager.Instance.setMultiplier(multiplier);

        return scoreGained;
    }
}
