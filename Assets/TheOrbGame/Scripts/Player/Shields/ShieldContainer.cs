using UnityEngine;

public class ShieldContainer : AbstractHittable
{
    public Shield shield;
    public float Scale;
    private Shield clone;
 
    public override bool IsValidHit(RaycastHit hit, IHittable firedFrom)
    {
        return base.IsValidHit(hit, firedFrom) && firedFrom.Tag != "Player";
    }

    public override void Hit(Transform collider, Transform transform, RaycastHit hit, BasicProjectile projectile)
    {
        base.Hit(collider, transform, hit, projectile);

        if (HitPoints > 0)
        {
            var rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
            clone = Instantiate(shield, hit.point, rotation);
            clone.Apply(this);
        }   
    }
}