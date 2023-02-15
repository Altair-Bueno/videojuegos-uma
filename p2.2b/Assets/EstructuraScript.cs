using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstructuraScript : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform[] childrenTransform;
    
    void Start()
    {
        childrenTransform = this.gameObject.GetComponentsInChildren<Transform>()[1..];
    }

    // Update is called once per frame
    void Update()
    {
        
        foreach(var x in childrenTransform) {
            print(x.transform.position);
        }

        if (Time.realtimeSinceStartup > 3) {
            foreach(var x in childrenTransform) {
                var position = x.transform.position;
                position.y = 1.5f;
                x.transform.position = position;
            }
        }

        if (Time.realtimeSinceStartup > 4) {
            var index = ((int)Mathf.Floor(Time.realtimeSinceStartup - 4));
            if (index < childrenTransform.Length) {
                childrenTransform[index].gameObject.SetActive(false);
            }
        } 
    }
}
