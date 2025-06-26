using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Spawning")]
    public Transform spawnPoint;

    [Header("Movement")]
    public float runSpeed = 6f;
    public float speedIncrement;
    [SerializeField] float normalSizeRunSpeed = 6f;
    [SerializeField] float bigSizeRunSpeed = 6f;
    [SerializeField] float smallSizeRunSpeed = 6f;

    [Header("Jumping")]
    public float jumpForce = 8f;
    public float jumpIncrement;
    public float coyoteTime = 0.2f;
    [SerializeField] float baseGravityScale = 1;
    [SerializeField] float maxGravityScale = 10;
    [SerializeField] float jumpBuffer = 0.1f;
    public float coyoteTimer = -1;
    [SerializeField] private bool isJumping;
    public bool canJump = false;
    [SerializeField] float jumpSlowMult;
    float jumpSlowScale = 0.1f;
    [SerializeField] float normalSizeJumpForce = 8f;
    [SerializeField] float bigSizeJumpForce = 8f;
    [SerializeField] float smallSizeJumpForce = 8f;

    [Header("Ground Check")]
    public LayerMask groundLayer;
    private bool onGround;

    [Header("Platform Following")]
    private Transform currentPlatform = null;
    private Rigidbody2D rbPlatform;

    [Header("Graphics")]
    private SpriteRenderer sr;
    private Animator animator;

    private Rigidbody2D rb;
    private PlayerInput playerInput;
    private Vector2 moveInput;
    private bool jumpPressed;
    public bool facingRight = true;
    [SerializeField] CameraFollowObj camFollow;

    bool queJump = false;
    bool queHop = false;
    PlayerPowers powers;

    float testTimer;

    private bool jumpAnimPlayed;

    private void Awake()
    {
        transform.position = spawnPoint.transform.position;
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
        powers = GetComponent<PlayerPowers>();
        speedIncrement = runSpeed;
        jumpIncrement = jumpForce;

        playerInput.actions["Jump"].performed += JumpInput;
    }

    private void Update()
    {

        AnimationHandler();

        // Dynamic stats

        testTimer += Time.deltaTime;
        // Movement input

        TurnCheck();

        // Ground check for jumping/coyote
        JumpCheck();

        // Coyote timer
        if (coyoteTimer > 0)
        {
            coyoteTimer -= Time.deltaTime;
            if (coyoteTimer <= 0);
            canJump = false;
        }
    }

    private void FixedUpdate()
    {
        moveInput = playerInput.actions["Move"].ReadValue<Vector2>();

        rb.velocity = new Vector2(moveInput.x * runSpeed, rb.velocity.y);

        //Cap speed based on size
        
        if (powers.currentSize == PlayerPowers.PlayerSizes.normal)
        {
            runSpeed = normalSizeRunSpeed;
            jumpForce = normalSizeJumpForce;
        }
        if (powers.currentSize == PlayerPowers.PlayerSizes.big)
        {
            runSpeed = bigSizeRunSpeed;
            jumpForce = bigSizeJumpForce;
        }
        if (powers.currentSize == PlayerPowers.PlayerSizes.small)
        {
            runSpeed = smallSizeRunSpeed;
            jumpForce = smallSizeJumpForce;
        }
    }

    void AnimationHandler()
    {
        animator.speed = 1f;

        float verticalVelocity = rb.velocity.y;
        bool isMovingHorizontally = Mathf.Abs(moveInput.x) > 0.01f;

        if (!canJump && ((!powers.flipped && verticalVelocity > 0.1f) || (powers.flipped && verticalVelocity < -0.1f)))
        {
            if (!jumpAnimPlayed)
            {
                animator.SetBool("Jump", true);
                animator.SetBool("isFalling", false);
                animator.SetBool("isRunning", false);
                jumpAnimPlayed = true;
            }
            return;
        }

        if (!canJump && ((!powers.flipped && verticalVelocity < -0.1f) || (powers.flipped && verticalVelocity > 0.1f)))
        {
            animator.SetBool("isFalling", true);
            animator.SetBool("Jump", false);
            animator.SetBool("isRunning", false);
            return;
        }

        if (canJump)
        {
            jumpAnimPlayed = false;

            animator.SetBool("Jump", false);
            animator.SetBool("isFalling", false);

            if (isMovingHorizontally)
            {
                animator.SetBool("isRunning", true);
                animator.speed = Mathf.Abs(moveInput.x);
            }
            else
            {
                animator.SetBool("isRunning", false);
            }
        }
        else
        {
            animator.SetBool("isRunning", false);
        }

        if (!isJumping && !jumpAnimPlayed)
        {
            animator.SetBool("Jump", false);
        }
    }



    void JumpCheck()
    {
        // Combine ground and platform layers
        LayerMask combinedLayer = groundLayer | LayerMask.GetMask("Platform");

        Vector2 rayDir = Vector2.down * Mathf.Sign(rb.gravityScale); // handles flipped gravity
        float rayLength = transform.localScale.y * 5;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, rayDir, rayLength, combinedLayer);

        if (queJump)
        {
            if (jumpBuffer < 0) { queJump = false; jumpBuffer = 0.1f; }
            jumpBuffer -= Time.deltaTime;

        }

        if (rb.velocityY < 0 && !powers.flipped || rb.velocityY > 0 && powers.flipped)
        {
            isJumping = false;
        }

        {
            if (hit.collider != null && ((rb.velocityY <= 0.1f && !powers.flipped) || (rb.velocityY >= -0.1f && powers.flipped)))
            {
                print("you are falling");
                jumpSlowScale = 0.1f;
                if (powers.currentPower == PlayerPowers.Powers.song)
                    powers.canDoubleJump = true;
                canJump = true;
                coyoteTimer = -1;
                onGround = true;
                powers.canFlip = true;
            }
            else
            {
                if(hit.collider != null) { print("You are not falling"); }
                if (coyoteTimer <= 0)
                    coyoteTimer = coyoteTime;
                onGround = false;
            }
        }

        if (isJumping)
        {
            jumpSlowScale += Time.deltaTime * jumpSlowMult;
            rb.velocityY -= (Time.deltaTime * jumpSlowScale) * rb.gravityScale;
        }

        if (!isJumping && !onGround && powers.canFlip)
        {

            if (!powers.flipped && rb.gravityScale < maxGravityScale)
                rb.gravityScale += Time.deltaTime * 4f;
            else if (powers.flipped && rb.gravityScale > -maxGravityScale)
                rb.gravityScale -= Time.deltaTime * 4f;
        }
        else
        {
            if (!powers.flipped)
                rb.gravityScale = baseGravityScale;
            else rb.gravityScale = -baseGravityScale;
        }

        if (queJump && canJump) { Jump(); }
    }


    public void JumpInput(InputAction.CallbackContext context)
    {

        if (context.started && !isJumping) Jump();
        if (context.canceled)
        {
            if (isJumping) rb.velocityY /= 2;
            isJumping = false;
        }
    }

    void Jump()
    {
        if (canJump || powers.canDoubleJump)
        {
            // detach player from platform when jumping

            rb.gravityScale = 1;
            float direction = powers.flipped ? -1f : 1f;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce * direction);

            if (transform.parent.transform.parent == currentPlatform && currentPlatform != null)
            {
                //rbPlatform = currentPlatform.GetComponent<Rigidbody2D>();
                this.transform.parent.SetParent(null);
                moveInput += new Vector2(moveInput.x, rb.velocity.y);
                currentPlatform = null;
            }

            if (!canJump)
            {
                powers.canDoubleJump = false;
            }
            else
            {
                canJump = false;
            }
            isJumping = true;
            coyoteTimer = -1;
            currentPlatform = null;
            queJump = false;
            queHop = false;
        }
        else queJump = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                float normalY = contact.normal.y;

                // If gravity is normal, look for contact from below (normal pointing up)
                // If gravity is flipped, look for contact from above (normal pointing down)
                if ((!powers.flipped && normalY > 0.5f) || (powers.flipped && normalY < -0.5f))
                {
                    currentPlatform = collision.transform;
                    transform.parent.SetParent(currentPlatform);
                    Debug.Log("Attached to moving platform");
                    break;
                }
            }
        }
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MovingPlatform") && currentPlatform == collision.transform)
        {
            Debug.Log("player detached");
            this.transform.parent.SetParent(null);

            currentPlatform = null;
        }
    }

    void TurnCheck()
    {
        if (moveInput.x > 0 && !facingRight) Turn();
        else if (moveInput.x < 0 && facingRight) Turn();
    }

    void Turn()
    {
        Vector3 rot = new Vector3(0, 0, 0);

        if (moveInput.x < 0)
        {
            rot = powers.flipped ? new Vector3(180f, 180f, 0) : new Vector3(0f, 180f, 0);
        }
        else if (moveInput.x > 0)
        {
            rot = powers.flipped ? new Vector3(180f, 0f, 0) : new Vector3(0f, 0f, 0);
        }

        transform.localEulerAngles = rot;
        facingRight = moveInput.x > 0;
        camFollow.CallTurn(facingRight);
    }
}
