using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitLaserBullet : MonoBehaviour {

    public GameObject LaserBullet;

    public float splitAngle = 30;

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Enemy")
        {
            // FIRST LASER
            // create laser object pointer and fetch from pool
            GameObject l;
            ObjectPoolsAccessor.instance.laserPool.TryGetNextObject(transform.localPosition, Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + splitAngle), out l);
            // assign follow vector
            l.GetComponent<FollowVector>().vec = new Vector3(
                Mathf.Cos(Mathf.Deg2Rad * (transform.rotation.eulerAngles.z + splitAngle)), 
                Mathf.Sin(Mathf.Deg2Rad * (transform.rotation.eulerAngles.z + splitAngle)),
                0);
            // assign speed
            l.GetComponent<FollowVector>().speed = gameObject.GetComponent<FollowVector>().speed;
            // assign disable time
            l.GetComponent<TimedDisable>().DisableTime = gameObject.GetComponent<TimedDisable>().DisableTime;
            // assign the animation delay
            Animate a = l.GetComponent<Animate>();
            a.delayBetweenFrames = gameObject.GetComponent<Animate>().delayBetweenFrames;

            // add the split component so it can fork again
            SplitLaserBullet s = l.AddComponent<SplitLaserBullet>();
            s.splitAngle = splitAngle;
            s.LaserBullet = LaserBullet;

            // SECOND LASER
            // fetch from object pool
            ObjectPoolsAccessor.instance.laserPool.TryGetNextObject(transform.localPosition, Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z - splitAngle), out l);
            // set follow vector
            l.GetComponent<FollowVector>().vec = new Vector3(
                Mathf.Cos(Mathf.Deg2Rad * (transform.rotation.eulerAngles.z - splitAngle)),
                Mathf.Sin(Mathf.Deg2Rad * (transform.rotation.eulerAngles.z - splitAngle)),
                0);
            // set speed
            l.GetComponent<FollowVector>().speed = gameObject.GetComponent<FollowVector>().speed;
            // set time till remove
            l.GetComponent<TimedDisable>().DisableTime = gameObject.GetComponent<TimedDisable>().DisableTime;
            // set animation delay 
            a = l.GetComponent<Animate>();
            a.delayBetweenFrames = gameObject.GetComponent<Animate>().delayBetweenFrames;
            // add fork component so it can fork again
            s = l.AddComponent<SplitLaserBullet>();
            s.splitAngle = splitAngle;
            s.LaserBullet = LaserBullet;

            // Disable the current laser and return it to the object pool
            gameObject.SetActive(false);
        }
    }
}
