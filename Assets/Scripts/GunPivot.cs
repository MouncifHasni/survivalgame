using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPivot : MonoBehaviour
{
    public GameObject bulletPrefab; // The prefab of the bullet object
    public Transform firePoint; // The point from where the bullet will be spawned
    public float fireRate = 0.5f; // The rate of fire in bullets per second
    public float bulletSpeed = 10f; // The speed of the bullets
    private float nextFireTime; // The time when the next bullet can be fired
    public int damage = 10;

    // Start is called before the first frame update
    void Start()
    {
        nextFireTime = Time.time; // Set the initial next fire time
    }

    // Update is called once per frame
    void Update()
    {
        // Check if it's time to fire a bullet
        if (Time.time >= nextFireTime)
        {
            Shoot(); // Call the Shoot method
            nextFireTime = Time.time + 1 / fireRate; // Update the next fire time
        }
    }

    void Shoot()
    {
        // Calculate the direction in which to shoot the bullet based on the rotation of the fire point
        Vector2 direction = firePoint.right;

        // Calculate the rotation to apply to the bullet prefab
        Quaternion rotation = Quaternion.Euler(0, 0, -90);

        // Instantiate a new bullet object at the fire point position and rotation
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation * rotation);
        bullet.GetComponent<Bullet>().Initialize(damage);

        // Get the Rigidbody2D component of the bullet
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            // Add velocity to the bullet in the calculated direction
            rb.velocity = direction * bulletSpeed;
        }
    }
}

