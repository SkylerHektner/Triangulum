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

    public static UpgradeData data;

	// Use this for initialization
	void Start () {
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
	}

    public void SaveData()
    {
        jsonString = JsonUtility.ToJson(data);
        using (StreamWriter file = new StreamWriter(path))
        {
            file.WriteLine(jsonString);
        }
    }
	
}

/// <summary>
/// In this class we hold all upgrade data for the character and also control default values.
/// </summary>
[System.Serializable]
public class UpgradeData
{
    // POWER UPS
    // Drone Power Up
    public bool DronePower_Unlocked = true;
    public float DronePower_DroneDuration = 10;
    public float DronePower_NumDrones = 3;
    public float DronePower_DroneLaserCooldown = 2;
    // Laser Power Up
    public bool LaserPower_Unlocked = true;
    public int LaserPower_NumLasers = 60;
    public float LaserPower_LaserSpeed = 40;
    public float LaserPower_LaserDuration = 3;
    // Lasso Power Up
    public bool LassoPower_Unlocked = true;
    public float LassoPower_LassoDuration = 5;
    // Speed Power Up
    public bool SpeedPower_Unlocked = true;
    public float SpeedPower_SpeedDuration = 4;
    public float SpeedPower_Multiplier = 1.5f;

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
