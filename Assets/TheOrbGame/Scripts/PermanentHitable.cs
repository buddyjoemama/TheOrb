using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PermanentHitable : Hitable
{
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

   
    public override void Hit(Transform collider, Transform transform, RaycastHit hitPoint, BasicProjectile projectile)
    {
        //var clone = Instantiate(effects[0], hitPoint.point, Quaternion.identity);
        //clone.transform.forward = hitPoint.point.normalized;

        base.Hit(collider, transform, hitPoint, projectile);
    }

    public override void Update()
    {
        
    }
}
