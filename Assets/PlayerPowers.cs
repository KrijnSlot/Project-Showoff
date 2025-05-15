using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPowers : MonoBehaviour
{
    Rigidbody2D rb;
    PlayerInput input;

    enum Powers
    {
        gravityManip,
        timeManip,
        sizeManip,
        astralProject
    };
    [SerializeField] Powers currentPower = Powers.gravityManip;

    private void Start()
    {
        input = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody2D>();
    }
    public void UsePower(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            switch (currentPower)
            {
                case Powers.gravityManip: Flip(); break;
                case Powers.timeManip: print("Za Warudo"); break;
                case Powers.sizeManip: SizeManipulation(); break;
                case Powers.astralProject: print("Dr.Strange"); break;
            }

        }
    }

    [SerializeField] private float maxSizeCap = 5f;
    [SerializeField] private float minSizeCap = 0.25f;
    void SizeManipulation()
    {
        Vector3 pScale = transform.localScale;
        float scaleSpeed = 0.01f;

        if (input.actions["Grow"].ReadValue<bool>() && pScale.x <= maxSizeCap)
        {
            pScale += new Vector3(scaleSpeed, scaleSpeed, 0);
            Debug.Log("increasing size");
        }
        if (input.actions["Shrink"].ReadValue<bool>() && pScale.x >= minSizeCap)
        {
            pScale -= new Vector3(scaleSpeed, scaleSpeed, 0);
            Debug.Log("decreasing size");
        }
        // puts player back to a scale of 1
        if (input.actions["Stabalize"].ReadValue<bool>())
        {
            if (pScale.x >= 1.001f)
            {
                pScale -= new Vector3(scaleSpeed, scaleSpeed, 0);
            }
            else if (pScale.x <= 0.999f)
            {
                pScale += new Vector3(scaleSpeed, scaleSpeed, 0);
            }
        }
        transform.localScale = pScale;
    }

    public bool flipped = false;
    public bool canFlip = false;
    void Flip()
    {
        if (canFlip)
        {
            if (flipped)
            {
                Vector3 rot = new Vector3(180, 0, 0);
                print("Rotation: " + rot);
                transform.position = new Vector3 (transform.position.x, transform.position.y- transform.localScale.y/2, 0);
                transform.localEulerAngles = new Vector3(0,transform.localEulerAngles.y,0);
                print("RotationPost: " + transform.eulerAngles);
                print("flipped rightSideUp");
                flipped = false;
            }
            else
            {
                Vector3 rot = new Vector3(180, 0, 0);
                print("Rotation: " + rot);
                transform.position = new Vector3(transform.position.x, transform.position.y + transform.localScale.y/2, 0);
                transform.localEulerAngles = new Vector3(180, transform.localEulerAngles.y, 0);
                print("RotationPost: " + transform.eulerAngles);
                print("flipped upSideDown");
                flipped = true;
            }
            rb.velocityY = rb.velocityY / 2;
            rb.gravityScale *= -1;
            canFlip = false;
        }
    }
}
