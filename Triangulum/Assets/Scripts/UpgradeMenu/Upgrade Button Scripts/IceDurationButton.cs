using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceDurationButton : UpgradeButton
{

    public int DurationLevel = 1;

    public float NewDuration = 10;

    public override void ApplyUpgrade()
    {
        upgradeLoader.data.IcePower_FreezeDuration = NewDuration;
        upgradeLoader.data.IcePower_UpgradeDuration = DurationLevel;
    }

    public override bool CheckUnlocked()
    {
        return upgradeLoader.data.IcePower_UpgradeDuration >= DurationLevel;
    }
}
