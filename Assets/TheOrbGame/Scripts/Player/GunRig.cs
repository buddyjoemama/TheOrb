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
            rClone.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
            rClone.transform.position = lookAt + rClone.transform.up;
        }
        else
        {
            lookAt = ray.GetPoint(1000);
        }

        transform.LookAt(lookAt);
    }

    internal void Rotate(Vector2 vector2)
    {
        Quaternion rotation = transform.rotation;
        var currentX = rotation.eulerAngles.x;
        currentX += vector2.x;

        var currentY = rotation.eulerAngles.y;
        currentY += vector2.y;

        //   transform.rotation = Quaternion.Euler(currentX, 0, 0);
    }
}