using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ThreeDimensionalRigidbodyMovement : MonoBehaviour
{
    private new Rigidbody rigidbody;
    private float speed = 6f;
    private float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    public Transform groundCheck;
    private float groundDistance = 0.4f;
    private LayerMask groundMask = 6;

    private Transform cam;

    private float jumpHeight = 10;
    private Vector3 velocity;
    private float gravity = -9.81f;

    private float fallMultiplier = 2.5f;
    private float lowJumpMultiplier = 2f;

    bool isGrounded;
    private void Start()
    {
        rigidbody = gameObject.GetComponent<Rigidbody>();
        rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
        groundCheck = transform.GetChild(0);
        cam = Camera.main.transform;
    }

    void FixedUpdate()
    {
        //not working atm
        //isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        isGrounded = groundCheck.gameObject.GetComponent<GroundCheck>().IsGrounded;

        Move();

        Jump();
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rigidbody.velocity = Vector3.up * jumpHeight;
        }

        if (rigidbody.velocity.y < 0)
        {
            rigidbody.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rigidbody.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rigidbody.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    private void Move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            gameObject.transform.position += moveDir * speed * Time.deltaTime;
        }
    }
}
