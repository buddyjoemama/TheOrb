using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public Transform shieldBase;

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
        shieldBase.gameObject.SetActive(false);
    }

    internal void Activate()
    {
        shieldBase.gameObject.SetActive(true);
    }

    internal void Move(Vector3 position)
    {
        shieldBase.position = position;
    }
}
