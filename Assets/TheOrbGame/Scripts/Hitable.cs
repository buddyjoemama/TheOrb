﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEditor.UIElements;
using UnityEngine;

public class Hitable : MonoBehaviour, IHitable
{
    public int maxHitPoints;
    public int currentHitPoints;

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

    // Update is called once per frame
    void Update()
    {
        if (currentHitPoints == 0)
        {
            if (replace != null)
            {
                var position = new Vector3(transform.position.x, 5, transform.position.z);
                Instantiate(replace, position, Quaternion.Euler(-90, 0, 0));
            }

            Destroy(gameObject);
        }
        else
        {
            if (hit)
            {
                Color currentColor = material.GetColor("EmissionColor");

                if (material.GetColor("EmissionColor").r <= .3f && !reverse)
                {
                    float amount = currentColor.r + (3.8f * Time.deltaTime);

                    currentColor.r = amount;
                    material.SetColor("EmissionColor", currentColor);
                }
                else
                {
                    reverse = true;
                }

                if (reverse)
                {
                    if (material.GetColor("EmissionColor").r >= 0f)
                    {
                        float amount = material.GetColor("EmissionColor").r - (3.8f * Time.deltaTime);

                        currentColor.r = amount;
                        material.SetColor("EmissionColor", currentColor);
                        hit = material.GetColor("EmissionColor").r > 0f;
                    }
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