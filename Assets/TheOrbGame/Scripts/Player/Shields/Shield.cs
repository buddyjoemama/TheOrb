using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    private ShieldContainer _parentContainer;
    private Vector3 _difference = new Vector3();

    public void Start()
    {
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        float fadeAmount = 1f;

        Light light = GetComponentInChildren<Light>();

        do
        {
            GetComponent<Renderer>().material.SetFloat("FadeAmount", fadeAmount);
            fadeAmount -= (Time.deltaTime);
            light.intensity -= (Time.deltaTime * 80);

            yield return new WaitForFixedUpdate();
        }
        while (fadeAmount >= 0);

        Destroy(this.gameObject);
    }

    public void Update()
    {
        if(_parentContainer != null)
        {
            this.transform.position = (_parentContainer.transform.position - _difference);
            this.transform.localScale += new Vector3(.005f, .005f, .005f);
        }
    }

    internal void Apply(ShieldContainer shieldContainer)
    {
        _difference = shieldContainer.transform.position - this.transform.position;
        _parentContainer = shieldContainer;
    }
}
