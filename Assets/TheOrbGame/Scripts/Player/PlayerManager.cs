﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[ExecuteInEditMode]
public class PlayerManager : MonoBehaviour
{
    public Player player;
    public Shield mainShield;
    public GunRig rig;
    public BasicProjectile projectile;
    public ShatterableShield shatterableShield;
    public bool shieldEnabled = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(shieldEnabled)
        {
            EnableShield();
        }
        else
        {
            DisableShield();
        }
    }

    private void FixedUpdate()
    {
        mainShield.Move(player.transform.position);
        rig.Move(player.transform.position);
    }

    int shotsFired = 0;
    /// <summary>
    /// Fire the projectile.
    /// </summary>
    void OnFire() 
    {
        rig.Fire(projectile);

        //shotsFired += 1;

        //if(shotsFired >= 5)
        //{
        //    shieldEnabled = false;

        //    ShatterableShield shield = Instantiate(shatterableShield, player.transform.position, player.transform.rotation);
        //    shield.Destroy();
        //}
    }

    /// <summary>
    /// Move the player ball.
    /// </summary>
    /// <param name="move"></param>
    private void OnMove(InputValue move)
    {
        Vector2 movementVector = move.Get<Vector2>();
        player.Move(movementVector);
    }

    /// <summary>
    /// Move the gun rig.
    /// </summary>
    /// <param name="look"></param>
    private void OnLook(InputValue look)
    {
        //Vector2 lookVector = look.Get<Vector2>();

        ////var r = gunRig.rotation.eulerAngles + new Vector3(0, lookVector.y);

        ////gunRig.rotation = Quaternion.Euler(r);

        //gunRig.transform.Rotate(new Vector3(0, lookVector.y * 4), Space.Self);
    }

    public void EnableShield()
    {
        mainShield.Activate();
    }

    public void DisableShield()
    {
        mainShield.Deactivate();
    }
}
