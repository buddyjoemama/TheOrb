﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjecileMove : MonoBehaviour
{
    public float speed;
    public float fireRate;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * (speed * Time.deltaTime);    
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}