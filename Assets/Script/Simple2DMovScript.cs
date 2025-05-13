using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float runSpeed = 0.6f;
    public float jumpForce = 2.6f;

    public Sprite jumpSprite;

    private Rigidbody2D body;
    private SpriteRenderer sr;
    private Animator animator;

    public bool isGrounded;
    public Transform groundCheckPoint;
    public float groundCheckRadius;
    public LayerMask groundLayer;

    private bool jumpPressed = false;
    private bool APressed = false;
    private bool DPressed = false;

    private Transform currentPlatform = null;
    private Vector3 lastPlatformPos;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) jumpPressed = true;
        if (Input.GetKey(KeyCode.A)) APressed = true;
        if (Input.GetKey(KeyCode.D)) DPressed = true;
    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, groundLayer);

        // Player input movement
        if (APressed)
        {
            body.velocity = new Vector2(-runSpeed, body.velocity.y);
            transform.eulerAngles = new Vector3(0, 180, 0);
            APressed = false;
        }
        else if (DPressed)
        {
            body.velocity = new Vector2(runSpeed, body.velocity.y);
            transform.eulerAngles = new Vector3(0, 0, 0);
            DPressed = false;
        }
        else
        {
            body.velocity = new Vector2(0, body.velocity.y);
        }

        // Jump
        if (jumpPressed && isGrounded)
        {
            body.velocity = new Vector2(body.velocity.x, jumpForce);
            jumpPressed = false;
            currentPlatform = null;
        }

        // Platform follow logic
        if (currentPlatform != null)
        {
            Vector3 delta = currentPlatform.position - lastPlatformPos;
            transform.position += delta;
            lastPlatformPos = currentPlatform.position;
        }

        // Animation
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

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                if (contact.normal.y > 0.5f) 
                {
                    currentPlatform = collision.transform;
                    lastPlatformPos = currentPlatform.position;
                    Debug.Log("Landed on moving platform");
                    break;
                }
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            if (currentPlatform == collision.transform)
            {
                currentPlatform = null;
                Debug.Log("Left moving platform");
            }
        }
    }
}