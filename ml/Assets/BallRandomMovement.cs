using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallRandomMovement : MonoBehaviour
{
    public Rigidbody ballRigidbody;

    public float maxForce = .4f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        var x = Random.Range(-1f, 1f);
        var y = Random.Range(-1f, 1f);
        var v = new Vector3(x, y);
        ballRigidbody.AddForce(v * maxForce);
    }
}
