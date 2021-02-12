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

    public delegate void HitDelegate(int damage);
    public delegate void DestroyedDelegate();
    public delegate void HitPointsChangedDelegate();

    public event HitDelegate OnHit;
    public event DestroyedDelegate OnDestroyed;
    public event HitPointsChangedDelegate OnHitPointsChanged;

    public System.Guid Id { get; set; }

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
        if (currentHitPoints <= 0)
        {
            if (effects != null && effects.Count > 0)
            {
                Transform randomEffect = effects[Random.Range(0, effects.Count)];
                Transform shatterEffect = Instantiate(randomEffect, transform.position, transform.rotation);

                Destroy(shatterEffect.gameObject, 5f);
            }

            DestroyMe();
        }
    }

    protected virtual void DestroyMe()
    {
        try
        {
            int dropIndex = Random.Range(0, droppedItems.Count - 1);
            var item = droppedItems[dropIndex];

            if (item != null && dropsItems)
            {
                Instantiate(droppedItems[dropIndex], transform.position, Quaternion.identity);
            }
        }
        catch { }

        if(OnDestroyed != null)
        {
            OnDestroyed();
        }

        Destroy(gameObject);
    }

    public virtual void Hit(Transform collider, Transform transform, RaycastHit hitPoint, BasicProjectile projectile)
    {
        currentHitPoints -= projectile.damage;

        if(OnHit != null)
        {
            OnHit(projectile.damage);
        }
    }
}
