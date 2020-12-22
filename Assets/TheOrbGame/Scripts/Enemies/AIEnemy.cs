using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIEnemy : MonoBehaviour
{
    private Transform owner;
    private Transform target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        owner.LookAt(target);
    }

    internal void Configure(Transform owner, Transform target)
    {
        this.owner = owner;
        this.target = target;
    }
}
