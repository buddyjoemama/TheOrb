using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[ExecuteInEditMode]
public class Player : HittableBase
{
    private Rigidbody rigidBody;
    private float movementX;
    private float movementY;
    private Camera mainCamera;
    private Vector3 cameraOffset;
    // Start is called before the first frame update
    private GunRig rig;
    private Shield shield;

    public bool shieldActivated = false;

    public Transform shieldPlaceholder;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
        cameraOffset = Camera.main.transform.position - transform.position;
        rig = GetComponentInChildren<GunRig>();
        //shield = GetComponentInChildren<Shield>();

    }

    internal void AddHealth(int lifeValue)
    {
        
    }

    /// <summary>
    /// Move the player ball.
    /// </summary>
    /// <param name="move"></param>
    private void OnMove(InputValue move)
    {
        Vector2 movementVector = move.Get<Vector2>();
        this.movementX = movementVector.x;
        this.movementY = movementVector.y;
    }

    void OnLook(InputValue value)
    {
        rig.Rotate(value.Get<Vector2>());
    }

    private void OnFire()
    {
        rig.OnFire();
    }
    public void Update()
    {
        if (shield != null)
        {
           // if (shieldActivated)
           //     shield.Activate();
           // else
           //     shield.Deactivate();
        }

        if(Physics.Raycast(rigidBody.transform.position, new Vector3(0, 0, 1), out RaycastHit hitFront))
        {
            var localPoint = hitFront.collider.transform.InverseTransformPoint(hitFront.point);

            if (hitFront.collider.gameObject.GetComponent<Wall>() != null)
            {
                hitFront.collider.gameObject.GetComponent<Wall>().UpdatePlayerPosition(localPoint, hitFront.distance);
            }
        }

        if (Physics.Raycast(rigidBody.transform.position, new Vector3(1, 0, 0), out RaycastHit hit))
        {
            var localPoint = hit.collider.transform.InverseTransformPoint(hit.point);

            if (hit.collider.gameObject.GetComponent<Wall>() != null)
            {
                hit.collider.gameObject.GetComponent<Wall>().UpdatePlayerPosition(localPoint, hit.distance);
            }
        }

        if (Physics.Raycast(rigidBody.transform.position, new Vector3(-1, 0, 0), out RaycastHit leftHit))
        {
            var localPoint = leftHit.collider.transform.InverseTransformPoint(leftHit.point);
            if (leftHit.collider.gameObject.GetComponent<Wall>() != null)
            {
                leftHit.collider.gameObject.GetComponent<Wall>().UpdatePlayerPosition(localPoint, leftHit.distance);
            }
        }

        if (Physics.Raycast(rigidBody.transform.position, new Vector3(0, 0, -1), out RaycastHit backHit))
        {
            var localPoint = backHit.collider.transform.InverseTransformPoint(hit.point);
            if (backHit.collider.gameObject.GetComponent<Wall>() != null)
            {
                backHit.collider.gameObject.GetComponent<Wall>().UpdatePlayerPosition(localPoint, backHit.distance);
            }
        }
    }

    private void FixedUpdate()
    {
        Vector3 movementForce = new Vector3(this.movementX, 0.0f, this.movementY) * 25;

        // Make it relative to the camera.
        Vector3 transformVector = Camera.main.transform.TransformVector(movementForce);
        rigidBody.AddForce(new Vector3(transformVector.x, 0.0f, transformVector.z)); 
    }

    private void LateUpdate()
    {
        mainCamera.transform.position = transform.position + cameraOffset;
        //this.shield.transform.position = this.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        var trigger = other.GetComponent<ITrigger>();
        
        if(trigger != null)
            trigger.Apply(this);
    }

    public override bool Hit(Transform collider, Transform transform, RaycastHit hit, BasicProjectile projectile)
    {
        return true;
    }
}
