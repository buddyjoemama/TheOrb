using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public abstract class HittableBase : MonoBehaviour, IHittable
{
    private Guid id = Guid.NewGuid();

    public HittableBase()
    {
        
    }

    public virtual Guid Id => id;

    public virtual bool ShouldDestroy => false;

    public virtual Quaternion EffectOrientation => Quaternion.identity;

    /// <summary>
    /// Anything that can fire an projectile is itself hittable.
    /// </summary>
    /// <param name="collider"></param>
    /// <param name="transform"></param>
    /// <param name="hit"></param>
    /// <param name="projectile"></param>
    /// <param name="firedFrom"></param>
    /// <returns></returns>
    public virtual bool Hit(Transform collider, Transform transform, RaycastHit hit, BasicProjectile projectile)
    {
        return true;
    }

    /// <summary>
    /// What was hit (IHittable)? 
    /// </summary>
    /// <param name="hit"></param>
    /// <returns></returns>
    public virtual bool IsValidHit(RaycastHit hit, IHittable firedFrom)
    {
        return true;
    }
}