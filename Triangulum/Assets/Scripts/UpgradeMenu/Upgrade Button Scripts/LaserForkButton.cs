using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserForkButton : UpgradeButton
{

    public bool Unlock = true;


    public override void ApplyUpgrade()
    {
        upgradeLoader.data.LaserPower_Fork = Unlock;
    }

    public override bool CheckUnlocked()
    {
        return upgradeLoader.data.LaserPower_Fork;
    }
}
