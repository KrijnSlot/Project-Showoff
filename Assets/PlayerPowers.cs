using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPowers : MonoBehaviour
{
    Rigidbody2D rb;
    PlayerInput input;
    PlayerMovement movementScript;
    CameraFollowObj cam;
    GameManager gameManager;

    public Vector3 pScale;

    enum Powers
    {
        gravityManip,
        timeManip,
        sizeManip,
        astralProject
    };
    [SerializeField] Powers currentPower = Powers.gravityManip;

    bool sizaManipOn;
    bool timeManipOn;
    private void Start()
    {
        gameManager = GameManager.Instance;
        input = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody2D>();
        movementScript = GetComponent<PlayerMovement>();
        cam = transform.parent.GetComponentInChildren<CameraFollowObj>();
    }
    public void UsePower(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            switch (currentPower)
            {
                case Powers.gravityManip: Flip(); break;
                case Powers.timeManip: timeManipOn = !timeManipOn; gameManager.timeScale = 1; break;
                case Powers.sizeManip: sizaManipOn = !sizaManipOn; break;
                case Powers.astralProject: AstralProj(); break;
            }

        }
    }

    private void Update()
    {
        if (sizaManipOn)
            SizeManipulation();
        if (timeManipOn)
            TimeManip();
    }

    [SerializeField] GameObject playerObj;
    [SerializeField] LayerMask playerLayer;
    private GameObject projectionObj;
    private bool isProjecting = false;
    private float projectionCooldown = 0.5f;
    private float lastProjectionTime = -Mathf.Infinity;
    private Vector2 bodyPos;
    void AstralProj()
    {
        if (Time.time - lastProjectionTime < projectionCooldown)
            return;

        lastProjectionTime = Time.time;

        if (!isProjecting)
        {
            Debug.Log("Astral projection started");
            projectionObj = Instantiate(playerObj, playerObj.transform.position, playerObj.transform.rotation);
            projectionObj.name = "ProjectionClone";
            bodyPos = projectionObj.transform.position;
            playerObj.transform.parent.gameObject.layer = LayerMask.NameToLayer("GhostProjection");

            isProjecting = true;
        }
        else
        {
            Debug.Log("Returning to body");
            if (projectionObj != null)
            {
                playerObj.transform.parent.gameObject.layer = playerLayer.value - 1;
                Debug.Log(playerObj.transform.parent.gameObject.layer);
                playerObj.transform.parent.position = bodyPos;
                Destroy(projectionObj);
            }
            isProjecting = false;
        }
    }

    [SerializeField] private float maxSizeCap = 5f;
    [SerializeField] private float minSizeCap = 0.25f;

    [Header("Rescaling Power")]
    int sizeCycle = 1;
    [SerializeField] float bigmode, normalMode, smallMode, scaleSpeed, growthHeight;
    [SerializeField] LayerMask mask;
    void SizeManipulation()
    {

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, growthHeight, mask);
        print(hit.collider);
        Vector3 pScale = transform.localScale;
        float scaleSpd = scaleSpeed;

        if (input.actions["Grow"].IsPressed())
        {
            sizeCycle++;
        }
        if (sizeCycle == 1 && pScale.x <= normalMode - 0.01f)
        {
            pScale += new Vector3(scaleSpd, scaleSpd, 0);
        }
        else if (sizeCycle == 1 && pScale.x >= normalMode + 0.01f)
        {
            pScale -= new Vector3(scaleSpd, scaleSpd, 0);
        }
        if (sizeCycle == 2 && pScale.x <= bigmode - 0.01f && !hit)
        {
            pScale += new Vector3(scaleSpd, scaleSpd, 0);
        }
        else if (sizeCycle == 2 && hit && pScale.x <= bigmode - 0.5)
            sizeCycle--;
        if (sizeCycle == 3 && pScale.x >= smallMode + 0.01f)
        {
            pScale -= new Vector3(scaleSpd, scaleSpd, 0);
        }

        if (sizeCycle >= 4)
            sizeCycle = 1;
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
                transform.position = new Vector3(transform.position.x, transform.position.y - transform.localScale.y / 2, 0);
                transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
                print("RotationPost: " + transform.eulerAngles);
                print("flipped rightSideUp");
                flipped = false;
            }
            else
            {
                Vector3 rot = new Vector3(180, 0, 0);
                print("Rotation: " + rot);
                transform.position = new Vector3(transform.position.x, transform.position.y + transform.localScale.y / 2, 0);
                transform.localEulerAngles = new Vector3(180, transform.localEulerAngles.y, 0);
                print("RotationPost: " + transform.eulerAngles);
                print("flipped upSideDown");
                flipped = true;
            }
            cam.flipped = flipped;
            rb.velocityY = rb.velocityY / 2;
            rb.gravityScale *= -1;
            canFlip = false;
        }
    }


    [SerializeField] float maxSpeed;
    [SerializeField] float timeManip;
    void TimeManip()
    {
        if (input.actions["SpeedUp"].IsPressed())
        {
            if (gameManager.timeScale < maxSpeed)
            {
                gameManager.timeScale += timeManip;
            }
        }
        if (input.actions["SlowDown"].IsPressed())
        {
            if (gameManager.timeScale > 0)
            {
                gameManager.timeScale -= timeManip;
            }
        }
    }
}
