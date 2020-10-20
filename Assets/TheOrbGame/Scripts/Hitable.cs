using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class Hitable : MonoBehaviour, IHitable
{
    public int hitPoints = 0;

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
    }

    // Update is called once per frame
    void Update()
    {
        if(hit)
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

            if(reverse)
            {
                if(material.GetColor("EmissionColor").r >= 0f)
                {
                    float amount = material.GetColor("EmissionColor").r - (3.8f * Time.deltaTime);

                    currentColor.r = amount;
                    material.SetColor("EmissionColor", currentColor);
                    hit = material.GetColor("EmissionColor").r > 0f;
                }                    
            }
        }
    }

    public void Hit(Transform collider)
    {
        hit = true;
        reverse = false;
    }
}
