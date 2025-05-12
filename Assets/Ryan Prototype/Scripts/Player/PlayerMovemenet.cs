using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovemenet : MonoBehaviour
{
    public float speed;
    private Vector2 moveInput;
    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Vector2 curPos = rb.position;
        Vector2 Inputvector = new Vector2(moveInput.x, moveInput.y);
        Inputvector = Vector2.ClampMagnitude(Inputvector, 1);
        Vector2 movement = Inputvector * speed;
        Vector2 newPos = curPos + movement * Time.fixedDeltaTime;
        rb.MovePosition(newPos);
    }

    public void OnMove(InputAction.CallbackContext ctx) => moveInput = ctx.ReadValue<Vector2>();
}
