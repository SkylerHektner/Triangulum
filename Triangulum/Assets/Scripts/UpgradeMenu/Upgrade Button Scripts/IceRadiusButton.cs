using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceRadiusButton : UpgradeButton
{

    public int RadiusLevel = 1;

    public float NewRadius = 10;

    public override void ApplyUpgrade()
    {
        upgradeLoader.data.IcePower_Radius = NewRadius;
        upgradeLoader.data.IcePower_UpgradeRadius = RadiusLevel;
    }

    public override bool CheckUnlocked()
    {
        return upgradeLoader.data.IcePower_UpgradeRadius >= RadiusLevel;
    }
}
