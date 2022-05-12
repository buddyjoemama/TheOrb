using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public abstract class AbstractHittable : MonoBehaviour, IHittable
{
    private Guid id = Guid.NewGuid();

    public virtual Guid Id => id;

    public String Tag => this.tag;

    public virtual bool ShouldDestroy => false;

    public virtual Quaternion EffectOrientation => Quaternion.identity;

    public virtual Vector3 EffectPosition => transform.position;

    public int HitPoints = 0;

    /// <summary>
    /// Effect used when hittable is destoyed.
    /// </summary>
    public Transform explosionEffect;

    /// <summary>
    /// The effect used at the hit location
    /// </summary>
    public Transform projectileHitEffect;

    protected IHitEffect _hitEffect;

    protected IHitAction _hitAction;

    protected IDestroyAction _destroyAction;

    public virtual void Start()
    {
        _hitEffect = this.GetComponent<IHitEffect>();
        _hitAction = this.GetComponent<IHitAction>();
        _destroyAction = this.GetComponent<IDestroyAction>();
    }

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

        if(HitPoints > 0)
        {
            _hitEffect?.Apply();
            _hitAction?.Apply(collider, transform, hit, projectile);
        }
        else
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
            hit.collider.GetComponent<IHittable>().Id != firedFrom.Id &&
            this.gameObject.activeSelf;
    }

    /// <summary>
    /// Called when hitpoints reaches 0.
    /// </summary>
    public virtual void DestroyHittable()
    {
        if(explosionEffect != null)
        {
            var explosion = Instantiate(explosionEffect, EffectPosition, EffectOrientation);
            DestroyExplosion(explosion);
        }
        
        _destroyAction?.Apply();

        this.gameObject.SetActive(false);
    }

    protected virtual void DestroyExplosion(Transform effect)
    {
        //Destroy(effect, 10);
    }
}