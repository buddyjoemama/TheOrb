using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private Rigidbody rigidBody;
    private float movementX;
    private float movementY;
    private Camera mainCamera;
    private Vector3 cameraOffset;
    // Start is called before the first frame update

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
        cameraOffset = Camera.main.transform.position - transform.position;
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
    }

    private void OnTriggerEnter(Collider other)
    {
        // other.GetComponent
        var pickup = other.GetComponent<Pickup>();

        if(pickup != null)
        {
            pickup.Apply(this);
        }
    }
}
