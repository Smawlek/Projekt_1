using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Luminosity.IO;

public class PlayerMovement : MonoBehaviour
{
    public static Transform PlayerTransform;

    public int playerSpeed = 5;
    public int jumpForce = 8;
    public int additionalJumps = 1;
    public int dashSpeed = 13;

    public float jumpCheckRadius = 0.3f;
    public float wallCheckDistance = 1f;
    public float wallSlidingSpeed = 0.1f;
    public float knockback = 10f;
    public float knockbackLenght = 0.15f;
    public float wallJumpForce = 20f;
    public float dashTime = 3f;

    public bool canMove = true;
    public bool canJump = true;
    public bool canDash = true;
    public bool isWallSlideEnabled = false;
    public bool isWallJumpEnabled = false;

    public LayerMask whatIsGround;

    public Rigidbody2D playerRig;

    public Transform feetPos;
    public Transform wallCheck;

    public UI_Controller uiController;
    public PlayerInfo playerInfo;

    // Skok
    private bool isGrounded = true;
    private bool isJumping = false;
    private bool isWallSliding = false;
    private bool isItWallJump = false;
    private bool knockFromRight = false;
    private bool checkKnockBackOnce = true;

    private float jumpTime = 0.3f;
    private float jumpTimeCounter = 0;
    private float knockbackCount = 0;

    private int additionalJumpsRemaining;

    // Wall Sliding
    private bool isTouchingWall = false;

    // Dash
    private bool isDashing = false;
    private float remainingDashTime;
    private int remainingDashes = 1;
    private int numberOfDashes = 1;

    // Start is called before the first frame update
    void Start()
    {
        PlayerTransform = this.transform;
        additionalJumpsRemaining = additionalJumps;
        remainingDashes = numberOfDashes;
    }

    // Update is called once per frame
    void Update()
    {
        CheckSurroundings();

        if(!uiController.isOverlayActive)
		{
            // Pohyb
            if (canMove)
            {
                Move();

                // Skok
                if (canJump && (isGrounded || additionalJumpsRemaining > 0))
                {
                    Jump();
                }
            }
            
            if (canDash && (remainingDashes > 0 || isGrounded))
            {
                Dash();
            }
        }

        WallSlide();
        KnockBackCheck();
    }

	void FixedUpdate()
	{
        if(isDashing)
		{
            Dashing();
		}
	}

    void OnDrawGizmosSelected()
    {
        // Wall Check
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y, wallCheck.position.z));
    }

    private void CheckSurroundings()
	{
        isGrounded = Physics2D.OverlapCircle(feetPos.position, jumpCheckRadius, whatIsGround);

        if(isGrounded)
		{
            additionalJumpsRemaining = additionalJumps;
            remainingDashes = numberOfDashes;
        }

        isTouchingWall = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsGround);
    }

    private void Dash()
	{
        if (InputManager.GetButtonDown("Dash"))
		{
            if(!isGrounded)
			{
                remainingDashes--;
			}

            remainingDashTime = dashTime;
            
            Dashing();
        }
	}

    private void Dashing()
	{
        canMove = false;
        isDashing = true;

        remainingDashTime -= Time.deltaTime;

        if (playerInfo.facingRight)
        {
            playerRig.velocity = Vector2.right * dashSpeed;
        }
        else
        {
            playerRig.velocity = Vector2.left * dashSpeed;
        }

        if (remainingDashTime <= 0)
        {
            playerRig.velocity = Vector2.zero;
            canMove = true;
            isDashing = false;
        }
    }

    private void Move()
	{
        float horizontal = InputManager.GetAxisRaw("Horizontal");

        if (horizontal != 0)
        {
            playerRig.velocity = new Vector2(horizontal * playerSpeed * Time.fixedDeltaTime, playerRig.velocity.y);
        } else
		{
            playerRig.velocity = new Vector2(0, playerRig.velocity.y);
		}
    }

    private void Jump()
	{
        isGrounded = Physics2D.OverlapCircle(feetPos.position, jumpCheckRadius, whatIsGround);

        // Start skoku
        if(InputManager.GetButtonDown("Jump") && (isGrounded || additionalJumpsRemaining > 1 || isWallSliding))
		{
            if(isWallSliding && isWallJumpEnabled)
			{
                WallJump();

                return;
			}

            isJumping = true;
            jumpTimeCounter = jumpTime;

            playerRig.velocity = new Vector2(playerRig.velocity.x, jumpForce);

            if (!isGrounded)
            {
                additionalJumpsRemaining--;
            }
        }

        // Během držení tlačítka pro skok
        if(InputManager.GetButton("Jump") && isJumping)
		{
            if (jumpTimeCounter > 0)
            {
                playerRig.velocity = new Vector2(playerRig.velocity.x, jumpForce);

                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        // Konec skoku
        if(InputManager.GetButtonUp("Jump"))
		{
            isJumping = false;
		}
    }

    private void WallJump()
	{
        isItWallJump = true;

        ActiveKnockBack(playerInfo.facingRight);
	}

    private void WallSlide()
	{
        if (isTouchingWall && !isGrounded && playerRig.velocity.y < 0)
        {
            isWallSliding = true;
        }
        else
        {
            isWallSliding = false;
        }

        if (isWallSliding && isWallSlideEnabled)
		{
            if (playerRig.velocity.y < -wallSlidingSpeed)
			{
				playerRig.velocity = new Vector2(playerRig.velocity.x, -wallSlidingSpeed);
			}
		}
	}

    public void ActiveKnockBack(bool knockFromRight)
	{
        canMove = false;
        checkKnockBackOnce = false;
        this.knockFromRight = knockFromRight;

        knockbackCount = knockbackLenght;
	}

    private void KnockBackCheck()
	{
        if(knockbackCount > 0)
		{
            if (!isItWallJump)
            {
                if (knockFromRight)
                {
                    playerRig.velocity = new Vector2(-knockback, knockback);
                }
                else if (!knockFromRight)
                {
                    playerRig.velocity = new Vector2(knockback, knockback);
                }
            }
            else
            {
                if (knockFromRight)
                {
                    playerRig.velocity = new Vector2(-wallJumpForce, wallJumpForce * 1.9f);
                }
                else if (!knockFromRight)
                {
                    playerRig.velocity = new Vector2(wallJumpForce, wallJumpForce * 1.9f);
                }
            }

            knockbackCount -= Time.deltaTime;
        } else if(!checkKnockBackOnce)
		{
            canMove = true;
            isItWallJump = false;
            checkKnockBackOnce = true;

            playerRig.velocity = Vector2.zero;
        }
    }

    public void UnlockDoubleJump()
	{
        additionalJumps = 2;
	}

    public void UnlockDash()
	{
        canDash = true;
	}

    public void UnlockWallJump()
	{
        isWallJumpEnabled = true;
	}
}
