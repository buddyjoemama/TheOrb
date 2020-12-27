using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEditor.UIElements;
using UnityEngine;

public class Hitable : MonoBehaviour, IHitable
{
    public int maxHitPoints;
    public int currentHitPoints;

    private bool hit = false;
    private bool reverse = false;
    public List<Transform> effects;
    public List<Pickup> droppedItems;

    public delegate void HitDelegate(int amount);
    public event HitDelegate OnHit;

    public System.Guid Id { get; set; }

    public Hitable()
    {
        Id = System.Guid.NewGuid();
        currentHitPoints = maxHitPoints;
    }

    // Start is called before the first frame update
    public virtual void Start()
    {
        currentHitPoints = maxHitPoints;
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (currentHitPoints == 0)
        {
            if (effects != null && effects.Count > 0)
            {
                Transform randomEffect = effects[Random.Range(0, effects.Count)];
                Transform shatterEffect = Instantiate(randomEffect, transform.position, Quaternion.identity);

                Destroy(shatterEffect.gameObject, 5f);
            }

            DestroyMe();
        }
    }

    protected virtual void DestroyMe()
    {
        try
        {
            int dropIndex = Random.Range(0, droppedItems.Count);
            var item = droppedItems[dropIndex];

            if (item != null)
            {
                Instantiate(droppedItems[dropIndex], transform.position, Quaternion.identity);
            }
        }
        catch { }

        Destroy(gameObject);
    }

    public void FixedUpdate()
    {
        Material material = GetComponent<Renderer>().material;

        if (hit && material.HasProperty("HitColor"))
        {
            Color currentColor = material.GetColor("HitColor");

            if (material.GetColor("HitColor").r <= .4f && !reverse)
            {
                float amount = currentColor.r + (3.8f * Time.deltaTime);

                currentColor.r = amount;
                material.SetColor("HitColor", currentColor);
            }
            else
            {
                reverse = true;
            }

            if (reverse)
            {
                if (material.GetColor("HitColor").r >= 0f)
                {
                    float amount = material.GetColor("HitColor").r - (3.8f * Time.deltaTime);

                    currentColor.r = amount;
                    material.SetColor("HitColor", currentColor);
                    hit = material.GetColor("HitColor").r > 0f;
                }
            }
        }
    }

    public virtual void Hit(Transform collider, Transform transform, RaycastHit hitPoint, BasicProjectile projectile)
    {
        hit = true;
        reverse = false;
        currentHitPoints -= 1;
    }
}
