using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LassoDurationButton : UpgradeButton
{

    public int DurationLevel = 1;

    public float NewDuration = 10;

    public override void ApplyUpgrade()
    {
        upgradeLoader.data.LassoPower_LassoDuration = NewDuration;
        upgradeLoader.data.LassoPower_UpgradeDuration = DurationLevel;
    }

    public override bool CheckUnlocked()
    {
        return upgradeLoader.data.LassoPower_UpgradeDuration >= DurationLevel;
    }
}
