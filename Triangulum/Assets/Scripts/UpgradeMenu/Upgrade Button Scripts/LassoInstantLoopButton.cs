using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LassoInstantLoopButton : UpgradeButton
{

    public bool Unlock = true;


    public override void ApplyUpgrade()
    {
        upgradeLoader.data.LassoPower_InstantLoop = Unlock;
    }

    public override bool CheckUnlocked()
    {
        return upgradeLoader.data.LassoPower_InstantLoop;
    }
}
