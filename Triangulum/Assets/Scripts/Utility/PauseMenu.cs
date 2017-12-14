using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

    GameObject canvas;

	// Use this for initialization
	void Start () {
        canvas = transform.GetChild(0).gameObject;
        canvas.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown("escape"))
        {
            if (canvas.activeSelf)
            {
                Time.timeScale = 1;
                canvas.SetActive(false);
                GameObject.Find("SoundTrack").GetComponent<AudioSource>().UnPause();
            }
            else
            {
                Time.timeScale = 0;
                canvas.SetActive(true);
                GameObject.Find("SoundTrack").GetComponent<AudioSource>().Pause();
            }
        }
	}

    public void unPause()
    {
        Time.timeScale = 1;
        canvas.SetActive(false);
        GameObject.Find("SoundTrack").GetComponent<AudioSource>().UnPause();
    }
}
