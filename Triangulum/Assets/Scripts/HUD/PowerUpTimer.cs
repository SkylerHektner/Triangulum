using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpTimer : MonoBehaviour {

    // duration of the power up
    public float powerUpDuration;
    // image of the powerup used
    public Sprite powerUpImage;

    //pointer to the slider we will adjust to show the duration left on the power
    private Slider slider;

    private float durationLeft;

	void Start () {
        transform.GetChild(1).gameObject.GetComponent<Image>().sprite = powerUpImage;
        slider = gameObject.GetComponentInChildren<Slider>();
        durationLeft = powerUpDuration;
	}
	
	void Update () {
        durationLeft -= Time.deltaTime;
        slider.value = durationLeft / powerUpDuration;
	}
}
