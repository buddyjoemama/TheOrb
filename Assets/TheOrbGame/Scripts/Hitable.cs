using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEditor.UIElements;
using UnityEngine;

public class Hitable : MonoBehaviour, IHitable
{
    public int maxHitPoints;
    public int currentHitPoints;
    public DissolvableCube dissolvable;
    public Transform replace;
    private Material material;
    private bool hit = false;
    private bool reverse = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        material = GetComponent<Renderer>().material;
        currentHitPoints = maxHitPoints;
    }

    bool destroyed = false;
    // Update is called once per frame
    void Update()
    {
        if (currentHitPoints == 0)
        {
            if (replace != null && !destroyed)
            {
                destroyed = true;
                var position = new Vector3(transform.position.x, 5, transform.position.z);
                Instantiate(replace, position, Quaternion.Euler(-90, 0, 0));

                if (dissolvable != null)
                {
                    Instantiate(dissolvable, position, transform.rotation);
                    
                }
            }

            Destroy(gameObject);
        }
    }

    public void FixedUpdate()
    {
        if (hit)
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

    Transform hitBy;
    public void Hit(Transform collider, Transform transform)
    {
        hit = true;
        reverse = false;
        currentHitPoints -= 1;
        hitBy = transform;
    }
}
