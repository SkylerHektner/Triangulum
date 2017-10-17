using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUnlockButton : UpgradeButton
{

    public bool Unlock = true;


    public override void ApplyUpgrade()
    {
        upgradeLoader.data.SpeedPower_Unlocked = Unlock;
    }

    public override bool CheckUnlocked()
    {
        return upgradeLoader.data.SpeedPower_Unlocked;
    }
}
