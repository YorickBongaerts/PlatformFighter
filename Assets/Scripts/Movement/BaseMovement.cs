using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class BaseMovement : MonoBehaviour
{
    #region Variables

    private CharacterController controller;
    private bool isMoving = false;
    private Vector2 inputValues;
    public float speed = 6f;
    private float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;
    private Vector3 velocity;

    public Transform groundCheck;
    private float groundDistance = 0.4f;
    private LayerMask groundMask = 6;
    private float gravity = -9.81f;
    private bool isGrounded;
    public float jumpHeight = 5;
    private bool hasJump;

    public string state;

    private Animator animator;

    private Transform cam;

    public float percent = 0;
    private Vector3 impact = new Vector3();
    public float mass = 3;

    #endregion variables

    #region Unity lifecycle

    public virtual void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        groundCheck = transform.GetChild(0);
        cam = Camera.main.transform;
        animator = gameObject.GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        //not working atm
        //isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        isGrounded = groundCheck.gameObject.GetComponent<GroundCheck>().IsGrounded;
        if (isGrounded)
        {
            hasJump = true;
        }
        if (isGrounded && velocity.y < 0)
        {
            ResetGravity();
        }
        ApplyGravity();
        Move();
    }

    #endregion Unity lifecycle

    #region Basic movement

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isMoving = true;
            inputValues = context.ReadValue<Vector2>();
        }
        if (context.canceled) isMoving = false;
    }

    private void Move()
    {
        float horizontal = inputValues.x;
        float vertical = 0;

        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

        animator.SetFloat("Speed", direction.magnitude);

        //move and turn in the right direction
        if (direction.magnitude >= 0.1f && isMoving)
        {
            gameObject.transform.LookAt(gameObject.transform.position + new Vector3(0, 0, -horizontal));
            controller.Move(direction.normalized * speed * Time.fixedDeltaTime);
        }

        DirectionStates(inputValues);

        // apply the impact force
        if (impact.magnitude > 0.2F) controller.Move(impact * Time.fixedDeltaTime);
        
        // consumes the impact energy each cycle:
        impact = Vector3.Lerp(impact, Vector3.zero, 5 * Time.fixedDeltaTime);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (hasJump)
        {
            Debug.Log("jump");
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            hasJump = false;
        }
        controller.Move(velocity * Time.fixedDeltaTime);
    }

    private void ApplyGravity()
    {
        velocity.y += gravity * Time.fixedDeltaTime;

        controller.Move(velocity * Time.fixedDeltaTime);
    }

    public void ResetGravity()
    {
        velocity.y = -2f;
    }

    #endregion Basic movement

    #region Attack helpers

    /// <summary>
    /// Registers which way the move stick is held and stores the value in the string variable 'state'
    /// </summary>
    /// <param name="inputValues"></param>
    private void DirectionStates(Vector2 inputValues)
    {
        if (inputValues.x == 1 && inputValues.y == 0)
        {
            state = "right";
        }
        if (inputValues.x == -1 && inputValues.y == 0)
        {
            state = "left";
        }
        if (inputValues.x == 0 && inputValues.y == 1)
        {
            state = "up";
        }
        if (inputValues.x == 0 && inputValues.y == -1)
        {
            state = "down";
        }
        if (inputValues.x == 0 && inputValues.y == 0)
        {
            state = "neutral";
        }
    }

    /// <summary>
    /// Calculates the impact to be applied on this character
    /// </summary>
    /// <param name="dir"></param>
    /// <param name="force"></param>
    public void AddImpact(Vector3 dir, float force)
    {
        dir.Normalize();
        if (dir.y < 0) dir.y = -dir.y; // reflect down force on the ground
        impact += dir.normalized * force / mass * percent;
    }
    
    #endregion Attack helpers
}
    