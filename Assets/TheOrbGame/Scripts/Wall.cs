using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    internal void UpdatePlayerPosition(Vector3 point, float distance)
    {
        float chunk = (.02f * (distance)) - .2f;

        GetComponent<Renderer>().sharedMaterial.SetVector("Position", new Vector4(point.x, 0, 0, 0));
        GetComponent<Renderer>().sharedMaterial.SetFloat("Distance", Mathf.Clamp(chunk, 0, .2f));
    }
}
