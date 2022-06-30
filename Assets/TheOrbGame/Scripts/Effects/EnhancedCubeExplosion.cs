using RayFire;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnhancedCubeExplosion : MonoBehaviour
{
    public float Strength = 10;

    void Awake()
    {
        var rf = this.GetComponentInChildren<RayfireBomb>();
        rf.strength = Strength;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
