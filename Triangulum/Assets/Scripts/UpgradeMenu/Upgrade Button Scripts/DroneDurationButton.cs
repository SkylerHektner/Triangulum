using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneDurationButton : UpgradeButton
{

    public int DurationLevel = 1;

    public float NewDuration = 10;

    public override void ApplyUpgrade()
    {
        upgradeLoader.data.DronePower_DroneDuration = NewDuration;
        upgradeLoader.data.DronePower_UpgradeDuration = DurationLevel;
    }

    public override bool CheckUnlocked()
    {
        return upgradeLoader.data.DronePower_UpgradeDuration >= DurationLevel;
    }
}
