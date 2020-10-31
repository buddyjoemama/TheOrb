using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunRig : MonoBehaviour
{
    public Transform firePoint;

    public void Fire(BasicProjectile projectile)
    {
        BasicProjectile clone = Instantiate(projectile, firePoint.transform.position, firePoint.rotation);
        clone.Fire(firePoint.forward, firePoint.position);
    }

    internal void Move(Vector3 position)
    {
        transform.position = position;
    }
}