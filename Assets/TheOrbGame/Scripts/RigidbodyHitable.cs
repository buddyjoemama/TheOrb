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
            int x = -4;
            int z = -4;

            var dirX = projectile.transform.position.x - rb.position.x;
            var dirZ = projectile.transform.position.z - rb.position.z;

            if(dirX <= 0)
            {
                x = 4;
            }
            if(dirZ <= 0)
            {
                z = 4;
            }

            if(projectile.transform.position.x > rb.position.x)
                rb.AddForceAtPosition(new Vector3(x, 0, z), hitPoint.point, ForceMode.Impulse);
        }
    }
}
