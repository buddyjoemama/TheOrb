using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEditor.UIElements;
using UnityEngine;

// eventually this needs to be promoted to the base class
public class Hittable : AbstractHittable
{
    public int maxHitPoints;
    public int currentHitPoints;
    public List<Transform> effects;
    public List<Pickup> droppedItems;
    public bool dropsItems = true;
    public string hitColorVarName = null;
    public Color hitColor;

    public delegate void HitDelegate(int damage);
    public delegate void DestroyedDelegate();
    public delegate void HitPointsChangedDelegate();

    public event HitDelegate OnHit;
    public event DestroyedDelegate OnDestroyed;
    public event HitPointsChangedDelegate OnHitPointsChanged;

    public Hittable()
    {
        
    }

    internal void AddHitPoint(int lifeValue)
    {
        currentHitPoints += lifeValue;
        
        if(OnHitPointsChanged != null)
        {
            OnHitPointsChanged();
        }
    }

    // Start is called before the first frame update
    public virtual void Start()
    {
        currentHitPoints = maxHitPoints;
    }

    protected virtual void DestroyMe()
    {
        // Does this hittable drop items?
        if (dropsItems && droppedItems.Count >= 1)
        {
            int dropIndex = Random.Range(0, droppedItems.Count);
            var item = droppedItems[dropIndex];

            if (item != null && dropsItems)
            {
                Instantiate(droppedItems[dropIndex], transform.position, Quaternion.identity);
            }
        }

        // Does this have a shatter effect? 
        if (effects != null && effects.Count > 0)
        {
            Transform randomEffect = effects[Random.Range(0, effects.Count)];
            Transform shatterEffect = Instantiate(randomEffect, transform.position, EffectOrientation);

            Destroy(shatterEffect.gameObject, 5f);
        }

        if (OnDestroyed != null)
        {
            OnDestroyed();
        }

        if(ShouldDestroy)
            Destroy(gameObject);
    }

    public override void Hit(Transform collider, Transform transform, RaycastHit hitPoint, BasicProjectile projectile)
    {
        currentHitPoints -= projectile.damage;

        if(OnHit != null)
        {
            OnHit(projectile.damage);
        }

        if(currentHitPoints <= 0)
        {
            DestroyMe();
        }

        //// Delegate the actual effect
        //if(this.hitColorVarName != null)
        //{
        //    if(this.GetComponentInChildren<SimpleHitEffect>() != null)
        //    {
        //        var hitEffect = this.GetComponentInChildren<SimpleHitEffect>();
        //        hitEffect.Apply(this.hitColorVarName, this.hitColor);
        //    }
        //}
    }
}
