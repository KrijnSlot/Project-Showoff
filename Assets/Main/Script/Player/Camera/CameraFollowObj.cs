using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class CameraFollowObj : MonoBehaviour
{
    [SerializeField] float yDamp;
    [SerializeField] float rotationTime;
    [SerializeField] GameObject player;
    [SerializeField] float yLerpSpeed = 2;
    Rigidbody2D rb;
    float rotateTimer;

    float curOffset;

    Coroutine _turnCoroutine;

    [SerializeField] CinemachineFollow follow;
    [SerializeField] CinemachineCamera cam;

    bool _facingRight = true;

    bool turn = false;

    float yDampening;
    float dampTimer = 0;
    public bool flipped = false;

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
    }
    // Start is called before the first frame update

    private void Update()
    {
        if(lockState == CameraLockStates.free)
        {
            transform.position = player.transform.position;
        }
        else if(lockState == CameraLockStates.yLock)
        {
            transform.position = new Vector3(player.transform.position.x, offset, player.transform.position.z);
        }
            Turn();

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
