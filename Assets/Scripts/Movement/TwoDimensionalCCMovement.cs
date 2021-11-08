using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class TwoDimensionalCCMovement : MonoBehaviour
{
    private CharacterController controller;
    private float speed = 6f;
    private float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    public Transform groundCheck;
    private float groundDistance = 0.4f;
    private LayerMask groundMask = 6;

    private Transform cam;

    private float jumpHeight = 5;
    private Vector3 velocity;
    private float gravity = -9.81f;

    bool isGrounded;
    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        groundCheck = transform.GetChild(0);
        cam = Camera.main.transform;
    }

    void FixedUpdate()
    {
        //not working atm
        //isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        isGrounded = groundCheck.gameObject.GetComponent<GroundCheck>().IsGrounded;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        Move();

        Jump();
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Debug.Log("jump");
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.fixedDeltaTime;

        controller.Move(velocity * Time.fixedDeltaTime);
    }

    private void Move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = 0;

        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            controller.Move(direction.normalized * speed * Time.fixedDeltaTime);
        }
    }
}
