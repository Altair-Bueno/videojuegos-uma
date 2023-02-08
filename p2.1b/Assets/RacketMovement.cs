using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacketMovement : MonoBehaviour
{
    public GameObject ball; 
    public Vector3 speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var distance = Mathf.Abs(ball.transform.position.y - this.transform.position.y);
        transform.position += distance * speed;
    }
}
