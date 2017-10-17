using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldUnlockButton : UpgradeButton
{

    public bool Unlock = true;


    public override void ApplyUpgrade()
    {
        upgradeLoader.data.ShieldPower_Unlocked = Unlock;
    }

    public override bool CheckUnlocked()
    {
        return upgradeLoader.data.ShieldPower_Unlocked;
    }
}
