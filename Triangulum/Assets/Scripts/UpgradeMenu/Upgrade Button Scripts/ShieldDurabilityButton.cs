using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldDurabilityButton : UpgradeButton
{

    public int DurabilityLevel = 1;

    public int NewDurability = 10;

    public override void ApplyUpgrade()
    {
        upgradeLoader.data.ShieldPower_Durability = NewDurability;
        upgradeLoader.data.ShieldPower_UpgradeDurability = DurabilityLevel;
    }

    public override bool CheckUnlocked()
    {
        return upgradeLoader.data.ShieldPower_UpgradeDurability >= DurabilityLevel;
    }
}
