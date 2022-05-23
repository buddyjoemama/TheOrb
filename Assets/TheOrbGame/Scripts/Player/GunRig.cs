using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GunRig : MonoBehaviour
{
    public Transform firePoint;
    public BasicProjectile projectile;
    public Reticle reticle;
    private Reticle rClone;

    public virtual void Start()
    {
        rClone = Instantiate(reticle);
    }

    public virtual void OnFire()
    {
        BasicProjectile clone = Instantiate(projectile, firePoint.transform.position, firePoint.rotation);
        clone.Fire(firePoint.forward, this.gameObject.GetComponentInParent<IHittable>(), this.transform);
    }

    public virtual void Update()
    {
        var mousePos = InputSystem.GetDevice<Mouse>().position;

        Ray ray = Camera.main.ScreenPointToRay(new Vector3(mousePos.x.ReadValue(), mousePos.y.ReadValue()));

        if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue))
        {
            Vector3 lookAtPoint = hit.collider.GetComponent<ILookAt>()?.GetLookAtPoint(hit) ?? hit.point;

            rClone.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
            rClone.transform.position = lookAtPoint + (rClone.transform.up / 10);

            transform.LookAt(lookAtPoint);
        }
    }
}