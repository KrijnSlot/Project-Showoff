using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

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

    private bool jumpAnimPlayed;

    [Header("Audio")]
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip landSound;
    [SerializeField] private AudioClip runSound;

    private AudioSource runAudioSource;

    private void Awake()
    {
        transform.position = spawnPoint.transform.position;
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
        powers = GetComponent<PlayerPowers>();

        runAudioSource = gameObject.AddComponent<AudioSource>();
        runAudioSource.clip = runSound;
        runAudioSource.loop = true;
        runAudioSource.playOnAwake = false;

        speedIncrement = runSpeed;
        jumpIncrement = jumpForce;

        playerInput.actions["Jump"].performed += JumpInput;
    }

    private void Update()
    {
        moveInput = playerInput.actions["Move"].ReadValue<Vector2>();

        AnimationHandler();
        TurnCheck();
        JumpCheck();

        if (coyoteTimer > 0)
        {
            coyoteTimer -= Time.deltaTime;
            if (coyoteTimer <= 0)
                canJump = false;
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveInput.x * runSpeed, rb.velocity.y);

        // Adjust speed/jump force based on size
        switch (powers.currentSize)
        {
            case PlayerPowers.PlayerSizes.normal:
                runSpeed = normalSizeRunSpeed;
                jumpForce = normalSizeJumpForce;
                break;
            case PlayerPowers.PlayerSizes.big:
                runSpeed = bigSizeRunSpeed;
                jumpForce = bigSizeJumpForce;
                break;
            case PlayerPowers.PlayerSizes.small:
                runSpeed = smallSizeRunSpeed;
                jumpForce = smallSizeJumpForce;
                break;
        }

        if (currentPlatform != null)
        {
            Vector3 platformDelta = currentPlatform.position - lastPlatformPosition;
            transform.position += platformDelta;
            lastPlatformPosition = currentPlatform.position;
        }
    }

    void AnimationHandler()
    {
        float verticalVelocity = rb.velocity.y;
        bool isMovingHorizontally = Mathf.Abs(moveInput.x) > 0.01f;

        animator.speed = 1f;

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
            animator.SetBool("Jump", false);
            animator.SetBool("isFalling", true);
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

                if (!runAudioSource.isPlaying && runSound != null)
                    runAudioSource.Play();
            }
            else
            {
                animator.SetBool("isRunning", false);
                if (runAudioSource.isPlaying)
                    runAudioSource.Stop();
            }
        }
        else
        {
            animator.SetBool("isRunning", false);
            if (runAudioSource.isPlaying)
                runAudioSource.Stop();
        }

        if (!isJumping && !jumpAnimPlayed)
        {
            animator.SetBool("Jump", false);
        }
    }

    void JumpCheck()
    {
        bool wasGrounded = onGround;

        LayerMask combinedLayer = groundLayer | LayerMask.GetMask("Platform");
        Vector2 rayDir = Vector2.down * Mathf.Sign(rb.gravityScale);
        float rayLength = transform.localScale.y * 5;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, rayDir, rayLength, combinedLayer);

        if (queJump)
        {
            if (jumpBuffer < 0) { queJump = false; jumpBuffer = 0.1f; }
            jumpBuffer -= Time.deltaTime;
        }

        if ((rb.velocity.y < 0 && !powers.flipped) || (rb.velocity.y > 0 && powers.flipped))
            isJumping = false;

        if (hit.collider != null && ((rb.velocity.y <= 0.1f && !powers.flipped) || (rb.velocity.y >= -0.1f && powers.flipped)))
        {
            jumpSlowScale = 0.1f;
            if (powers.currentPower == PlayerPowers.Powers.song)
                powers.canDoubleJump = true;

            canJump = true;
            coyoteTimer = -1;
            onGround = true;
            powers.canFlip = true;

            if (!wasGrounded && landSound != null)
                AudioSource.PlayClipAtPoint(landSound, transform.position);
        }
        else
        {
            if (coyoteTimer <= 0)
                coyoteTimer = coyoteTime;
            onGround = false;

            if (runAudioSource.isPlaying)
                runAudioSource.Stop();
        }

        if (isJumping)
        {
            jumpSlowScale += Time.deltaTime * jumpSlowMult;
            rb.velocity -= new Vector2(0, Time.deltaTime * jumpSlowScale * rb.gravityScale);
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
            rb.gravityScale = powers.flipped ? -baseGravityScale : baseGravityScale;
        }

        if (queJump && canJump)
            Jump();
    }

    public void JumpInput(InputAction.CallbackContext context)
    {
        if (context.started && !isJumping)
            Jump();

        if (context.canceled)
        {
            if (isJumping)
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y / 2f);
            isJumping = false;
        }
    }

    void Jump()
    {
        if (canJump || powers.canDoubleJump)
        {
            rb.gravityScale = 1;
            float direction = powers.flipped ? -1f : 1f;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce * direction);

            if (transform.parent.transform.parent == currentPlatform && currentPlatform != null)
            {
                transform.parent.SetParent(null);
                moveInput += new Vector2(moveInput.x, rb.velocity.y);
                currentPlatform = null;
            }

            if (!canJump)
                powers.canDoubleJump = false;
            else
                canJump = false;

            isJumping = true;
            coyoteTimer = -1;
            currentPlatform = null;
            queJump = false;
            queHop = false;

            if (jumpSound != null)
                AudioSource.PlayClipAtPoint(jumpSound, transform.position);
        }
        else queJump = true;
    }

    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                float normalY = contact.normal.y;

                if ((!powers.flipped && normalY > 0.5f) || (powers.flipped && normalY < -0.5f))
                {
                    currentPlatform = collision.transform;
                    transform.parent.SetParent(currentPlatform);
                    break;
                }
            }
        }
    }*/

    Vector3 lastPlatformPosition;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("MovingPlatform"))
        {
            currentPlatform = collision.transform;
            lastPlatformPosition = currentPlatform.position;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform == currentPlatform && collision.collider.CompareTag("MovingPlatform"))
        {
            currentPlatform = null;
        }
    }

    /*private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MovingPlatform") && currentPlatform == collision.transform)
        {
            transform.parent.SetParent(null);
            currentPlatform = null;
        }
    }*/

    void TurnCheck()
    {
        if (moveInput.x > 0 && !facingRight) Turn();
        else if (moveInput.x < 0 && facingRight) Turn();
    }

    void Turn()
    {
        Vector3 rot = moveInput.x < 0
            ? (powers.flipped ? new Vector3(180f, 180f, 0) : new Vector3(0f, 180f, 0))
            : (powers.flipped ? new Vector3(180f, 0f, 0) : new Vector3(0f, 0f, 0));

        transform.localEulerAngles = rot;
        facingRight = moveInput.x > 0;
        camFollow.CallTurn(facingRight);
    }
}
