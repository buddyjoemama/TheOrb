﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashIn : MonoBehaviour
{
    private Material material;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(UpdateColor());
    }

    private void Awake()
    {
        material = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator UpdateColor()
    {
        Color currentColor = new Color(4, 4, 4);

        while (currentColor.r >= 0.0f)
        {
            currentColor.r = currentColor.g = currentColor.b = (currentColor.r -= .1f);
            material.SetColor("Color", currentColor);

            yield return new WaitForSeconds(0.005f);
        }
    }
}
