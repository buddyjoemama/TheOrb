using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public abstract class HittableBase : MonoBehaviour, IHittable
{
    private Guid id = Guid.NewGuid();

    public virtual Guid Id => id;

    public String Tag => this.tag;

    public virtual bool ShouldDestroy => false;

    public virtual Quaternion EffectOrientation => Quaternion.identity;

    public int HitPoints = 0;

    /// <summary>
    /// Effect used when hittable is destoyed.
    /// </summary>
    public Transform explosionEffect;

    /// <summary>
    /// Anything that can fire a projectile is itself hittable.
    /// </summary>
    /// <param name="collider"></param>
    /// <param name="transform"></param>
    /// <param name="hit"></param>
    /// <param name="projectile"></param>
    /// <param name="firedFrom"></param>
    /// <returns></returns>
    public virtual void Hit(Transform collider, Transform transform, RaycastHit hit, BasicProjectile projectile)
    {
        HitPoints -= 1;

        if(HitPoints == 0)
        {
            DestroyHittable();
        }
    }

    /// <summary>
    /// What was hit (IHittable)? Cant hit yourself...
    /// </summary>
    /// <param name="hit"></param>
    /// <returns></returns>
    public virtual bool IsValidHit(RaycastHit hit, IHittable firedFrom)
    {
        return hit.collider.GetComponent<IHittable>() != null &&
            hit.collider.GetComponent<IHittable>().Id != firedFrom.Id;
    }

    /// <summary>
    /// Called when hitpoints reaches 0.
    /// </summary>
    public virtual void DestroyHittable()
    {
        if(explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, EffectOrientation);
        }
    }
}