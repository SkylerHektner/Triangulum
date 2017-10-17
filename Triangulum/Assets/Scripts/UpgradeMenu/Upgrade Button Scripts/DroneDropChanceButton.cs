using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneDropChanceButton : UpgradeButton
{

    public int DropChanceLevel = 1;

    public float NewDropChance = 10;

    public override void ApplyUpgrade()
    {
        upgradeLoader.data.DronePower_DropChance = NewDropChance;
        upgradeLoader.data.DronePower_UpgradeDropChange = DropChanceLevel;
    }

    public override bool CheckUnlocked()
    {
        return upgradeLoader.data.DronePower_UpgradeDropChange >= DropChanceLevel;
    }
}
