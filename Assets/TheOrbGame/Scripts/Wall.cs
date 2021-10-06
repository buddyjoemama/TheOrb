using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : AbstractHittable
{
    public override void Hit(Transform collider, Transform transform, RaycastHit hit, BasicProjectile projectile)
    {
        if (projectileHitEffect != null)
        {
            Instantiate(projectileHitEffect, hit.point, Quaternion.FromToRotation(Vector3.forward, hit.normal));
        }
    }
}
