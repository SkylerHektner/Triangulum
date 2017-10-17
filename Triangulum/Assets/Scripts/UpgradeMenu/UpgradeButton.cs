using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UpgradeButton : MonoBehaviour {

    public bool locked = true;

    public bool treeStart = false;

    public GameObject[] Dependents;

    public int UnlockNeeded = 0;

    public int cost = 100;

    private int unlockCount = 0;

    private Button button;

    // Use this for initialization
    void Start () {
        button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(UpgradeRequest);
        if (!treeStart)
        {
            button.interactable = !locked;
        }

        if (CheckUnlocked())
        {
            locked = false;
            button.interactable = true;
        }
	}

    public void UpgradeRequest()
    {
        // check if they can afford the upgrade
        if (cost > upgradeLoader.data.Player_TaxPayerDollars)
        {
            Debug.Log("You can't afford that");
            return;
        }
        if (CheckUnlocked())
        {
            Debug.Log("You already unlocked this");
            return;
        }


        ApplyUpgrade();
        for (int i = 0; i < Dependents.Length; i++)
        {
            Dependents[i].GetComponent<UpgradeButton>().AddUnlockCredit();
        }
    }

    public virtual bool CheckUnlocked()
    {
        return false;
    }

    public virtual void ApplyUpgrade()
    {

    }

    public void AddUnlockCredit()
    {
        unlockCount++;
        if (unlockCount == UnlockNeeded)
        {
            locked = false;
            button.interactable = true;
        }
    }
	
	
}
