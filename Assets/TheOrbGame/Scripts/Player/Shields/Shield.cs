using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : Hitable
{
    // Start is called before the first frame update
    private Coroutine hitEffect;

    public override bool ShouldDestroy => false;
    public override Quaternion EffectOrientation => Quaternion.Euler(-90, 0, 0);

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
        base.DestroyMe();
        this.Deactivate();
    }

    public override bool Hit(Transform collider, Transform transform, RaycastHit hitPoint, BasicProjectile projectile)
    {
        //if (hitEffect != null)
        //{
        //    StopCoroutine(hitEffect);

        //    Color alpha = new Color(0, 0, 0, 0);
        //    GetComponent<Renderer>().material.SetColor("_BaseColor", alpha);
        //}

        //// Start the fade effect
        //hitEffect = StartCoroutine(UpdateOpacity());

        var p = hitPoint.collider.transform.InverseTransformPoint(hitPoint.point);

        GetComponent<Renderer>().material.SetVector("HitLocation", p);

        StartCoroutine(UpdateFade());

        Debug.Log(p);
        return base.Hit(collider, transform, hitPoint, projectile);
    }

    IEnumerator UpdateFade()
    {
        float amount = .3f;

        while (amount > 0)
        {
            GetComponent<Renderer>().material.SetFloat("FadeAmt", amount);
            amount -= (Time.deltaTime / 1.2f);

            yield return new WaitForFixedUpdate();
        }
    }

    IEnumerator UpdateOpacity()
    {
        Color red = new Color(1, 0, 0, .75f);
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
