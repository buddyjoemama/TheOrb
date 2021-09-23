using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public float beginFadeAmount = .01f;
    public int LocalScale = 30;
    private ShieldContainer _parentContainer;

    internal void Apply(Vector3 hitPoint, ShieldContainer parentContainer)
    {
        _parentContainer = parentContainer;
        transform.localScale = new Vector3(LocalScale, LocalScale, LocalScale);
        transform.LookAt(hitPoint);
        StartCoroutine(FadeOut());
    }

    private void FixedUpdate()
    {
        if (_parentContainer != null)
        {
            this.transform.position = _parentContainer.transform.position;
            this.transform.localScale += new Vector3(.1f, .1f, .1f);
        }
    }

    IEnumerator FadeOut()
    {
        float fadeAmount = beginFadeAmount;

        do
        {
            GetComponent<Renderer>().material.SetFloat("FadeAmt", fadeAmount);
            fadeAmount += Time.deltaTime;

            yield return new WaitForFixedUpdate();
        }
        while (fadeAmount <= 1 && _parentContainer.HitPoints > 0);

        Destroy(this.gameObject);
    }
}
