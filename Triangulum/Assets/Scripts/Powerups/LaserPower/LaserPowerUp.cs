using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPowerUp : Powerup
{

    /// <summary>
    /// number of lasers the powerup generates
    /// </summary>
    public int numLasers;

    /// <summary>
    /// Speed of lasers in units per second
    /// </summary>
    public float laserSpeed = 40;

    /// <summary>
    /// time in seconds before lasers destroy themselves
    /// </summary>
    public float laserExpirationTime = 3;

    public AudioClip LaserSound;

    /// <summary>
    /// MUST POINT TO THE LASER PREFAB
    /// </summary>
    public GameObject laser;

    public bool Fork = true;

    public float ForkRadialDeviance = 30;

    public override void OnUse()
    {
        float radialDeviance = 360 / numLasers;
        for (int i = 0; i < numLasers; i++)
        {
            GameObject l = GameObject.Instantiate(laser);
            l.transform.localPosition = transform.localPosition;
            l.transform.localRotation = Quaternion.Euler(0, 0, -i * radialDeviance + 90);
            l.GetComponent<FollowVector>().vec = new Vector3(
                Mathf.Sin(Mathf.Deg2Rad * i * radialDeviance), Mathf.Cos(Mathf.Deg2Rad * i * radialDeviance), 0);
            l.GetComponent<FollowVector>().speed = laserSpeed;
            l.GetComponent<RemoveSelf>().timeTillRemove = laserExpirationTime;
            Animate a = l.GetComponent<Animate>();
            a.delayBetweenFrames = laserExpirationTime / a.frames.Length;

            if (Fork)
            {
                SplitLaserBullet s = l.AddComponent<SplitLaserBullet>();
                s.splitAngle = ForkRadialDeviance;
                s.LaserBullet = laser;
            }
            
            gameObject.GetComponent<AudioSource>().PlayOneShot(LaserSound, .07f);
        }
        base.OnUse();
    }

    public override void OnEnd()
    {
        base.OnEnd();
    }
}
