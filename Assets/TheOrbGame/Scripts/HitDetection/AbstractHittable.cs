﻿using System;
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

    public int HitPoints = 0;

    protected IHitEffect _hitEffect;

    protected IHitAction _hitAction;

    protected IEnumerable<IDestroyAction> _destroyActions;

    public virtual void Start()
    {
        _hitEffect = this.GetComponent<IHitEffect>();
        _hitAction = this.GetComponent<IHitAction>();
        _destroyActions = this.GetComponents<IDestroyAction>().AsEnumerable();
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
    public virtual void Hit(Transform collider, RaycastHit hit, BasicProjectile projectile)
    {
        HitPoints -= 1;

        if(HitPoints > 0)
        {
            _hitEffect?.Apply();
            _hitAction?.Apply(collider, hit, projectile);
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
        foreach(var action in _destroyActions)
        {
            action.Apply();
        }

        OnDestroyed();
    }

    protected virtual void OnDestroyed()
    {
        Destroy(gameObject);
    }
}