using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float runSpeed = 6f;
    public float speedIncrement;

    [Header("Jumping")]
    public float jumpForce = 8f;
    public float jumpIncrement;
    public float coyoteTime = 0.2f;
    private float coyoteTimer = -1;
    public bool canJump = false;

    [Header("Ground Check")]
    public LayerMask groundLayer;

    [Header("Platform Following")]
    private Transform currentPlatform = null;
    private Vector3 lastPlatformPos;

    [Header("Graphics")]
    public Sprite jumpSprite;
    private SpriteRenderer sr;
    private Animator animator;

    private Rigidbody2D rb;
    private PlayerInput playerInput;
    private Vector2 moveInput;
    private bool jumpPressed;
    public bool facingRight = true;
    [SerializeField] CameraFollowObj camFollow;

    PlayerPowers powers;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
        powers = GetComponent<PlayerPowers>();
        speedIncrement = runSpeed;
        jumpIncrement = jumpForce;

        playerInput.actions["Jump"].performed += Jump;
    }

    private void Update()
    {
        // Dynamic stats
        

        // Movement input
        moveInput = playerInput.actions["Move"].ReadValue<Vector2>();
        rb.velocity = new Vector2(moveInput.x * runSpeed, rb.velocity.y);
        TurnCheck();

        // Ground check for jumping/coyote
        if ((rb.velocity.y < 0 && rb.gravityScale > 0) || (rb.velocity.y > 0 && rb.gravityScale < 0))
            JumpCheck();

        // Coyote timer
        if (coyoteTimer > 0)
        {
            coyoteTimer -= Time.deltaTime;
            if (coyoteTimer <= 0)
                canJump = false;
        }
    }

    private void FixedUpdate()
    {
        if (currentPlatform != null)
        {
            Vector3 delta = currentPlatform.position - lastPlatformPos;
            transform.position += delta;
            lastPlatformPos = currentPlatform.position;
        }

        if (runSpeed <= 18)
            runSpeed = speedIncrement / transform.localScale.x;
        if (runSpeed > 18) runSpeed = 18;
        if (jumpForce <= 22)
            jumpForce = jumpIncrement / transform.localScale.x;
        if (jumpForce > 10) jumpForce = 10;
    }


    void JumpCheck()
    {
        // Combine ground and platform layers
        LayerMask combinedLayer = groundLayer | LayerMask.GetMask("Platform");

        Vector2 rayDir = Vector2.down * Mathf.Sign(rb.gravityScale); // handles flipped gravity
        float rayLength = transform.localScale.y / 4f;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, rayDir, rayLength, combinedLayer);

        if (hit.collider != null)
        {
            canJump = true;
            coyoteTimer = -1;
            powers.canFlip = true;
        }
        else
        {
            if (coyoteTimer <= 0)
                coyoteTimer = coyoteTime;
        }
    }


    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && canJump)
        {
            float direction = powers.flipped ? -1f : 1f;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce * direction);
            canJump = false;
            coyoteTimer = -1;
            currentPlatform = null;
        }
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
                    lastPlatformPos = currentPlatform.position;
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
        Vector3 rot;

        if (moveInput.x < 0)
            rot = powers.flipped ? new Vector3(180f, 180f, 0) : new Vector3(0f, 180f, 0);
        else
            rot = powers.flipped ? new Vector3(180f, 0f, 0) : new Vector3(0f, 0f, 0);

        transform.localEulerAngles = rot;
        camFollow.CallTurn();
        facingRight = moveInput.x > 0;
    }
}
