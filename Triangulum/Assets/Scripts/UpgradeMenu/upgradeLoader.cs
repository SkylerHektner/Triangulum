using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class upgradeLoader : MonoBehaviour {

    string path;
    string jsonString;

    /// <summary>
    /// controls if you need to reset the upgrades JSON to default values on play, if false then tries to load
    /// upgrade info from an existing JSON in the folder
    /// </summary>
    public bool resetDataOnPlay = true;

    /// <summary>
    /// A public static accescable data class for all upgrade info
    /// </summary>
    public static UpgradeData data;

    public static upgradeLoader Instance;

	// Use this for initialization
	void Start () {
        // set Instance
        Instance = this;

        // set the path to the upgrades Json and load the data contained or create a fresh data set
        path = Application.streamingAssetsPath + "/Upgrades.json";
        if (resetDataOnPlay)
        {
            data = new UpgradeData();
            jsonString = JsonUtility.ToJson(data);
            SaveData();
        }
        else
        {
            jsonString = File.ReadAllText(path);
            data = JsonUtility.FromJson<UpgradeData>(jsonString);
        }

        // adjust the player
        try
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            adjustPlayer(p);
        }
        catch
        {
            Debug.LogError("You don't have a player in the scene you dummy!");
        }
	}

    public void SaveData()
    {
        jsonString = JsonUtility.ToJson(data);
        using (StreamWriter file = new StreamWriter(path))
        {
            file.WriteLine(jsonString);
        }
    }

    /// <summary>
    /// Takes in the GameObject of the player and sets their default settings to what the JSON dictates
    /// </summary>
    /// <param name="player"></param>
    public static void adjustPlayer(GameObject player)
    {
        PlayerDeath d = player.GetComponent<PlayerDeath>();
        d.Health = data.Player_Health;
        PlayerMovement m = player.GetComponent<PlayerMovement>();
        m.speed = data.Player_Speed;
        m.acceleration = data.Player_Acceleration;
    }

    /// <summary>
    /// Takes in a powerUp GameObject and a string describing what power up it is taking in, then
    /// applies data from the upgrade database to that powerup config.
    /// </summary>
    /// <param name="powerUp"></param>
    /// <param name="power"></param>
    public static void adjustPowerUp(GameObject powerUp, string power)
    {
        if (power == "Speed")
        {
            SpeedPowerUp s = powerUp.GetComponent<SpeedPowerUp>();
            s.speedMultiplier = data.SpeedPower_Multiplier;
            s.duration = data.SpeedPower_SpeedDuration;
            s.train = data.SpeedPower_Train;
            s.trainRadius = data.SpeedPower_TrainRadius;
        }
        else if (power == "Drone")
        {
            DronePowerUp d = powerUp.GetComponent<DronePowerUp>();
            d.numDrones = data.DronePower_NumDrones;
            d.duration = data.DronePower_DroneDuration;
            d.laserCoolDown = data.DronePower_DroneLaserCooldown;
            d.spikedBodies = data.DronePower_SpikedBodies;
            d.spikedBodiesRadius = data.DronePower_SpikedBodiesRadius;
            d.orbitRadius = data.DrownPower_OrbitRadius;
        }
        else if (power == "Laser")
        {
            LaserPowerUp l = powerUp.GetComponent<LaserPowerUp>();
            l.numLasers = data.LaserPower_NumLasers;
            l.laserExpirationTime = data.LaserPower_LaserDuration;
            l.laserSpeed = data.LaserPower_LaserSpeed;
            l.Fork = data.LaserPower_Fork;
            l.ForkRadialDeviance = data.LaserPower_ForkRadialDeviance;
        }
        else if (power == "Lasso")
        {
            LassoPowerUp l = powerUp.GetComponent<LassoPowerUp>();
            l.duration = data.LassoPower_LassoDuration;
        }
        else if (power == "Ice")
        {
            IcePowerUp i = powerUp.GetComponent<IcePowerUp>();
            i.freezeExpansionTime = data.IcePower_ExpansionTime;
            i.freezeRadius = data.IcePower_Radius;
            i.freezeDuration = data.IcePower_FreezeDuration;
            i.timeTillExpire = data.IcePower_TimeTillExpire;
        }
        else if (power == "Shield")
        {
            ShieldPowerUp s = powerUp.GetComponent<ShieldPowerUp>();
            s.durability = data.ShieldPower_Durability;
            s.duration = data.ShieldPower_Duration;
            s.radius = data.ShieldPower_Radius;
            s.flickerDuration = data.ShieldPower_flickerDuration;
            s.unbreakable = data.ShieldPower_Unbreakable;
        }

        else
        {
            Debug.LogError("You tried to modify a powerup that does not exist");
        }
    }
	
}

/// <summary>
/// In this class we hold all upgrade data for the character and also set default values.
/// </summary>
[System.Serializable]
public class UpgradeData
{
    // POWER UPS
    // Drone Power Up
    public bool DronePower_Unlocked = false;
    public float DronePower_DropChance = .05f;
    public float DronePower_DroneDuration = 10;
    public int DronePower_NumDrones = 3;
    public float DronePower_DroneLaserCooldown = 2;
    public bool DronePower_SpikedBodies = false;
    public float DronePower_SpikedBodiesRadius = 4;
    public float DrownPower_OrbitRadius = 8;

    public int DronePower_UpgradeNumDrones = 0;
    public int DronePower_UpgradeDuration = 0;
    public int DronePower_UpgradeDropChange = 0;
    // Laser Power Up
    public bool LaserPower_Unlocked = false;
    public float LaserPower_DropChance = .05f;
    public int LaserPower_NumLasers = 20;
    public float LaserPower_LaserSpeed = 60;
    public float LaserPower_LaserDuration = 2;
    public bool LaserPower_Fork = false;
    public float LaserPower_ForkRadialDeviance = 30;

    public int LaserPower_UpgradeDropChange = 0;
    public int LaserPower_UpgradeRange = 0;
    public int LaserPower_UpgradeNumLaser = 0;
    // Lasso Power Up
    public bool LassoPower_Unlocked = false;
    public float LassoPower_DropChance = .05f;
    public float LassoPower_LassoDuration = 5;
    public bool LassoPower_InstantLoop = false;

    public int LassoPower_UpgradeDropChance = 0;
    public int LassoPower_UpgradeDuration = 0;
    // Speed Power Up
    public bool SpeedPower_Unlocked = false;
    public float SpeedPower_DropChance = .05f;
    public float SpeedPower_SpeedDuration = 4;
    public float SpeedPower_Multiplier = 1.5f;
    public bool SpeedPower_Train = false;
    public float SpeedPower_TrainRadius = 5f;

    public int SpeedPower_UpgradeDropChange = 0;
    public int SpeedPower_UpgradeDuration = 0;
    public int SpeedPower_UpgradeMultiplier = 0;
    // Ice Power Up
    public bool IcePower_Unlocked = false;
    public float IcePower_DropChance = .05f;
    public float IcePower_Radius = 60;
    public float IcePower_ExpansionTime = .3f;
    public float IcePower_FreezeDuration = 5;
    public float IcePower_TimeTillExpire = 2;

    public int IcePower_UpgradeRadius = 0;
    public int IcePower_UpgradeDuration = 0;
    public int IcePower_UpgradeDropChange = 0;
    // shield Power Up
    public bool ShieldPower_Unlocked = false;
    public float ShieldPower_DropChance = .05f;
    public float ShieldPower_Duration = 5;
    public float ShieldPower_Radius = 6;
    public int ShieldPower_Durability = 1;
    public float ShieldPower_flickerDuration = 2f;
    public bool ShieldPower_Unbreakable = false;

    public int ShieldPower_UpgradeDurability = 0;
    public int ShieldPower_UpgradeDropChance = 0;
    public int ShieldPower_UpgradeDuration = 0;
    // CHARACTER UPGRADES
    // health
    public int Player_Health = 1;
    // movement
    public float Player_Speed = 50;
    public float Player_Acceleration = 500;
    // Credits
    public int Player_TaxPayerDollars = 10000;

    // ABILITIES
    // teleport
    public bool Teleport_CanTeleport = false;
    public float Teleport_Cooldown = 10;
    public bool Teleport_Lethal = false;
    public float Teleport_LethalRadius = 10;
    public int Teleport_Charges = 1;

    public int Teleport_UpgradeRecharge = 0;
    public int Teleport_UpgradeBattery = 0;
}
