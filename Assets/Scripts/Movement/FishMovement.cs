using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class FishMovement : BaseMovement
{
    /// <summary>
    /// Set some parameters for this specific character
    /// </summary>
    public override void Start()
    {
        base.Start();
        mass = 3;
        jumpHeight = 5;
        speed = 6;
    }
}

