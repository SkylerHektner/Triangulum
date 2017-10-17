using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldDropChanceButton : UpgradeButton
{

    public int DropChanceLevel = 1;

    public float NewDropChance = 10;

    public override void ApplyUpgrade()
    {
        upgradeLoader.data.ShieldPower_DropChance = NewDropChance;
        upgradeLoader.data.ShieldPower_UpgradeDropChance = DropChanceLevel;
    }

    public override bool CheckUnlocked()
    {
        return upgradeLoader.data.ShieldPower_UpgradeDropChance >= DropChanceLevel;
    }
}
