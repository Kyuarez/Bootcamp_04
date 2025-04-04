using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float moveSpeed = 5.0f;
    private float jumpForce = 7.5f;

    public Transform groundChecker;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    private Rigidbody2D rigid;
    private SpriteRenderer spr;
    private bool isGrounded;
    private bool isJump = false;

    private PlayerAnimation anim;
    
    public bool IsGrounded
    {
        get { return isGrounded; }
    }

    public bool CheckFlip()
    {
        return spr.flipX;
    }


    private void Awake()
    {
        spr = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<PlayerAnimation>();
    }

    public void HandleMovement()
    {
        Movement();
        OnJump();
    }

    private void Movement()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rigid.linearVelocity = new Vector2(moveInput * moveSpeed, rigid.linearVelocity.y);

        if (moveInput > 0)
        {
            spr.flipX = false;
        }
        else if(moveInput < 0)
        {
            spr.flipX = true;
        }

        if(isJump == false)
        {
            anim?.SetRunning((moveInput != 0) ? true : false);
        }
    }

    private void OnJump()
    {
        isGrounded = Physics2D.OverlapCircle(groundChecker.position, groundCheckRadius, groundLayer);
        if (Input.GetButtonDown("Jump") && isGrounded == true)
        {
            isJump = true;
            rigid.linearVelocity = new Vector2(rigid.linearVelocity.x, jumpForce);
            anim?.OnJump();
        }

        //bool isAirborne = !isGrounded;

        //if(isAirborne && rigid.linearVelocity.y < -0.1f)
        //{
        //    anim?.OnFall();
        //}

        //if (!isGrounded)
        //{
        //    anim.OnGrounded();
        //}

        //if(isGrounded == false)
        //{
        //    anim?.SetRunning(false);
        //}

        if (rigid.linearVelocityY < 0)
        {
            anim?.OnFall();
        }

        if (isGrounded == true)
        {
            isJump = false;
            anim?.OnGrounded();
        }
    }

}
