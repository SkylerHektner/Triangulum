using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LassoDropChanceButton : UpgradeButton
{

    public int DropChanceLevel = 1;

    public float NewDropChance = 10;

    public override void ApplyUpgrade()
    {
        upgradeLoader.data.LassoPower_DropChance = NewDropChance;
        upgradeLoader.data.LassoPower_UpgradeDropChance = DropChanceLevel;
    }

    public override bool CheckUnlocked()
    {
        return upgradeLoader.data.LassoPower_UpgradeDropChance >= DropChanceLevel;
    }
}
