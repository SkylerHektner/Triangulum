using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserUnlockButton : UpgradeButton
{

    public bool Unlock = true;


    public override void ApplyUpgrade()
    {
        upgradeLoader.data.LaserPower_Unlocked = Unlock;
    }

    public override bool CheckUnlocked()
    {
        return upgradeLoader.data.LaserPower_Unlocked;
    }
}
