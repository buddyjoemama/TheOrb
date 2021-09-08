using UnityEngine;

public class ShieldContainer : HittableBase
{
    public ShieldContainer()
    {

    }

    public override bool Hit(Transform collider, Transform transform, RaycastHit hit, BasicProjectile projectile)
    {
        Debug.Log("Hit");

        return true;// base.Hit(collider, transform, hit, projectile);
    }
}