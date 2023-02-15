using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public const float SPEED = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Si no uso rigid body, el movimiento va en el update
        var horizontal = Input.GetAxis("Horizontal");
        this.transform.Translate(Vector3.right * horizontal * SPEED * Time.deltaTime);
    }
}
