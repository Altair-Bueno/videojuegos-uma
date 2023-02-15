using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere : MonoBehaviour
{
    public const float MOVEMENT_DURATION = 1;
    public const float SPEED = 1;
    public GameObject enemy;
    public float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var distance = Vector3.Distance(this.transform.position, enemy.transform.position);

        if (distance <= 3f) {
            timer = MOVEMENT_DURATION;
        }

        if (timer > 0) {
            timer -= Time.deltaTime;
            // normalized: Vector en forma normal
            // deltaTime: Porque no sabemos cuando se va a producir el update y 
            // puede haber varianza en los fps
            this.transform.Translate((this.transform.position - enemy.transform.position).normalized * SPEED * Time.deltaTime);
        }
    }
}
