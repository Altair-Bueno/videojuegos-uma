using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circles : MonoBehaviour
{
    public float speed = 10;
    Rigidbody rb;
    
    private void Start() {
        rb = GetComponent<Rigidbody>();
    }

    void Update() {
        transform.Rotate(-1 * Vector3.up * speed * Time.deltaTime);
        //transform.Rotate(Vector3.up * Input.GetAxis("Horizontal") * velocGiro * Time.deltaTime);
        //rb.velocity = transform.forward* Input.GetAxis("Vertical") * velocFrontal;
    }

    private void OnCollisionEnter(Collision colision) {
        print("Colisión con " + colision.collider.gameObject.name+" con Tag= " +colision.collider.gameObject.tag);
    }
}
