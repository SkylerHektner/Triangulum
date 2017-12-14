using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeDialogue : MonoBehaviour {

    public string nameText;
    public string descriptionText;
    public UpgradeButton caller;
    public int cost;
    public AudioClip succesfullUpgradeSound;

    public static UpgradeDialogue instance;

	void Start ()
    {
        instance = this;

        gameObject.SetActive(false);

        transform.Find("CancelButton").GetComponent<Button>().onClick.AddListener(cancelButtonMethod);
    }

    void OnEnable()
    {
        transform.Find("NameText").GetComponent<Text>().text = nameText;
        transform.Find("DescriptionText").GetComponent<Text>().text = descriptionText + "\nCost: " + cost.ToString();

        transform.Find("PurchaseButton").GetComponent<Button>().onClick.RemoveAllListeners();
        transform.Find("PurchaseButton").GetComponent<Button>().onClick.AddListener(purchaseButtonMethod);
    }

    void cancelButtonMethod()
    {
        gameObject.SetActive(false);
    }

    void purchaseButtonMethod()
    {
        if (caller.UpgradeRequest())
        {
            GameObject.Find("Upgrades_Canvas").GetComponent<AudioSource>().PlayOneShot(succesfullUpgradeSound);
            gameObject.SetActive(false);
        }
    }
	
}
