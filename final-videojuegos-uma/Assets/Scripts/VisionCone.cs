using System.Linq;
using UnityEngine;

public class VisionCone : MonoBehaviour
{
    public Vector3 offset = Vector3.zero;
    public float fieldOfView = 60;
    public float distance = 10;
    public LayerMask layerMask;
    public Color gizmosColor = Color.clear;

    private RaycastHit[] onSight = { };

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        var eyes = GetEyesPosition();
        onSight = Physics.SphereCastAll(eyes, distance, transform.forward, layerMask)
            .Where(x => Vector3.Angle(x.transform.position - eyes, transform.forward) <= fieldOfView)
            .Where(x => Physics.Raycast(eyes, x.transform.position - eyes, distance, layerMask))
            .ToArray();
    }

    private void OnDrawGizmos()
    {
        var eyes = GetEyesPosition();
        Gizmos.color = gizmosColor;
        Gizmos.DrawWireSphere(eyes, distance);
        var directions = new[]
        {
            (fieldOfView, 0f, 0f), (0f, fieldOfView, 0f), (0f, 0f, fieldOfView)
        };
        foreach (var (x, y, z) in directions)
        {
            var direction = Quaternion.Euler(x, y, z);
            Gizmos.DrawLine(eyes, (direction * transform.forward).normalized * distance + eyes);
        }
    }

    private Vector3 GetEyesPosition()
    {
        return offset + transform.position;
    }

    public RaycastHit[] GetOnSight()
    {
        return onSight;
    }
}