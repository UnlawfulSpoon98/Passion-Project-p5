using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f; // player speed
    public float jumpForce = 7f; // force of player jump
    public float dashSpeed = 10f; // dash speed
    public float dashDuration = 0.5f; // duration of dash in seconds
    public float dashCooldown = 1f; // cooldown period for dashing in seconds
    public int maxJumps = 2; // maximum number of jumps the player can make

    private Rigidbody2D rb;
    private float horizontalInput;
    private bool isGrounded;
    private int jumpsLeft;
    private bool isDashing;
    private float dashTimeLeft;
    private float dashCooldownTimeLeft;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jumpsLeft = maxJumps;
    }

    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (jumpsLeft > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                jumpsLeft--;
            }
            else if (jumpsLeft == 0 && isGrounded == true)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && !isDashing && dashCooldownTimeLeft <= 0f)
        {
            isDashing = true;
            dashTimeLeft = dashDuration;
            dashCooldownTimeLeft = dashCooldown;
        }

        if (isDashing)
        {
            rb.velocity = new Vector2(horizontalInput * dashSpeed, rb.velocity.y);
            dashTimeLeft -= Time.deltaTime;

            if (dashTimeLeft <= 0f)
            {
                isDashing = false;
                rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);
            }
        }
        else
        {
            rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);
        }

        if (dashCooldownTimeLeft > 0f)
        {
            dashCooldownTimeLeft -= Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            jumpsLeft = maxJumps;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
