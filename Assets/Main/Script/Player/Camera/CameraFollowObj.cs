using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraFollowObj : MonoBehaviour
{
    [SerializeField] float yDamp;
    [SerializeField] float rotationTime;
    [SerializeField] GameObject player;
    [SerializeField] float yLerpSpeed = 2;
    [SerializeField] float camOffsetScalerX;
    [SerializeField] float camOffsetScalerY;
    Rigidbody2D rb;
    float rotateTimer;

    float curOffset = 1;

    Coroutine _turnCoroutine;

    [SerializeField] CinemachineFollow follow;
    [SerializeField] CinemachineCamera cam;

    bool _facingRight = true;

    bool turn = false;

    float yDampening;
    float dampTimer = 0;
    public bool flipped = false;

    private PlayerInput playerInput;

    public enum CameraLockStates
    {
        free,
        yLock,
        xLock
    }

    [SerializeField] CameraLockStates lockState;

    float offset;

    private void Awake()
    {
        rb = player.GetComponent<Rigidbody2D>();
        playerInput = transform.parent.GetComponentInChildren<PlayerInput>();
    }
    // Start is called before the first frame update

    private void Update()
    {
        if (lockState == CameraLockStates.free)
        {
            transform.position = player.transform.position;
        }
        Turn();
        MoveCam();

        if (lockState != CameraLockStates.yLock)
        {
            if (!flipped) JumpCamNorm();
            else JumpCamFlipped();
        }
    }

    void Turn()
    {
        if (turn)
        {
            float endOffset = 1;
            if (_facingRight)
            {
                rotateTimer += Time.deltaTime;
                follow.FollowOffset.x = Mathf.Lerp(curOffset, endOffset, (rotateTimer / rotationTime));
            }
            else
            {
                rotateTimer += Time.deltaTime;
                endOffset = -1;
                follow.FollowOffset.x = Mathf.Lerp(curOffset, endOffset, (rotateTimer / rotationTime));
            }
            curOffset = follow.FollowOffset.x;
            if (follow.FollowOffset.x == endOffset) { turn = false; }
        }
    }

    void MoveCam()
    {
        Vector2 offset = playerInput.actions["Look"].ReadValue<Vector2>();
        if (offset.x != 0)
        {
            follow.FollowOffset.x = curOffset + offset.x* camOffsetScalerX;
        }
        else
        {
            follow.FollowOffset.x = curOffset;
        }
        if (offset.y != 0)
        {
            follow.FollowOffset.y =  0 + offset.y * camOffsetScalerY;
        }
        else
        {
            follow.FollowOffset.y = 0;
        }
    }

    void JumpCamNorm()
    {
        if (rb.velocityY < 0)
        {
            dampTimer += Time.deltaTime;
            follow.TrackerSettings.PositionDamping.y = Mathf.Lerp(follow.TrackerSettings.PositionDamping.y, 0, (dampTimer / yLerpSpeed));
        }
        else
        {
            dampTimer = 0;
            follow.TrackerSettings.PositionDamping.y = yDamp;
        }

    }
    void JumpCamFlipped()
    {
        if (rb.velocityY > 0)
        {
            dampTimer += Time.deltaTime;
            follow.TrackerSettings.PositionDamping.y = Mathf.Lerp(follow.TrackerSettings.PositionDamping.y, 0, (dampTimer / yLerpSpeed));
        }
        else
        {
            dampTimer = 0;
            follow.TrackerSettings.PositionDamping.y = yDamp;
        }
    }

    public void CallTurn(bool PfacingRight)
    {
        //_turnCoroutine = StartCoroutine(FlipYLerp());
        turn = true;
        rotateTimer = 0;
        _facingRight = PfacingRight;
        //LeanTween.rotateY(gameObject, EndRotation(), rotationTime).setEaseInOutSine();

    }

    public void Lock(CameraLockStates Pstate, float Poffset)
    {
        lockState = Pstate;
        offset = Poffset;
    }
}
