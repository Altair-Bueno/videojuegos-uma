using UnityEngine;

public class PickupRotationAnimation : MonoBehaviour
{
    public float speed = 100f;

    private void Update()
    {
        transform.Rotate(Vector3.up, Time.deltaTime * speed);
    }
}