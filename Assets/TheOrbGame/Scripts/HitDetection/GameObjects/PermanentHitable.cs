using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Anything that responds to being hit but cant be destroyed.
/// </summary>
public class PermanentHitable : AbstractHittable
{
    public override void Hit(Transform collider, Transform transform, RaycastHit hitPoint, BasicProjectile projectile)
    {
        
    }
}
