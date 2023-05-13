using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    public Rigidbody rigidBody;
    public float speed = 1;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("w"))
        {
            rigidBody.AddForce(Vector3.forward * speed);
        }

        if (Input.GetKey("a"))
        {
            rigidBody.AddForce(Vector3.left * speed);
        }

        if (Input.GetKey("d"))
        {
            rigidBody.AddForce(Vector3.right * speed);
        }

        if (Input.GetKey("s"))
        {
            rigidBody.AddForce(Vector3.back * speed);
        }
    }
}