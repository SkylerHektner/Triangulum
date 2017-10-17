using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedDurationButton : UpgradeButton {

    public int DurationLevel = 1;

    public float NewDuration = 10;

    public override void ApplyUpgrade()
    {
        upgradeLoader.data.SpeedPower_SpeedDuration = NewDuration;
        upgradeLoader.data.SpeedPower_UpgradeDuration = DurationLevel;
    }

    public override bool CheckUnlocked()
    {
        return upgradeLoader.data.SpeedPower_UpgradeDuration >= DurationLevel;
    }
}
