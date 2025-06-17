using System;
using System.Globalization;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPowers : MonoBehaviour
{
    Rigidbody2D rb;
    PlayerInput input;
    PlayerMovement movementScript;
    CameraFollowObj cam;
    [SerializeField] GameManager gameManager;

    public static event Action swapReality;

    private Animator animator;

    public Vector3 pScale;

    public enum Powers
    {
        gravityManip,
        timeManip,
        sizeManip,
        astralProject,
        realityManip,
        song
    };
    public Powers currentPower = Powers.gravityManip;

    bool sizaManipOn;
    bool timeManipOn;
    bool songOn;
    private void Start()
    {
        gameManager = GameManager.Instance;
        input = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        movementScript = GetComponent<PlayerMovement>();
        cam = transform.parent.GetComponentInChildren<CameraFollowObj>();
        input.actions["Grow"].performed += SizeManipCycle;
        if (currentPower == Powers.song) songOn = true;
    }

    private void OnEnable()
    {
        Die.died += ResetPowers;
    }
    private void OnDisable()
    {
        Die.died -= ResetPowers;
    }

    private void ResetPowers(int playerNumb)
    {
        if ((playerNumb == 1 && gameObject.tag == "Player1") || playerNumb == 2 && gameObject.tag == "Player2")
        {
            switch (currentPower)
            {
                case Powers.gravityManip:flipped = true; canFlip = true; Flip() ; break;
                case Powers.sizeManip: sizaManipOn = true; currentSize = PlayerSizes.normal; break;
                case Powers.astralProject: isProjecting = true; AstralProjection(); break;

            }
        }
    }

    public void UsePower(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            switch (currentPower)
            {
                case Powers.gravityManip: Flip(); break;
                case Powers.timeManip: timeManipOn = !timeManipOn; gameManager.timeScale = 1; break;
                case Powers.sizeManip: sizaManipOn = true; nextSize = true; break;
                case Powers.astralProject: AstralProjection(); break;
                case Powers.realityManip: swapReality?.Invoke(); break;
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                
            }

        }
    }

    private void FixedUpdate()
    {
        CheckProjectionDistance();

        if (sizaManipOn)
            SizeManipulation();
        if (timeManipOn)
            TimeManip();
        if (songOn) Song();

        if (currentPower == Powers.sizeManip && currentSize == PlayerSizes.big)
        {
            Vector2 rayDir = Vector2.down * Mathf.Sign(rb.gravityScale);
            RaycastHit2D breakableCheck = Physics2D.Raycast(transform.position, rayDir, transform.localScale.y * 5, mask);

            if (breakableCheck)
            {
                print("checkPassed");
                if (breakableCheck.collider.gameObject.tag == "Breakable")
                {
                    print("secondCheckPassed");
                    if (hitObj != breakableCheck.collider.gameObject)
                    {
                        if (hitUse != null)
                        {
                            hitUse.DeActivate();
                        }
                        hitObj = breakableCheck.collider.gameObject;
                        hitUse = breakableCheck.collider.gameObject.GetComponent<UseAble>();
                    }
                    else
                    {
                        hitUse.Activate();
                    }

                }
                else DeActivateBreakable();
            }
            else DeActivateBreakable();
        }
        else DeActivateBreakable();
    }

    void DeActivateBreakable()
    {
        if (hitUse != null)
        {
            print("rahh");
            hitObj = null;
            hitUse.DeActivate();
            hitUse = null;
        }
    }


    [Header("AstralProject")]
    [SerializeField] GameObject playerObj;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] float maxProjectionDistance;
    private bool isProjecting = false;
    private float projectionCooldown = 0.5f;
    private float lastProjectionTime = -Mathf.Infinity;
    private Vector2 bodyPos;

    [SerializeField] private string projectionLayerName = "AstralBody";
    private int originalLayer;

    void SetOpacity(GameObject obj, float alpha)
    {
        Renderer renderer = obj.GetComponent<Renderer>();
        if (renderer != null)
        {
            Material mat = renderer.material;
            Color color = mat.color;
            color.a = alpha;
            mat.color = color;

            mat.SetFloat("_Mode", 3);
            mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            mat.SetInt("_ZWrite", 0);
            mat.DisableKeyword("_ALPHATEST_ON");
            mat.EnableKeyword("_ALPHABLEND_ON");
            mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            mat.renderQueue = 3000;
        }
    }

    Transform originalParent;
    void AstralProjection()
    {
        if (Time.time - lastProjectionTime < projectionCooldown)
            return;

        lastProjectionTime = Time.time;

        if (!isProjecting)
        {
            Debug.Log("Astral projection started");

            SetOpacity(this.gameObject, 0.5f);
            bodyPos = this.gameObject.transform.position;
            playerObj.transform.position = bodyPos;

            originalLayer = this.gameObject.layer;
            this.gameObject.layer = LayerMask.NameToLayer(projectionLayerName);

            originalParent = playerObj.transform.parent;
            playerObj.transform.parent = null;

            playerObj.SetActive(true);
            isProjecting = true;
        }
        else
        {
            Debug.Log("Astral projection ended");

            SetOpacity(this.gameObject, 1f);
            this.gameObject.transform.position = playerObj.transform.position;

            this.gameObject.layer = originalLayer;

            playerObj.SetActive(false);
            playerObj.transform.parent = originalParent;

            isProjecting = false;
        }
    }

    void CheckProjectionDistance()
    {
        if (!isProjecting) { return; }

        float projectionDistance = Vector2.Distance(playerObj.transform.position, this.transform.position);
        if (projectionDistance > maxProjectionDistance)
        {
            Debug.Log("Astral projection ended");

            SetOpacity(this.gameObject, 1f);
            this.gameObject.transform.position = playerObj.transform.position;

            this.gameObject.layer = originalLayer;

            playerObj.SetActive(false);
            playerObj.transform.parent = originalParent;

            isProjecting = false;
        }
    }


    [Header("Player Resizing Power")]

    [SerializeField] private float maxSizeCap = 5f;
    [SerializeField] private float minSizeCap = 0.25f;
    public int sizeCycle = 1;
    [SerializeField] float bigSize, normalSize, smallSize, scaleSpeed;
    [SerializeField][Tooltip("Checks the height above the player, to see if its big enough to grow")] float growthHeightCheck;
    [SerializeField] LayerMask mask;
    bool nextSize = false;
    GameObject hitObj;
    UseAble hitUse;

    public enum PlayerSizes
    {
        normal,
        big,
        small
    };

    public PlayerSizes currentSize = PlayerSizes.normal;

    public void SizeManipulation()
    {

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, growthHeightCheck, mask);
        //print(hit.collider);
        Vector3 pScale = transform.localScale;
        float scaleSpd = scaleSpeed;

        switch (currentSize)
        {
            case PlayerSizes.normal:
                if (nextSize) { currentSize = PlayerSizes.big; nextSize = false; break; }
                if (pScale.x <= normalSize - 0.01f) { pScale += new Vector3(scaleSpd, scaleSpd, 0); }
                else if (pScale.x >= normalSize + 0.01f) { pScale -= new Vector3(scaleSpd, scaleSpd, 0); }
                else if (pScale.x <= normalSize + 0.01f && pScale.x >= normalSize - 0.01f) { sizaManipOn = false; }
                break;
            case PlayerSizes.big:
                if (nextSize) { currentSize = PlayerSizes.small; nextSize = false; break; }
                if (pScale.x <= bigSize - 0.01f && !hit) { pScale += new Vector3(scaleSpd, scaleSpd, 0); }
                else if (hit && pScale.x <= bigSize - 0.5) { currentSize--; }
                else if (pScale.x >= bigSize - 0.01f) { sizaManipOn = false; }
                break;
            case PlayerSizes.small:
                if (nextSize) { currentSize = PlayerSizes.normal; nextSize = false; break; }
                if (pScale.x >= smallSize + 0.01f) { print("smoll"); pScale -= new Vector3(scaleSpd, scaleSpd, 0); }
                else if (pScale.x <= smallSize + 0.01f) { sizaManipOn = false; }
                break;
        }





        transform.localScale = pScale;
    }

    public void SizeManipCycle(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("button press is working atleast..");
            sizeCycle++;
        }
    }

    [Header("Flip")]
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
                transform.localEulerAngles = movementScript.facingRight ? new Vector3(0f, 0f, 0) : new Vector3(0f, 180f, 0);
                print("RotationPost: " + transform.eulerAngles);
                print("flipped rightSideUp");
                flipped = false;
            }
            else
            {
                Vector3 rot = new Vector3(180, 0, 0);
                print("Rotation: " + rot);
                transform.position = new Vector3(transform.position.x, transform.position.y + transform.localScale.y / 2, 0);
                transform.localEulerAngles = movementScript.facingRight ? new Vector3(180f, 0f, 0) : new Vector3(180f, 180f, 0);
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

    [Header("TimeManip")]
    [SerializeField] float maxSpeed;
    [SerializeField] float timeManip;
    void TimeManip()
    {
        if (input.actions["SpeedUp"].IsPressed())
        {
            print("sped");
            if (gameManager.timeScale < maxSpeed)
            {
                gameManager.timeScale += timeManip;
            }
            else gameManager.timeScale = maxSpeed;
        }
        if (input.actions["SlowDown"].IsPressed())
        {
            if (gameManager.timeScale > 0)
            {
                gameManager.timeScale -= timeManip;
            }
            else gameManager.timeScale = 0;
        }
    }

    [Header("Song")]
    public bool canDoubleJump;
    [SerializeField] bool canReset;
    [SerializeField] GameObject jumpNote;

    void Song()
    {
        if (!canDoubleJump && canReset)
        {

            animator.SetBool("isSinging", true);
            print("On");
            jumpNote.GetComponent<SpriteRenderer>().enabled = true;
            jumpNote.GetComponent<NoteTimer>().Activate();
            canReset = false;
        }
        if (canDoubleJump)
        {
            canReset = true;
            animator.SetBool("isSinging", false);
        }
    }
}
