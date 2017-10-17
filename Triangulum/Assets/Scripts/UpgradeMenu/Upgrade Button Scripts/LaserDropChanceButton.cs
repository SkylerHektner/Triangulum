using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserDropChanceButton : UpgradeButton
{

    public int DropChanceLevel = 1;

    public float NewDropChance = 10;

    public override void ApplyUpgrade()
    {
        upgradeLoader.data.LaserPower_DropChance = NewDropChance;
        upgradeLoader.data.LaserPower_UpgradeDropChange = DropChanceLevel;
    }

    public override bool CheckUnlocked()
    {
        return upgradeLoader.data.LaserPower_UpgradeDropChange >= DropChanceLevel;
    }
}
