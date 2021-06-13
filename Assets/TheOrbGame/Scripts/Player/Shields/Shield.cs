using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : Hitable
{
    // Start is called before the first frame update
    private Coroutine hitEffect;

    internal void Deactivate()
    {
        gameObject.SetActive(false);
    }

    internal void Activate()
    {
        gameObject.SetActive(true);
    }

    internal void Move(Vector3 position)
    {
        transform.position = position;
    }

    protected override void DestroyMe()
    {
        this.Deactivate();
    }

    public override bool Hit(Transform collider, Transform transform, RaycastHit hitPoint, BasicProjectile projectile)
    {
        if (hitEffect != null)
        {
            StopCoroutine(hitEffect);

            Color alpha = new Color(0, 0, 0, 0);
            GetComponent<Renderer>().material.SetColor("_BaseColor", alpha);
        }

        // Start the fade effect
        hitEffect = StartCoroutine(UpdateOpacity());

        return base.Hit(collider, transform, hitPoint, projectile);
    }

    IEnumerator UpdateOpacity()
    {
        Color red = new Color(1, 0, 0, .5f);
        GetComponent<Renderer>().material.SetColor("_BaseColor", red);

        yield return null;

        Color currentColor = GetComponent<Renderer>().material.GetColor("_BaseColor");
        while (currentColor.a >= 0)
        {
            currentColor.a = currentColor.a - (Time.deltaTime );
            GetComponent<Renderer>().material.SetColor("_BaseColor", currentColor);
            yield return null;
        }
    }
}
