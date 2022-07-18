using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public delegate void OnShotFired(Player player);

public class Player : AbstractHittable
{
    private float movementX;
    private float movementY;
    private GunRig rig;
    private ShieldContainer shieldContainer;
    private PlayerInput input;
    private IEnumerable<IPlayerPowerup> powerups;
    public event OnShotFired OnShotFired;

    private void Awake()
    {
        RigidBody = GetComponent<Rigidbody>();
        rig = GetComponentInChildren<GunRig>();
        shieldContainer = GetComponentInChildren<ShieldContainer>();
        input = GetComponent<PlayerInput>();
        powerups = GetComponents<IPlayerPowerup>().AsEnumerable();
    }

    public bool IsGrounded { get; set; }
    public Rigidbody RigidBody { get; set; }

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

    private void OnFire()
    {
        rig.OnFire();
        if(OnShotFired != null)
            OnShotFired(this);
    }

    private void OnCollisionStay(Collision collision)
    {
        IsGrounded = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Floor"))
        {
            RigidBody.velocity = RigidBody.angularVelocity = Vector3.zero;
        }
    }

    private void FixedUpdate()
    {
        Vector3 movementForce = new Vector3(this.movementX, 0.0f, this.movementY) * 25;

        if (input.GetDevice<Keyboard>().bKey.isPressed)
        {
            RigidBody.velocity = new Vector3(0, 0, 0);
            RigidBody.angularVelocity = new Vector3(0, 0, 0);
        }
        else if(!IsGrounded)
        {
            movementForce /= 10;
        }

        // Make it relative to the camera.
        Vector3 transformVector = Camera.main.transform.TransformVector(movementForce);

        RigidBody.AddForce(new Vector3(transformVector.x, 0.0f, transformVector.z));
    }

    private void OnTriggerEnter(Collider other)
    {
        var trigger = other.GetComponent<ITrigger>();
        
        if(trigger != null)
            trigger.Apply(this);
    }
}
