﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayMoney : MonoBehaviour {

    private Text t;
	// Use this for initialization
	void Start () {
        t = gameObject.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        t.text = "Tax Payer Dollars: " + upgradeLoader.data.Player_TaxPayerDollars.ToString();
	}
}
