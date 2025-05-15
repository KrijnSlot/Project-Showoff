using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPowers : MonoBehaviour
{
    Rigidbody2D rb;

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

    void SizeManipulation()
    {
        Vector3 pScale = transform.localScale;
        float scaleSpeed = 0.01f;

        if (Input.GetKey(KeyCode.R) && pScale.x <= 5)
        {
            pScale += new Vector3(scaleSpeed, scaleSpeed, 0);
            Debug.Log("increasing size");
        }
        if (Input.GetKey(KeyCode.T) && pScale.x >= 0.25f)
        {
            pScale -= new Vector3(scaleSpeed, scaleSpeed, 0);
            Debug.Log("decreasing size");
        }
        // puts player back to a scale of 1
        if (Input.GetKey(KeyCode.Y))
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

    bool flipped;
    public bool canFlip = false;
    void Flip()
    {
        if (flipped && canFlip)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            flipped = false;
        }
        else
        {
            transform.eulerAngles = new Vector3(180, 0, 0);
            flipped = true;
        }
        rb.velocityY = rb.velocityY / 2;
        rb.gravityScale *= -1;
        canFlip = false;
    }
}
