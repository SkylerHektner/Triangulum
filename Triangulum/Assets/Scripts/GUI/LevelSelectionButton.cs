using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectionButton : MonoBehaviour {

    public int levelToUnlock = 1;
    public int waveToUnlock = 15;

	// Use this for initialization
	void Start ()
    {
        gameObject.GetComponent<Button>().interactable = upgradeLoader.data.highestWave[levelToUnlock - 1] >= waveToUnlock;
    }
}
