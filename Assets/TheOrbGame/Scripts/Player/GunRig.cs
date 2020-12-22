using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GunRig : MonoBehaviour
{
    public Transform firePoint;
    public Transform target;
    public BasicProjectile projectile;

    void OnFire()
    {
        BasicProjectile clone = Instantiate(projectile, firePoint.transform.position, firePoint.rotation);
        clone.Fire(firePoint.forward, this.gameObject.GetComponentInParent<Hitable>());
    }

    private void Update()
    {
        if (target == null)
        {
            var mousePos = InputSystem.GetDevice<Mouse>().position;

            Ray ray = Camera.main.ScreenPointToRay(new Vector3(mousePos.x.ReadValue(), mousePos.y.ReadValue()));
            Vector3 lookAt = Vector3.zero;

            if (Physics.Raycast(ray, out RaycastHit hit, 1000))
            {
                lookAt = hit.point;
            }
            else
            {
                lookAt = ray.GetPoint(1000);
            }

            transform.LookAt(lookAt);
        }
        else
        {
            transform.LookAt(target);
        }

        //playerManager.rig.transform.LookAt(lookAt);
    }
}