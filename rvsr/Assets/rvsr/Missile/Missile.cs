using rvsr.Rabbit;
using rvsr.Robot.State.Attack;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public Rigidbody rigidbody;
    public float duration = 10;
    public float speed = 10;
    public MissileState missileState;

    private float timer;

    // Start is called before the first frame update
    private void Start()
    {
        timer = duration;
    }

    // Update is called once per frame
    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0) missileState.OnMissileHitFallback();
    }

    private void FixedUpdate()
    {
        var moved = speed * Time.deltaTime * transform.forward;
        rigidbody.MovePosition(transform.position + moved);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Rabbit"))
        {
            missileState.OnRabbitMissileHit(collision.gameObject.GetComponent<Rabbit>());
            return;
        }

        missileState.OnMissileHitFallback();
    }
}