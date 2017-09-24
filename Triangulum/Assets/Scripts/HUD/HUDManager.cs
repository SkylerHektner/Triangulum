using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour {

    // a constant reference to the HUD manager that can be used by other scripts
    public static HUDManager Instance;
    // a pointer to the prefab we use for displaying power up timers
    public GameObject powerTimer;
    // pointers to the power up sprites
    public Sprite SpeedPowerSprite;
    public Sprite DronePowerSprite;
    public Sprite LassoPowerSprite;

    private Text scoreText;
    private Text multiplierText;
    private List<GameObject> powerUpDurationTimers = new List<GameObject>();

	// Use this for initialization
	void Start () {
        Instance = this;

        scoreText = transform.GetChild(0).Find("Score").Find("ScoreText").GetComponent<Text>();
        multiplierText = transform.GetChild(0).Find("Score").Find("MultiplierText").GetComponent<Text>();
    }

    public void displayPowerUpTimer(float Duration, Sprite powerUpImage)
    {
        GameObject t = GameObject.Instantiate(powerTimer);
        t.transform.SetParent(transform.GetChild(0));
        RectTransform trans = t.GetComponent<RectTransform>();
        trans.offsetMax = new Vector2(0, 0);
        trans.offsetMin = new Vector2(0, 0);

        // set the duration of the powerup and the image to be displayed
        PowerUpTimer s = t.GetComponent<PowerUpTimer>();

        s.powerUpImage = powerUpImage;
        s.powerUpDuration = Duration;
        // set the time till the object removes itself
        t.GetComponent<RemoveSelf>().timeTillRemove = Duration;
        // add the object to the list of timers
        powerUpDurationTimers.Add(t);
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
