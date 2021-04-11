using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyHitable : Hitable
{

    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();
    }

    public override void Hit(Transform collider, Transform transform, RaycastHit hitPoint, BasicProjectile projectile)
    {
        base.Hit(collider, transform, hitPoint, projectile);

        if (currentHitPoints == 0)
        {
            var clone = Instantiate(effects[0], hitPoint.point, Quaternion.identity);
            clone.transform.forward = projectile.transform.forward;// hitPoint.point.normalized;

           // clone = Instantiate(effects[1], hitPoint.point, Quaternion.identity);
           // clone.transform.forward = projectile.transform.forward;///hitPoint.point.normalized;

            DestroyMe();
        }
    }
}
