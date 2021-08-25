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

            // Fire the ray from the muzzle.
            if (Physics.Raycast(this.firePoint.position, this.transform.forward, out RaycastHit hitInfo) && hitInfo.collider.tag == "Box")
            {
                rClone.transform.position = hitInfo.point;
                rClone.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
            }
            else
            {
                rClone.transform.LookAt(target.transform.position);
                rClone.transform.position = lookAt + rClone.transform.up;
            }
            //if (rClone != null)
            //{
            //    //rClone.transform.LookAt(target.transform.position);// //Quaternion.LookRotation(ray.direction, Vector3.up);// ray.direction;// Quaternion.FromToRotation(Vector3.up, ray.direction);
            //    //rClone.transform.Rotate(90, 0, 0, Space.Self);

            //    // rClone.transform.position = lookAt + rClone.transform.up + new Vector3(0, 10, 0);
            //    rClone.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
            //    rClone.transform.position = lookAt + rClone.transform.up;
            //}
        }
        //else
        //{
        //    lookAt = ray.GetPoint(1000);
        //}

        //Debug.DrawLine(firePoint.position, lookAt);
        //Debug.DrawRay(ray.origin, ray.direction);
        //Debug.DrawLine(ray.origin, hit.point);

        lookAt.y = 10;
        transform.LookAt(lookAt);

        //if(Physics.Raycast(this.firePoint.position, this.transform.forward, out RaycastHit hitInfo) && hitInfo.collider.tag == "Box")
        //{
        ////    rClone.transform.position = hitInfo.point;
        //}
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