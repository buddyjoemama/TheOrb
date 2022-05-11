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
        var rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);

        if (HitPoints > 0)
        {
            clone = Instantiate(shield, hit.point, rotation);
            clone.Apply(this);    
        }
        else
        {
            var clone = Instantiate(explosionEffect, hit.point, rotation);
            Destroy(clone.gameObject, 10f);
            this.gameObject.SetActive(false);
        }
    }

    public override void DestroyHittable()
    {
        _destroyAction?.Apply();

        this.gameObject.SetActive(false);
    }
}