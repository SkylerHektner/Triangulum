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

	// Use this for initialization
	void Start () {
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
        }
        else if (power == "Drone")
        {
            DronePowerUp d = powerUp.GetComponent<DronePowerUp>();
            d.numDrones = data.DronePower_NumDrones;
            d.duration = data.DronePower_DroneDuration;
            d.laserCoolDown = data.DronePower_DroneLaserCooldown;
        }
        else if (power == "Laser")
        {
            LaserPowerUp l = powerUp.GetComponent<LaserPowerUp>();
            l.numLasers = data.LaserPower_NumLasers;
            l.laserExpirationTime = data.LaserPower_LaserDuration;
            l.laserSpeed = data.LaserPower_LaserSpeed;
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
    public bool DronePower_Unlocked = true;
    public float DronePower_DropChance = .1f;
    public float DronePower_DroneDuration = 10;
    public int DronePower_NumDrones = 3;
    public float DronePower_DroneLaserCooldown = 2;
    // Laser Power Up
    public bool LaserPower_Unlocked = true;
    public float LaserPower_DropChance = .1f;
    public int LaserPower_NumLasers = 60;
    public float LaserPower_LaserSpeed = 40;
    public float LaserPower_LaserDuration = 3;
    // Lasso Power Up
    public bool LassoPower_Unlocked = true;
    public float LassoPower_DropChance = .1f;
    public float LassoPower_LassoDuration = 5;
    // Speed Power Up
    public bool SpeedPower_Unlocked = true;
    public float SpeedPower_DropChance = .1f;
    public float SpeedPower_SpeedDuration = 4;
    public float SpeedPower_Multiplier = 1.5f;
    // ice Power Up
    public bool IcePower_Unlocked = true;
    public float IcePower_DropChance = .1f;
    public float IcePower_Radius = 60;
    public float IcePower_ExpansionTime = 1;
    public float IcePower_FreezeDuration = 5;

    // CHARACTER UPGRADES
    // health
    public int Player_Health = 1;
    // movement
    public float Player_Speed = 50;
    public float Player_Acceleration = 500;

    // ABILITIES
    // teleport
    public bool Teleport_CanTeleport = false;
    public float Teleport_Cooldown = 10;
    public bool Teleport_Lethal = true;
    public float Teleport_LethalRadius = 10;
}
