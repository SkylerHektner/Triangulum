using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportLethalButton : UpgradeButton
{

    public bool Unlock = true;

    public override void ApplyUpgrade()
    {
        upgradeLoader.data.Teleport_Lethal = Unlock;
    }

    public override bool CheckUnlocked()
    {
        return upgradeLoader.data.Teleport_Lethal;
    }
}
