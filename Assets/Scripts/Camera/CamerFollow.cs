using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerFollow : MonoBehaviour
{
    public float FollowSpeed = 2f;
    public float yOffset = -10f;
    public Transform target;


    private void Start()
    {

        transform.position = target.position;
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 newPos = new Vector3(target.position.x, target.position.y + yOffset, -10f);
        transform.position = Vector3.Slerp(transform.position, newPos, FollowSpeed * Time.deltaTime);



    }
}
