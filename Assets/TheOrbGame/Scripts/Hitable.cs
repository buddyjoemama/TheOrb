using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEditor.UIElements;
using UnityEngine;

public class Hitable : MonoBehaviour, IHitable
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

    public System.Guid Id { get; set; }

    public virtual bool ShouldDestroy => true;

    public Hitable()
    {
        Id = System.Guid.NewGuid();
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

    // Update is called once per frame
    public virtual void Update()
    {

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
            Transform shatterEffect = Instantiate(randomEffect, transform.position, transform.rotation);

            Destroy(shatterEffect.gameObject, 5f);
        }

        if (OnDestroyed != null)
        {
            OnDestroyed();
        }

        if(ShouldDestroy)
            Destroy(gameObject);
    }

    public virtual bool Hit(Transform collider, Transform transform, RaycastHit hitPoint, BasicProjectile projectile)
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

        // Delegate the actual effect
        if(this.hitColorVarName != null)
        {
            if(this.GetComponentInChildren<SimpleHitEffect>() != null)
            {
                var hitEffect = this.GetComponentInChildren<SimpleHitEffect>();
                hitEffect.Apply(this.hitColorVarName, this.hitColor);
            }
        }

        return true;
    }
}
