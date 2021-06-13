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

   
    public override bool Hit(Transform collider, Transform transform, RaycastHit hitPoint, BasicProjectile projectile)
    {
        return false;
    }

    public override void Update()
    {
        
    }
}
