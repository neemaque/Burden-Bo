using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private CustomInput input = null;
    private float speed = 6f;
    private float jumpingPower = 12f;
    private bool isFacingRight = true;
    private bool isDead = false;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Collider2D collider;
    private void Awake()
    {
        input = new CustomInput();
    }

    private void OnEnable() 
    {
        input.Enable();
        input.Player.XMovement.performed += OnXMovementPerformed;
        input.Player.XMovement.canceled += OnXMovementCancelled;
        input.Player.Jump.performed += OnJumpPerformed;
    } 

    private void OnDisable()
    {
        input.Disable();
        input.Player.XMovement.performed -= OnXMovementPerformed;
        input.Player.XMovement.canceled -= OnXMovementCancelled;
        input.Player.Jump.performed -= OnJumpPerformed;
    }

    private void OnXMovementPerformed(InputAction.CallbackContext value)
    {
        if(!isDead)horizontal = value.ReadValue<float>();
    }
    private void OnXMovementCancelled(InputAction.CallbackContext value)
    {
        horizontal = 0;
    }
    private void OnJumpPerformed(InputAction.CallbackContext value)
    {
        if(IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }
    }


    void Update()
    {
        Flip();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private void Death(int hitDirection)
    {
        isDead = true;
        rb.constraints = RigidbodyConstraints2D.None;
        rb.AddForce(new Vector2(hitDirection*2f,8f), ForceMode2D.Impulse);
        rb.AddTorque(100f);
        collider.enabled = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Spike")
        {
            int hitDirection = 0;
            if(collision.gameObject.transform.position.x > transform.position.x)hitDirection = -1;
            else hitDirection = 1;
            Death(hitDirection);
        }
    }
}
