using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Shoot : MonoBehaviour
{
    public float recoil = 1f;

    public GameObject bulletPrefab;
    public Transform firePoint;
    public Rigidbody2D rb;

    void Update() {
        rb = GetComponent<Rigidbody2D>();
        if (Input.GetKeyDown(KeyCode.Space)) shoot();
    }

    private void shoot() {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.direction = transform.rotation;

        rb.AddForce(transform.up * -recoil, ForceMode2D.Impulse);
    }
}
