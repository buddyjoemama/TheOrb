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

        Rigidbody rb = GetComponent<Rigidbody>();
        if(rb != null)
        {
            rb.AddForceAtPosition(new Vector3(4, 0, 4), hitPoint.point, ForceMode.Impulse);
        }
    }
}
