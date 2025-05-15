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
                case Powers.sizeManip: print("Ant Man"); break;
                case Powers.astralProject: print("Dr.Strange"); break;
            }

        }
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
