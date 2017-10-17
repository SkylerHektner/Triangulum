using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportRechargeButton : UpgradeButton
{

    public int RechargeLevel = 1;

    public float NewRecharge = 10;

    public override void ApplyUpgrade()
    {
        upgradeLoader.data.Teleport_Cooldown = NewRecharge;
        upgradeLoader.data.Teleport_UpgradeRecharge = RechargeLevel;
    }

    public override bool CheckUnlocked()
    {
        return upgradeLoader.data.Teleport_UpgradeRecharge >= RechargeLevel;
    }
}
