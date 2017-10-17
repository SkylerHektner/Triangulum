using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserNumLaserButton : UpgradeButton
{

    public int NumLaserLevel = 1;

    public int NewNumLasers = 10;

    public override void ApplyUpgrade()
    {
        upgradeLoader.data.LaserPower_NumLasers = NewNumLasers;
        upgradeLoader.data.LaserPower_UpgradeNumLaser = NumLaserLevel;
    }

    public override bool CheckUnlocked()
    {
        return upgradeLoader.data.LaserPower_UpgradeNumLaser >= NumLaserLevel;
    }
}
