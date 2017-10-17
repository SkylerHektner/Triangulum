using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LassoUnlockButton : UpgradeButton
{

    public bool Unlock = true;


    public override void ApplyUpgrade()
    {
        upgradeLoader.data.LassoPower_Unlocked = Unlock;
    }

    public override bool CheckUnlocked()
    {
        return upgradeLoader.data.LassoPower_Unlocked;
    }
}
