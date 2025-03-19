using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;
    private BoxCollider2D coll;

    [SerializeField] private LayerMask jumpableGround;

    private float dirx = 0f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;
    [SerializeField] private float doubleJumpForce = 16f;

    private bool canDoubleJump;

    //walls
    bool isTouchingRight;
    bool isTouchingLeft;

    bool isTouchingFront = false;
    bool wallSliding;

    public float wallSlidingSpeed = 5f;

    //walls
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PalkaRight"))
        {
            isTouchingFront = true;
            isTouchingRight = true;
        }

        if (collision.gameObject.CompareTag("PalkaLeft"))
        {
            isTouchingFront = true;
            isTouchingLeft = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isTouchingFront = false;
        isTouchingRight = false;
        isTouchingLeft = false;
    }

    private enum MovementState { idle, running, jumping, falling }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        //horizontal
        dirx = Input.GetAxisRaw("Horizontal");

        rb.velocity = new Vector2(dirx * moveSpeed, rb.velocity.y);

        //jump
        if (Input.GetButtonDown("Jump"))
        {
            if (IsGrounded())
            {
                Jump(jumpForce);
                canDoubleJump = true;
            }
            else if (canDoubleJump)
            {
                Jump(doubleJumpForce);
                canDoubleJump = false;
            }
        }

        //check for wall sliding
        if (isTouchingFront && !IsGrounded())
        {
            wallSliding = true;
        }
        else
        {
            wallSliding = false;
        }

        //wall jumping
        if (wallSliding && Input.GetButtonDown("Jump"))
        {
            WallJump();
        }

        UpdateAnimationState();
    }

    private void Jump(float jumpSpeed)
    {
        if (IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
        }
        else if (canDoubleJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0f);
            rb.AddForce(new Vector2(0f, jumpSpeed), ForceMode2D.Impulse);
            canDoubleJump = false;
        }
    }

    private void WallJump()
    {
        rb.velocity = new Vector2(-isTouchingLeft.ToFloat() * jumpForce * 0.75f, jumpForce * 0.75f);
        canDoubleJump = true;
        wallSliding = false;
    }

    private void UpdateAnimationState()
    {
        MovementState state;

        if (wallSliding)
        {
            anim.Play("Wall");
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
            return;
        }

        if (dirx != 0f)
        {
            state = MovementState.running;
            sprite.flipX = dirx < 0f;
        }
        else
        {
            state = MovementState.idle;
        }

        if (rb.velocity.y > 0.1f)
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -0.1f)
        {
            state = MovementState.falling;
        }

        anim.SetInteger("state", (int)state);
    }

    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, 0.2f, jumpableGround);
        return hit.collider != null;
    }
}

public static class BoolExtensions
{
    public static float ToFloat(this bool value)
    {
        return value ? 1f : 0f;
    }
}