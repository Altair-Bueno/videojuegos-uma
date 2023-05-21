using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PickupRotationAnimation : MonoBehaviour
{
    public float speed = 100f;
    
    void Update()
    {
        transform.Rotate(Vector3.up, Time.deltaTime * speed);
    }
}