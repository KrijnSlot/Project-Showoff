using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovemenet : MonoBehaviour
{
    [SerializeField] float speed;
    private Vector2 moveInput;
    private PlayerInput playerInput;
    Rigidbody2D rb;

    [SerializeField] float jumpStrenght = 400;
    [SerializeField] bool canJump = false;
    [SerializeField] float coyoteTime;
    [SerializeField] float coyoteTimer = -1;

    PlayerPowers powers;

    public bool facingRight = true;
    [SerializeField] CameraFollowObj camFollow;

    [SerializeField] Transform trans;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        powers = GetComponent<PlayerPowers>();
    }

    private void FixedUpdate()
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
            JumpCheck();
        }
        if(coyoteTimer > 0)
        {
            coyoteTimer -= Time.deltaTime;
            if (coyoteTimer <= 0)
            {
                canJump = false;
            }
        }
    }

    void JumpCheck()
    {
        if (Physics2D.Raycast(transform.position, -Vector3.up, transform.localScale.y / 4, LayerMask.GetMask("Ground")))
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
        if (canJump && context.performed)
        {
            rb.AddForceY(jumpStrenght * rb.gravityScale);
            canJump = false;
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
