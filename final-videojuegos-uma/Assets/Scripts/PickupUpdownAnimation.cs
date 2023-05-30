using System.Collections;
using UnityEngine;

public class PickupUpdownAnimation : MonoBehaviour
{
    public AnimationCurve curve;
    public float animationDuration = 2;
    public float standbyDuration = 2;
    public float maxHeight = 1;

    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(nameof(PickupUpdownAnimationCorutine));
    }

    public IEnumerator PickupUpdownAnimationCorutine()
    {
        var origin = transform.position;
        var destination = origin + Vector3.up * maxHeight;

        while (true)
        {
            yield return TranslateCorutine(origin, destination);
            yield return new WaitForSeconds(standbyDuration);
            (origin, destination) = (destination, origin);
        }
    }

    private IEnumerator TranslateCorutine(Vector3 origin, Vector3 destination)
    {
        for (var timer = 0f; timer < animationDuration; timer += Time.deltaTime)
        {
            var value = curve.Evaluate(timer / animationDuration);
            transform.position = Vector3.Lerp(origin, destination, value);
            yield return null;
        }

        transform.position = destination;
    }
}