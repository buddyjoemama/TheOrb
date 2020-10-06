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
    public Transform platform;

    private bool saving = false;
    private Vector3 savePosition;

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

        //var text = Instantiate(projectile, transform.position, transform.rotation);

        // Destroy(text.gameObject, 1.0f);

       // Instantiate(platform, transform.position, Quaternion.identity);
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
        //if (shield != null)
       //     shield.position = transform.position;
    }
}
