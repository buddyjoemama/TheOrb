using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal void Deactivate()
    {
        gameObject.SetActive(false);
    }

    internal void Activate()
    {
        gameObject.SetActive(true);
    }

    internal void Move(Vector3 position)
    {
        transform.position = position;
    }
}
