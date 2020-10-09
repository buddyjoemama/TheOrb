using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody projectile;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal void Fire(Vector3 forward)
    {
        projectile = GetComponent<Rigidbody>();
        projectile.velocity = forward * 50;
        Destroy(projectile.gameObject, 2f);
    }
}
