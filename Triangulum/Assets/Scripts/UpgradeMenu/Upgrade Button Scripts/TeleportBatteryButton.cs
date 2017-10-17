using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportBatteryButton : UpgradeButton
{

    public int BatteryLevel = 1;

    public int NewBattery = 2;

    public override void ApplyUpgrade()
    {
        upgradeLoader.data.Teleport_Charges = NewBattery;
        upgradeLoader.data.Teleport_UpgradeBattery = BatteryLevel;
    }

    public override bool CheckUnlocked()
    {
        return upgradeLoader.data.Teleport_UpgradeBattery >= BatteryLevel;
    }
}
