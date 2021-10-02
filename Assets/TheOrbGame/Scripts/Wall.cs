using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : HittableBase
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    internal void UpdatePlayerPosition(Vector3 point, float distance)
    {
        //float chunk = (.02f * (distance)) - .2f;

        //GetComponent<Renderer>().sharedMaterial.SetVector("Position", new Vector4(point.x, 0, 0, 0));
        //GetComponent<Renderer>().sharedMaterial.SetFloat("Distance", Mathf.Clamp(chunk, 0, .2f));
    }

    public override void Hit(Transform collider, Transform transform, RaycastHit hit, BasicProjectile projectile)
    {
        //  rClone.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
        if (projectileHitEffect != null)
        {
            var clone = Instantiate(projectileHitEffect, hit.point, Quaternion.FromToRotation(Vector3.forward, hit.normal));
        }
    }

    public override bool IsValidHit(RaycastHit hit, IHittable firedFrom)
    {
        return true;
    }
}
