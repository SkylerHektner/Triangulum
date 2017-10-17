using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneNumDronesButton : UpgradeButton
{

    public int NumDronesLevel = 1;

    public int NewNumDrones = 10;

    public override void ApplyUpgrade()
    {
        upgradeLoader.data.DronePower_NumDrones = NewNumDrones;
        upgradeLoader.data.DronePower_UpgradeNumDrones = NumDronesLevel;
    }

    public override bool CheckUnlocked()
    {
        return upgradeLoader.data.DronePower_UpgradeNumDrones >= NumDronesLevel;
    }
}
