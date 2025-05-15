using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI.Table;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float runSpeed = 6f;
    public float speedIncrement = 1f; // New variable for speed increment

    [Header("Jumping")]
    public float jumpForce = 8f;
    public float jumpIncrement = 1f; // New variable for jump force increment
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

        playerInput.actions["Jump"].performed += Jump;

    }

    private void Update()
    {
        // Adjust runSpeed and jumpForce based on the player's scale
        runSpeed = 6f * transform.localScale.x * speedIncrement;
        jumpForce = 8f * transform.localScale.x * jumpIncrement;

        moveInput = playerInput.actions["Move"].ReadValue<Vector2>();
        Vector2 Inputvector = new Vector2(moveInput.x, 0);
        Vector2 movement = Inputvector * runSpeed;
        rb.velocity = new Vector2(movement.x, rb.velocity.y);
        TurnCheck();

        if ((rb.velocity.y < 0 && rb.gravityScale > 0) || (rb.velocity.y > 0 && rb.gravityScale < 0))
        {
            JumpCheck();
        }


        if (coyoteTimer > 0)
        {
            coyoteTimer -= Time.deltaTime;
            if (coyoteTimer <= 0)
                canJump = false;
        }

    }

    private void FixedUpdate()
    {
        if (Physics2D.Raycast(transform.position, -Vector3.up, transform.localScale.y / 4, LayerMask.GetMask("Platform")))
        {
            Vector3 delta = currentPlatform.position - lastPlatformPos;
            transform.position += delta;
            lastPlatformPos = currentPlatform.position;
        }
    }

    void JumpCheck()
    {
        if (Physics2D.Raycast(transform.position, -Vector3.up * rb.gravityScale, transform.localScale.y / 4, LayerMask.GetMask("Ground")))
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
            if(!powers.flipped)
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            else rb.velocity = new Vector2(rb.velocity.x, -jumpForce);
            canJump = false;
            coyoteTimer = -1;
            currentPlatform = null;
        }
    }

    /*private void OnCollisionEnter2D(Collision2D collision)
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
    }*/

    void TurnCheck()
    {
        if (moveInput.x > 0)
        {
            Turn();
        }
        else if (moveInput.x < 0)
        {
            Turn();
        }
    }

    void Turn()
    {
        if (moveInput.x < 0)
        {
            print("PlayerTrans: " + gameObject.transform.eulerAngles);
            Vector3 rot;
            if (powers.flipped) rot = new Vector3(180f, 180f, 0);
            else rot = new Vector3(0f, 180f, 0);
            print("Rotation: " + rot);
            transform.localEulerAngles = rot;
            print("PlayerTrans2: " + transform.rotation.eulerAngles);
            if (facingRight) camFollow.CallTurn();
            facingRight = false;
        }
        else if (moveInput.x > 0)
        {
            print("PlayerTrans: " + gameObject.transform.eulerAngles);
            Vector3 rot;
            if (powers.flipped) rot = new Vector3(180f, 0f, 0);
            else rot = new Vector3(0f, 0f, 0);
            transform.localEulerAngles = rot;
            print("PlayerTrans2: " + transform.rotation.eulerAngles);
            if (!facingRight) camFollow.CallTurn();
            facingRight = true;
        }

    }

}
