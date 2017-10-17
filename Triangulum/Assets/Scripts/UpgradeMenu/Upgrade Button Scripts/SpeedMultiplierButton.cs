using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedMultiplierButton : UpgradeButton
{

    public int MultiplierLevel = 1;

    public float NewMultiplier = 10;

    public override void ApplyUpgrade()
    {
        upgradeLoader.data.SpeedPower_Multiplier = NewMultiplier;
        upgradeLoader.data.SpeedPower_UpgradeMultiplier = MultiplierLevel;
    }

    public override bool CheckUnlocked()
    {
        return upgradeLoader.data.SpeedPower_UpgradeMultiplier >= MultiplierLevel;
    }
}
