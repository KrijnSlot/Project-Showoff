using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovemenet : MonoBehaviour
{
    public float speed;
    [SerializeField] float jumpStrenght = 400;
    private Vector2 moveInput;
    Rigidbody2D rb;
    private PlayerInput playerInput;
    private InputAction moveAction;
    [SerializeField] bool canJump = false;
    [SerializeField] float coyoteTime;
    [SerializeField] float coyoteTimer = -1;
    PlayerPowers powers;



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
        if (canJump && context.performed)
        {
            rb.AddForceY(jumpStrenght * rb.gravityScale);
            canJump = false;
        }
    }

}
