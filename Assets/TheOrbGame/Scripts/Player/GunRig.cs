using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GunRig : MonoBehaviour
{
    public Transform firePoint;
    public Player target;
    public BasicProjectile projectile;
    public Reticle reticle;
    private Reticle rClone;

    private void Start()
    {
        rClone = Instantiate(reticle);
        Cursor.visible = false;
    }

    public virtual void OnFire()
    {
        BasicProjectile clone = Instantiate(projectile, firePoint.transform.position, firePoint.rotation);
        clone.Fire(firePoint.forward, this.gameObject.GetComponentInParent<Hitable>());
    }

    protected virtual void Update() { }

    protected virtual void FixedUpdate()
    {
        var mousePos = InputSystem.GetDevice<Mouse>().position;

        Ray ray = Camera.main.ScreenPointToRay(new Vector3(mousePos.x.ReadValue(), mousePos.y.ReadValue()));
        Vector3 lookAt = Vector3.zero;

        if (Physics.Raycast(ray, out RaycastHit hit, 1000))
        {
            lookAt = hit.point;

            if (rClone != null)
            {
                rClone.transform.LookAt(target.transform.position);// //Quaternion.LookRotation(ray.direction, Vector3.up);// ray.direction;// Quaternion.FromToRotation(Vector3.up, ray.direction);
                rClone.transform.Rotate(90, 0, 0, Space.Self);

                rClone.transform.position = lookAt + rClone.transform.up + new Vector3(0, 10, 0);
            }
        }
        else
        {
            lookAt = ray.GetPoint(1000);
        }

        lookAt.y = 10;
        transform.LookAt(lookAt);
    }
}