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
        Vector2 Inputvector = new Vector2(moveInput.x,0);
        Vector2 movement = Inputvector * speed;
        rb.velocity = new Vector2 (movement.x,rb.velocityY);
        TurnCheck();
    }
    private void Update()
    {
        if ((rb.velocity.y < 0 && rb.gravityScale >0) || (rb.velocity.y >0 && rb.gravityScale <0) )
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
        if (Physics2D.Raycast(transform.position, -Vector3.up, transform.localScale.y / 4, LayerMask.GetMask("Ground")))
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

    void TurnCheck()
    {
        if(moveInput.x > 0 && !facingRight)
        {
            Turn();
        }
        else if(moveInput.x < 0 && facingRight)
        {
            Turn();
        }
    }

    void Turn()
    {
        if (facingRight)
        {
            print("PlayerTrans: " + trans.rotation);
            Vector3 rot = new Vector3(trans.rotation.x, 180f, trans.rotation.z);
            transform.rotation = Quaternion.Euler(rot);
            facingRight = false;
            camFollow.CallTurn();
        }
        else
        {
            print("PlayerTrans: " + trans.rotation);
            Vector3 rot = new Vector3(trans.rotation.x, 0f, trans.rotation.z);
            print(rot);
            transform.rotation = Quaternion.Euler(rot);
            facingRight = true;
            camFollow.CallTurn();
        }
    }

}
