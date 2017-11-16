using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour {

    // a constant reference to the HUD manager that can be used by other scripts
    public static HUDManager Instance;
    // a pointer to the prefab we use for displaying power up timers
    public GameObject powerTimer;
    // used for displaying the different number of teleports the player has saved up
    public Sprite[] teleportBatteries;

    private Text scoreText;
    private Text multiplierText;
    private Text moneyText;
    private List<GameObject> powerUpTimers = new List<GameObject>();

    private Slider teleportSlider;
    private Image teleportBatteryImage;


    void Start () {
        Instance = this;

        scoreText = transform.GetChild(0).Find("Score").Find("ScoreText").GetComponent<Text>();
        multiplierText = transform.GetChild(0).Find("Score").Find("MultiplierText").GetComponent<Text>();
        moneyText = transform.GetChild(0).Find("Moneys").Find("Text").GetComponent<Text>();
        moneyText.text = "Money: " + upgradeLoader.data.Player_TaxPayerDollars.ToString();

        teleportSlider = transform.GetChild(0).Find("Teleport").Find("Slider").gameObject.GetComponent<Slider>();
        teleportBatteryImage = transform.GetChild(0).Find("Teleport").Find("Battery Icon").gameObject.GetComponent<Image>();
    }

    void Update()
    {
        // check if any power up timers have expired
        if (powerUpTimers.Contains(null))
        {
            positionPowerUpTimers();
        }
    }

    // public function to remove the power up timer passed in before its duration expires
    public void removePowerUpTimer(GameObject timer)
    {
        if (timer != null)
        {
            Destroy(timer);
            positionPowerUpTimers();
        }
    }

    // public function used to instantiate a new power up timer
    public GameObject createPowerUpTimer(float Duration, Sprite powerUpImage)
    {
        GameObject t = GameObject.Instantiate(powerTimer);
        t.transform.SetParent(transform.GetChild(0));
        // set the duration of the powerup and the image to be displayed
        PowerUpTimer s = t.GetComponent<PowerUpTimer>();

        s.powerUpImage = powerUpImage;
        s.powerUpDuration = Duration;
        // set the time till the object removes itself
        t.GetComponent<RemoveSelf>().timeTillRemove = Duration;
        // add the object to the list of timers
        powerUpTimers.Add(t);

        // format the position of all timers
        positionPowerUpTimers();

        return t;
    }

    // private function used to correctly position multiple power up timers and remove null ones
    private void positionPowerUpTimers()
    {
        powerUpTimers.Remove(null);

        int offsetDelta = 80; // NEED TO FIND A WAY TO MAKE THIS DYNAMIC BASED ON RESOLUTION
        int curOffSet = 0;

        for (int i = 0; i < powerUpTimers.Count; i++)
        {
            GameObject t = powerUpTimers[i];
            RectTransform trans = t.GetComponent<RectTransform>();
            trans.offsetMax = new Vector2(0, -curOffSet);
            trans.offsetMin = new Vector2(0, -curOffSet);
            curOffSet += offsetDelta;

        }
    }

    // public function to set the score text of the HUD
    public void setScore(int s)
    {
        scoreText.text = s.ToString();
    }

    // public function to set the multiplier text of the HUD
    public void setMultiplier(float f)
    {
        multiplierText.text = "x" + f.ToString();
    }

    public void setMoney(int m)
    {
        moneyText.text = m.ToString();
    }

    public void setTeleportSliderValue(float v)
    {
        if (v > 1 || v < 0)
        {
            //Debug.Log("You passed in the wrong value for the teleport slider in the HUD: " + v);
            return;
        }
        teleportSlider.value = v;
    }

    public void setTeleportBatteryIcon(int numCharges)
    {
        if (numCharges < 0 || numCharges > 5)
        {
            Debug.Log("You passed in the wrong number of charges for the battery icon for teleport in the HUD: " + numCharges);
            return;
        }
        teleportBatteryImage.sprite = teleportBatteries[numCharges];
    }
}
