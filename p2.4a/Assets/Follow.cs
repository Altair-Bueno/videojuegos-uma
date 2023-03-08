using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public GameObject follow;
    public float speed = 5;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(follow.transform, Vector3.up);
        //transform.position+=Vector3.up * speed * Time.deltaTime;
        //transform.position += Vector3.up * speed * Time.deltaTime;
        //transform.Rotate(Vector3.up * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision colision) {
        print("Colisión con " + colision.collider.gameObject.name+" con Tag= " +colision.collider.gameObject.tag);
    }
}
