using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceUnlockButton : UpgradeButton
{

    public bool Unlock = true;


    public override void ApplyUpgrade()
    {
        upgradeLoader.data.IcePower_Unlocked = Unlock;
    }

    public override bool CheckUnlocked()
    {
        return upgradeLoader.data.IcePower_Unlocked;
    }
}
