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

    [Header("Jumping")]
    public float jumpForce = 8f;
    public float coyoteTime = 0.2f;
    private float coyoteTimer = -1;
    public bool canJump = false;

    [Header("Ground Check")]
    public Transform groundCheckPoint;
    public float groundCheckRadius = 0.2f;
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

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();

        playerInput.actions["Jump"].performed += Jump;
    }

    private void Update()
    {
        moveInput = playerInput.actions["Move"].ReadValue<Vector2>();

        bool isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, groundLayer);

        if (rb.velocity.y < 0 && isGrounded)
        {
            canJump = true;
            Debug.Log("canJump");
            coyoteTimer = -1;
        }
        else
        {
            if (coyoteTimer <= 0)
                coyoteTimer = coyoteTime;
        }

        /*if (coyoteTimer > 0)
        {
            coyoteTimer -= Time.deltaTime;
            if (coyoteTimer <= 0)
                canJump = false;
        }*/

        // Animation switching
        if (!isGrounded)
        {
            animator.enabled = false;
            sr.sprite = jumpSprite;
        }
        else
        {
            animator.enabled = true;
        }
    }

    private void FixedUpdate()
    {
        // Apply horizontal movement
        Vector2 velocity = rb.velocity;
        velocity.x = moveInput.x * runSpeed;
        rb.velocity = velocity;

        // Flip player direction
        if (moveInput.x > 0.01f)
            transform.eulerAngles = new Vector3(0, 0, 0);
        else if (moveInput.x < -0.01f)
            transform.eulerAngles = new Vector3(0, 180, 0);

        // Move with platform
        if (currentPlatform != null)
        {
            Vector3 delta = currentPlatform.position - lastPlatformPos;
            transform.position += delta;
            lastPlatformPos = currentPlatform.position;
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && canJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
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
                if (contact.normal.y > 0.5f)
                {
                    currentPlatform = collision.transform;
                    lastPlatformPos = currentPlatform.position;
                    break;
                }
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            if (currentPlatform == collision.transform)
            {
                currentPlatform = null;
            }
        }
    }
}
