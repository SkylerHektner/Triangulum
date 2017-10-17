using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneUnlockButton : UpgradeButton
{

    public bool Unlock = true;


    public override void ApplyUpgrade()
    {
        upgradeLoader.data.DronePower_Unlocked = Unlock;
    }

    public override bool CheckUnlocked()
    {
        return upgradeLoader.data.DronePower_Unlocked;
    }
}
