using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ContinueButton : MonoBehaviour {

    private int lastLevel = 1;

	// Use this for initialization
	void Start () {
        lastLevel = upgradeLoader.data.lastLevelPlayed;
	}
	
	public void loadLevel()
    {
        if (lastLevel == 1)
        {
            SceneManager.LoadScene("Desert_Level");
        }
        else if (lastLevel == 2)
        {
            SceneManager.LoadScene("Snow_Level");
        }
        else if (lastLevel == 3)
        {
            SceneManager.LoadScene("Lava_Level");
        }
    }
}
