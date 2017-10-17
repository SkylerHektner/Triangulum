using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneSpikedBodiesButton : UpgradeButton
{

    public bool Unlock = true;


    public override void ApplyUpgrade()
    {
        upgradeLoader.data.DronePower_SpikedBodies = Unlock;
    }

    public override bool CheckUnlocked()
    {
        return upgradeLoader.data.DronePower_SpikedBodies;
    }
}
