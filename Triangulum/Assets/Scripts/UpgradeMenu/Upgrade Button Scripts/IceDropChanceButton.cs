using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceDropChanceButton : UpgradeButton
{

    public int DropChanceLevel = 1;

    public float NewDropChance = 10;

    public override void ApplyUpgrade()
    {
        upgradeLoader.data.IcePower_DropChance = NewDropChance;
        upgradeLoader.data.IcePower_UpgradeDropChange = DropChanceLevel;
    }

    public override bool CheckUnlocked()
    {
        return upgradeLoader.data.IcePower_UpgradeDropChange >= DropChanceLevel;
    }
}
