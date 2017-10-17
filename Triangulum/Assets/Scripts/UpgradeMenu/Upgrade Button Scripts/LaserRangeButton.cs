using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserRangeButton : UpgradeButton
{

    public int RangeLevel = 1;

    public float NewSpeed = 10;
    public float NewDuration = 10;

    public override void ApplyUpgrade()
    {
        upgradeLoader.data.LaserPower_LaserDuration = NewDuration;
        upgradeLoader.data.LaserPower_LaserSpeed = NewSpeed;
        upgradeLoader.data.LaserPower_UpgradeRange = RangeLevel;
    }

    public override bool CheckUnlocked()
    {
        return upgradeLoader.data.LaserPower_UpgradeRange >= RangeLevel;
    }
}
