using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform firePoint;
    //public GameObject bulletPrefab;

    [SerializeField] private GameObject bulletPrefab;


    //public float bulletForce = 20f;
    [SerializeField] float speedPistol;
    [SerializeField] float speedShotgun;
    [SerializeField] float speedRifle;

    [SerializeField] AudioManager AM;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }


    void Shoot()
    {
        AM.AudioPlayerFireShot();
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * speedPistol, ForceMode2D.Impulse);
    }

    public void SetBulletPrefab(GameObject newBullet)
    {
        bulletPrefab = newBullet;
    }
}
