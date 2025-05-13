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
