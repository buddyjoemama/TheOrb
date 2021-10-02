using UnityEngine;

public class ShieldContainer : HittableBase
{
    public Shield shield;

    public override Quaternion EffectOrientation => Quaternion.Euler(-90, 0, 0);

 
    public override bool IsValidHit(RaycastHit hit, IHittable firedFrom)
    {
        return base.IsValidHit(hit, firedFrom) && firedFrom.Tag != "Player";
    }

    public override void Hit(Transform collider, Transform transform, RaycastHit hit, BasicProjectile projectile)
    {
        base.Hit(collider, transform, hit, projectile);

        if (HitPoints > 0)
        {
            Shield clone = Instantiate(shield, this.transform.position, Quaternion.identity);
            clone.Apply(hit.point, this);
        }
    }
}