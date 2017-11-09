using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitLaserBullet : MonoBehaviour {

    public GameObject LaserBullet;

    public float splitAngle = 30;

    public bool canSplit = false;

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (canSplit && collider.gameObject.tag == "Enemy")
        {
            Debug.Log("I split");
            // FIRST LASER
            // create laser object pointer and fetch from pool
            GameObject l;
            ObjectPoolsAccessor.instance.laserPool.requestObject(transform.localPosition, Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + splitAngle), out l);
            // assign follow vector
            l.GetComponent<FollowVector>().vec = new Vector3(
                Mathf.Cos(Mathf.Deg2Rad * (transform.rotation.eulerAngles.z + splitAngle)), 
                Mathf.Sin(Mathf.Deg2Rad * (transform.rotation.eulerAngles.z + splitAngle)),
                0);
            // assign speed
            l.GetComponent<FollowVector>().speed = gameObject.GetComponent<FollowVector>().speed;
            // assign disable time
            l.GetComponent<RemoveSelf>().timeTillRemove = gameObject.GetComponent<RemoveSelf>().timeTillRemove;
            // assign the animation delay
            Animate a = l.GetComponent<Animate>();
            a.delayBetweenFrames = gameObject.GetComponent<Animate>().delayBetweenFrames;

            // configure the split component so it can fork again
            SplitLaserBullet s = l.GetComponent<SplitLaserBullet>();
            s.splitAngle = splitAngle;
            s.LaserBullet = LaserBullet;
            s.canSplit = true;

            // SECOND LASER
            // fetch from object pool
            ObjectPoolsAccessor.instance.laserPool.requestObject(transform.localPosition, Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z - splitAngle), out l);
            // set follow vector
            l.GetComponent<FollowVector>().vec = new Vector3(
                Mathf.Cos(Mathf.Deg2Rad * (transform.rotation.eulerAngles.z - splitAngle)),
                Mathf.Sin(Mathf.Deg2Rad * (transform.rotation.eulerAngles.z - splitAngle)),
                0);
            // set speed
            l.GetComponent<FollowVector>().speed = gameObject.GetComponent<FollowVector>().speed;
            // set time till remove
            l.GetComponent<RemoveSelf>().timeTillRemove = gameObject.GetComponent<RemoveSelf>().timeTillRemove;
            // set animation delay 
            a = l.GetComponent<Animate>();
            a.delayBetweenFrames = gameObject.GetComponent<Animate>().delayBetweenFrames;
            // configure fork component so it can fork again
            s = l.GetComponent<SplitLaserBullet>();
            s.splitAngle = splitAngle;
            s.LaserBullet = LaserBullet;
            s.canSplit = true;

            // Disable the current laser and return it to the object pool
            gameObject.SetActive(false);
        }
    }
}
