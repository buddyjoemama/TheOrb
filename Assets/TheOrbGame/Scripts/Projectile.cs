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
        projectile.velocity = new Vector3(1, 0) * 50;
        Destroy(projectile.gameObject, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal void Fire()
    {
        projectile = GetComponent<Rigidbody>();
    }
}
