using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour {

    public static HUDManager Instance;

    private Text scoreText;
    private Text multiplierText;


	// Use this for initialization
	void Start () {
        Instance = this;

        scoreText = transform.GetChild(0).Find("Score").Find("ScoreText").GetComponent<Text>();
        multiplierText = transform.GetChild(0).Find("Score").Find("MultiplierText").GetComponent<Text>();
    }

    public void setScore(int s)
    {
        scoreText.text = s.ToString();
    }

    public void setMultiplier(float f)
    {
        multiplierText.text = "x" + f.ToString();
    }
}
