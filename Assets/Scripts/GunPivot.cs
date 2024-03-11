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
    public float bulletLifeSpan = 4f;
    public int damage = 10;
    public float range = 5f;
    private bool canShoot = false;

    // Start is called before the first frame update
    void Start()
    {
        nextFireTime = Time.time; // Set the initial next fire time
    }

    // Update is called once per frame
    void Update()
    {
        RotateTowardsClosestEnemy();
        // Check if it's time to fire a bullet
        if (Time.time >= nextFireTime && canShoot)
        {
            Shoot(); // Call the Shoot method
            nextFireTime = Time.time + 1 / fireRate; // Update the next fire time
        }
    }

    void RotateTowardsClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemies.Length > 0)
        {
            Transform closestEnemy = null;
            float closestDistance = Mathf.Infinity;
            Vector3 playerPosition = transform.position;

            foreach (GameObject enemy in enemies)
            {
                float distanceToEnemy = Vector3.Distance(playerPosition, enemy.transform.position);
                if (distanceToEnemy < closestDistance)
                {
                    closestDistance = distanceToEnemy;
                    closestEnemy = enemy.transform;
                }
            }
            if(closestEnemy!=null && closestDistance < range){
                 canShoot = true;

                 // If closest enemy is found, rotate the player towards it
                Vector3 directionToEnemy = closestEnemy.position - playerPosition;
                float angle = Mathf.Atan2(directionToEnemy.y, directionToEnemy.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

                
            }else{
                 canShoot = false;
            }

            
        }else{
            canShoot = false;
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

        // Destroy the bullet after a certain lifespan
        Destroy(bullet, bulletLifeSpan);
    }
}

