using UnityEngine;

public class ShieldContainer : AbstractHittable
{
    public Transform ExplosionEffect;
    public Shield Shield;
    private Shield _clone;

    public override void Start()
    {
        base.Start();
        this.gameObject.SetActive(true);
    }

    public override bool IsValidHit(RaycastHit hit, IHittable firedFrom)
    {
        return base.IsValidHit(hit, firedFrom) && firedFrom.Tag != "Player";
    }

    public override void Hit(Transform collider, RaycastHit hit, BasicProjectile projectile)
    {
        base.Hit(collider, hit, projectile);
        var rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);

        if (HitPoints > 0)
        {
            _clone = Instantiate(Shield, hit.point, rotation);
            _clone.Apply(this);    
        }
        else
        {
            var clone = Instantiate(ExplosionEffect, hit.point, rotation);
            Destroy(clone.gameObject, 10f);
            this.gameObject.SetActive(false);
        }
    }

    public override void DestroyHittable()
    {
        this.gameObject.SetActive(false);
    }
}