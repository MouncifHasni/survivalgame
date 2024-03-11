using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JMove : MonoBehaviour
{

    public Joystick movementJoystick;
    public float playerSpeed;
    public Rigidbody2D rb;
    public int health = 100;
    

    private void FixedUpdate()
    {
        // Move the player based on joystick input
        if (movementJoystick.Direction.y != 0)
        {
            rb.velocity = new Vector2(movementJoystick.Direction.x * playerSpeed, movementJoystick.Direction.y * playerSpeed);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
        
    }

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

}
