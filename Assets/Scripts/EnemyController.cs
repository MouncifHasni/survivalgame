using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int damage;
    public int health;
    public float speed = 3f;
    private GameObject playerObject; 
    
   // Speed at which the enemy moves towards the player
    private Transform playerTransform;
    public SpriteRenderer spriteHead;
    private Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        // Find the player GameObject using its tag
        playerObject = GameObject.FindGameObjectWithTag("Player");
        // Get the Rigidbody2D component attached to the enemy GameObject
        rb = GetComponent<Rigidbody2D>();

        if (playerObject != null)
        {
            // Get the Transform component of the player GameObject
            playerTransform = playerObject.transform;
        }
        else
        {
            Debug.LogError("Player GameObject not found");
        }
    }

    void FixedUpdate()
    {
        if (playerObject != null)
        {
            // Move the enemy towards the player
            MoveTowardsPlayer(playerTransform);

            
        }
    }


    void MoveTowardsPlayer(Transform playerTransform)
    {
        // Calculate direction towards the player
        Vector2 direction = (playerTransform.position - transform.position).normalized;

        //Look at Player
        // Flip the sprite horizontally if the player is on the right side
        spriteHead.flipX = direction.x > 0;

        // Move the enemy towards the player
        rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
        
    }

    public void AttackPlayer()
    {
        //playerObject.TakeDamage(damage);
    }

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the bullet collides with an enemy
        if (other.CompareTag("Player"))
        {
            // Call a method on the Player script to apply damage
            playerObject.GetComponent<JMove>().TakeDamage(damage);

        }
    }
}   
