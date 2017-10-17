using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldDurationButton : UpgradeButton
{

    public int DurationLevel = 1;

    public float NewDuration = 10;

    public override void ApplyUpgrade()
    {
        upgradeLoader.data.ShieldPower_Duration = NewDuration;
        upgradeLoader.data.ShieldPower_UpgradeDuration = DurationLevel;
    }

    public override bool CheckUnlocked()
    {
        return upgradeLoader.data.ShieldPower_UpgradeDuration >= DurationLevel;
    }
}
