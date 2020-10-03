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

    public Transform projectile;
    public Shield shield;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        shield.Deactivate();
    }

    void OnFire() 
    {
        // Projectile clone = (Projectile)Instantiate(projectile, transform.position, transform.rotation);
        //clone.Fire();

        var text = Instantiate(projectile, transform.position, transform.rotation);

        Destroy(text.gameObject, 1.0f);
    }

    bool shouldSave = false;
    Transform saveLocation;

    internal void MoveToSaveCenter(Transform transform)
    {
        saveLocation = transform;
        shouldSave = true;

        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.rotation = Quaternion.identity;
    }

    private void OnMove(InputValue move)
    {
        Vector2 movementVector = move.Get<Vector2>();
        this.movementX = movementVector.x;
        this.movementY = movementVector.y;
    }

    private void FixedUpdate()
    {
        Vector3 movementForce = new Vector3(this.movementX, 0.0f, this.movementY) * speed;

        // Make it relative to the camera.
        Vector3 transformVector = Camera.main.transform.TransformVector(movementForce);

        if (!shouldSave)
        {
            rb.AddForce(new Vector3(transformVector.x, 0.0f, transformVector.z));
        }
        else
        {
            rb.useGravity = false;
              rb.position = Vector3.Lerp(rb.position, saveLocation.position, .1f);
            var v = saveLocation.position - rb.position;

            //rb.AddForce(v, ForceMode.Impulse);
            shouldSave = rb.position.z != saveLocation.position.z && rb.position.x != saveLocation.position.x;
        }
    }

    private void Update()
    {
        //if (shield != null)
       //     shield.position = transform.position;
    }
}
