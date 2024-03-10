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

        // Rotate the player towards the closest enemy
        RotateTowardsClosestEnemy();
        /*
        // Rotate the player based on joystick input
        if (rotationJoystick.Direction != Vector2.zero)
        {
            // Calculate the angle of rotation based on joystick input
            float angle = Mathf.Atan2(rotationJoystick.Direction.y, rotationJoystick.Direction.x) * Mathf.Rad2Deg;

            // Apply rotation to the player
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }*/
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

        // If closest enemy is found, rotate the player towards it
        if (closestEnemy != null)
        {
            Vector3 directionToEnemy = closestEnemy.position - playerPosition;
            float angle = Mathf.Atan2(directionToEnemy.y, directionToEnemy.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}

}
