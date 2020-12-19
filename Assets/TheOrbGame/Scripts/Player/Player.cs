using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Rigidbody rb;
    private float movementX;
    private float movementY;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Move(Vector2 movementVector)
    {
        this.movementX = movementVector.x;
        this.movementY = movementVector.y;
    }

    private void FixedUpdate()
    {
        Vector3 movementForce = new Vector3(this.movementX, 0.0f, this.movementY) * 10;

        // Make it relative to the camera.
        Vector3 transformVector = Camera.main.transform.TransformVector(movementForce);
        rb.AddForce(new Vector3(transformVector.x, 0.0f, transformVector.z)); 
    }
}
