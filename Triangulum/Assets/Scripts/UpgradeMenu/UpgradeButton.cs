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

    public string upgradeName;

    public string descriptionText;

    private int unlockCount = 0;

    private Button button;

    // Use this for initialization
    void Start () {
        button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(CreateUpgradeDialogue);
        if (!treeStart)
        {
            button.interactable = !locked;
        }

        if (CheckUnlocked())
        {
            locked = false;
            button.interactable = true;
            for (int i = 0; i < Dependents.Length; i++)
            {
                try
                {
                    Dependents[i].GetComponent<UpgradeButton>().AddUnlockCredit();
                }
                catch
                {
                    Debug.Log("Failed to update next button");
                }
            }
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
	}

    public void CreateUpgradeDialogue()
    {
        if (!CheckUnlocked())
        {
            GameObject dio = UpgradeDialogue.instance.gameObject;
            dio.SetActive(false);
            UpgradeDialogue s = dio.GetComponent<UpgradeDialogue>();
            s.nameText = upgradeName;
            s.descriptionText = descriptionText;
            s.cost = cost;
            s.caller = this;

            dio.SetActive(true);
        }
    }

    public bool UpgradeRequest()
    {
        // check if they can afford the upgrade
        if (cost > upgradeLoader.data.Player_TaxPayerDollars)
        {
            Debug.Log("You can't afford that");
            return false;
        }
        if (CheckUnlocked())
        {
            Debug.Log("You already unlocked this");
            return false;
        }


        ApplyUpgrade();
        for (int i = 0; i < Dependents.Length; i++)
        {
            Dependents[i].GetComponent<UpgradeButton>().AddUnlockCredit();
        }
        upgradeLoader.data.Player_TaxPayerDollars -= cost;
        upgradeLoader.Instance.SaveData();

        transform.GetChild(0).gameObject.SetActive(true);

        return true;
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
