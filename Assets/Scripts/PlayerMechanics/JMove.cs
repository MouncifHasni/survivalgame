using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JMove : MonoBehaviour
{

    public Joystick movementJoystick;
    public float playerSpeed;
    public Rigidbody2D rb;

    public Joystick rotationJoystick;
    public float rotationSpeed;

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

        // Rotate the player based on joystick input
        if (rotationJoystick.Direction != Vector2.zero)
        {
            // Calculate the angle of rotation based on joystick input
            float angle = Mathf.Atan2(rotationJoystick.Direction.y, rotationJoystick.Direction.x) * Mathf.Rad2Deg;

            // Apply rotation to the player
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}
