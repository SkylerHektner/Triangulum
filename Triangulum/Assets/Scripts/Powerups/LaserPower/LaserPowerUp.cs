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
        // create laser gameobject pointer
        GameObject l;
        for (int i = 0; i < numLasers; i++)
        {
            // fetch from object pool
            ObjectPoolsAccessor.instance.laserPool.requestObject(transform.localPosition, Quaternion.Euler(0, 0, -i * radialDeviance + 90), out l);
            // assign the vector it must follow
            l.GetComponent<FollowVector>().vec = new Vector3(
                Mathf.Sin(Mathf.Deg2Rad * i * radialDeviance), Mathf.Cos(Mathf.Deg2Rad * i * radialDeviance), 0);
            // set the lasers movement speed
            l.GetComponent<FollowVector>().speed = laserSpeed;
            // set the lasers disable time
            l.GetComponent<RemoveSelf>().timeTillRemove = laserExpirationTime;
            // set the animation time based on the duration of the laser
            Animate a = l.GetComponent<Animate>();
            a.delayBetweenFrames = laserExpirationTime / a.frames.Length;

            // if the laser has fork, configure the fork script to it
            if (Fork)
            {
                SplitLaserBullet s = l.GetComponent<SplitLaserBullet>();
                s.splitAngle = ForkRadialDeviance;
                s.LaserBullet = laser;
                s.canSplit = true;
            }
            
            // play the pickup sound
            gameObject.GetComponent<AudioSource>().PlayOneShot(LaserSound, .07f);
        }
        base.OnUse();
    }

    public override void OnEnd()
    {
        base.OnEnd();
    }
}
