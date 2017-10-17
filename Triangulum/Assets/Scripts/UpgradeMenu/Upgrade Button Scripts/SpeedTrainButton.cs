using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedTrainButton : UpgradeButton
{

    public bool Unlock = true;


    public override void ApplyUpgrade()
    {
        upgradeLoader.data.SpeedPower_Train = Unlock;
    }

    public override bool CheckUnlocked()
    {
        return upgradeLoader.data.SpeedPower_Train;
    }
}
