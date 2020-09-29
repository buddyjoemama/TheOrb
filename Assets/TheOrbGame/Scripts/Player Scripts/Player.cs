﻿using System.Collections;
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
    public Transform shield;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        shield.gameObject.SetActive(false);
    }

    void OnFire() 
    {
        // Projectile clone = (Projectile)Instantiate(projectile, transform.position, transform.rotation);
        //clone.Fire();

        var text = Instantiate(projectile, transform.position, transform.rotation);

        Destroy(text.gameObject, 1.0f);
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

       rb.AddForce(new Vector3(transformVector.x, 0.0f, transformVector.z));
    }

    private void Update()
    {
        if (shield != null)
            shield.position = transform.position;
    }
}