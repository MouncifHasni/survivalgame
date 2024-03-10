using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int damage;

    public void Initialize(int damage)
    {
        this.damage = damage;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the bullet collides with an enemy
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("HIT");
            // Call a method on the enemy script to apply damage
            other.GetComponent<EnemyController>().TakeDamage(damage);

            // Destroy the bullet upon collision with an enemy
            Destroy(gameObject);
        }
    }
}
