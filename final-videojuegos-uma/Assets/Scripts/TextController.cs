using System.Collections;
using TMPro;
using UnityEngine;

public class TextController : MonoBehaviour
{
    public TextMeshPro text;
    public float speed = .1f;
    public float stickDelay = 1;

    public IEnumerator UpdateText(string content)
    {
        text.enabled = true;
        Clear();
        for (var i = 0; i <= content.Length; i++)
        {
            text.text = content.Substring(0, i);
            yield return new WaitForSeconds(speed);
        }

        yield return new WaitForSeconds(stickDelay);
        text.enabled = false;
    }

    public void Clear()
    {
        text.text = "";
    }
}