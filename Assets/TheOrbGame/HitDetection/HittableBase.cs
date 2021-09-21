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
}