using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float speed = 10;
    private Rigidbody rb;
    private float movementX;
    private float movementY;

    public BasicProjectile projectile;
    public Shield shield;
    public Transform platform;

    private bool saving = false;
    private Vector3 savePosition;
    public bool shieldEnabled = false;
    public Transform gunRig;
    public Transform firePoint;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        shield.Deactivate();
    }

    void OnFire() 
    {
        //Projectile clone = (Projectile)Instantiate(projectile, transform.position, transform.rotation);
        //clone.Fire();

        //var text = Instantiate(projectile, transform.position, transform.rotation);

        // Destroy(text.gameObject, 1.0f);

        // Instantiate(platform, transform.position, Quaternion.identity);

        Debug.Log("Front: " + firePoint.forward);
        Debug.Log("Right: " + firePoint.right);

        //Projectile clone = Instantiate(projectile, firePoint.transform.position, firePoint.rotation * Quaternion.Euler(90, 0, 0));
        //clone.Fire(firePoint.forward);

       // var c = projectile.transform.GetChild(0).GetChild(2).position;

      //  var p = firePoint.transform.position;

        BasicProjectile clone = Instantiate(projectile, firePoint.transform.position, firePoint.rotation);
        clone.Fire(firePoint.forward);
    }


    internal void MoveToSaveCenter(Vector3 position)
    {
        saving = true;

        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.rotation = Quaternion.identity;

        savePosition = position;
    }

    private void OnMove(InputValue move)
    {
        Vector2 movementVector = move.Get<Vector2>();
        this.movementX = movementVector.x;
        this.movementY = movementVector.y;
    }

    private void OnLook(InputValue look)
    {
        Vector2 lookVector = look.Get<Vector2>();

        //var r = gunRig.rotation.eulerAngles + new Vector3(0, lookVector.y);

        //gunRig.rotation = Quaternion.Euler(r);

        gunRig.transform.Rotate(new Vector3(0, lookVector.y * 4), Space.Self);
    }

    private void FixedUpdate()
    {
        Vector3 movementForce = new Vector3(this.movementX, 0.0f, this.movementY) * speed;

        // Make it relative to the camera.
        Vector3 transformVector = Camera.main.transform.TransformVector(movementForce);

        if (!saving)
        {
            rb.AddForce(new Vector3(transformVector.x, 0.0f, transformVector.z));
        }
        else
        {
            rb.position = Vector3.Lerp(rb.position, savePosition, .1f);
        }
    }

    private void Update()
    {
        shield.Move(transform.position);
        gunRig.position = transform.position;

        if(shieldEnabled)
        {
            shield.Activate();
        }
        else
        {
            shield.Deactivate();
        }
    }
}
