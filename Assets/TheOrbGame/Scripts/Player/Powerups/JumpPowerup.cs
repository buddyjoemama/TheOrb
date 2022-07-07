﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public class JumpPowerup : PlayerPowerup, IPlayerPowerup
{
    private PlayerInput input;
    public int JumpPower = 10;

    private void Start()
    {
        input = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        if(IsEnabled && Player.IsGrounded && input.GetDevice<Keyboard>().spaceKey.isPressed)
        {
            Player.RigidBody.AddForce(0, JumpPower, 0, ForceMode.Impulse);
            Player.IsGrounded = false;
        }
    }
}