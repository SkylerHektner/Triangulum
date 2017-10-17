using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedDropChanceButton : UpgradeButton
{

    public int DropChanceLevel = 1;

    public float NewDropChance = 10;

    public override void ApplyUpgrade()
    {
        upgradeLoader.data.SpeedPower_DropChance = NewDropChance;
        upgradeLoader.data.SpeedPower_UpgradeDropChange = DropChanceLevel;
    }

    public override bool CheckUnlocked()
    {
        return upgradeLoader.data.SpeedPower_UpgradeDropChange >= DropChanceLevel;
    }
}
