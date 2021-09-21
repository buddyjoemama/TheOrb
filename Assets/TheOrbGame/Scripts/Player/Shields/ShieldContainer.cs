using UnityEngine;

public class ShieldContainer : HittableBase
{
    public Shield shield;
    public float shieldScale = 1.5f;

    void Start()
    {
        
    }

    public override bool IsValidHit(RaycastHit hit, IHittable firedFrom)
    {
        return base.IsValidHit(hit, firedFrom) && firedFrom.Tag != "Player";
    }

    public override void Hit(Transform collider, Transform transform, RaycastHit hit, BasicProjectile projectile)
    {
        var clone = Instantiate(shield, this.transform.position, Quaternion.identity);
        clone.Apply(hit.point, this.transform);
    }
}