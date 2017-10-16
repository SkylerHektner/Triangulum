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
            GameObject l = GameObject.Instantiate(LaserBullet);
            l.transform.localPosition = transform.localPosition;
            l.transform.localRotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + splitAngle);

            l.GetComponent<FollowVector>().vec = new Vector3(
                Mathf.Cos(Mathf.Deg2Rad * (transform.rotation.eulerAngles.z + splitAngle)), 
                Mathf.Sin(Mathf.Deg2Rad * (transform.rotation.eulerAngles.z + splitAngle)),
                0);

            l.GetComponent<FollowVector>().speed = gameObject.GetComponent<FollowVector>().speed;
            l.GetComponent<RemoveSelf>().timeTillRemove = gameObject.GetComponent<RemoveSelf>().timeTillRemove;
            Animate a = l.GetComponent<Animate>();
            a.delayBetweenFrames = gameObject.GetComponent<Animate>().delayBetweenFrames;

            SplitLaserBullet s = l.AddComponent<SplitLaserBullet>();
            s.splitAngle = splitAngle;
            s.LaserBullet = LaserBullet;

            // SECOND LASER
            l = GameObject.Instantiate(LaserBullet);
            l.transform.localPosition = transform.localPosition;
            l.transform.localRotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z - splitAngle);

            l.GetComponent<FollowVector>().vec = new Vector3(
                Mathf.Cos(Mathf.Deg2Rad * (transform.rotation.eulerAngles.z - splitAngle)),
                Mathf.Sin(Mathf.Deg2Rad * (transform.rotation.eulerAngles.z - splitAngle)),
                0);

            l.GetComponent<FollowVector>().speed = gameObject.GetComponent<FollowVector>().speed;
            l.GetComponent<RemoveSelf>().timeTillRemove = gameObject.GetComponent<RemoveSelf>().timeTillRemove;
            a = l.GetComponent<Animate>();
            a.delayBetweenFrames = gameObject.GetComponent<Animate>().delayBetweenFrames;

            s = l.AddComponent<SplitLaserBullet>();
            s.splitAngle = splitAngle;
            s.LaserBullet = LaserBullet;

            // Destroy the current laser
            Destroy(gameObject);
        }
    }
}
