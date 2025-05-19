using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class CameraFollowObj : MonoBehaviour
{
    [SerializeField] Transform playerPos;
    [SerializeField] float rotationTime;
    float rotateTimer;

    float curOffset;

    Coroutine _turnCoroutine;

    private PlayerMovement player;
    [SerializeField] CinemachineFollow follow;

    bool _facingRight;

    bool turn = false;
    // Start is called before the first frame update
    void Awake()
    {
        player = playerPos.gameObject.GetComponent<PlayerMovement>();
        _facingRight = player.facingRight;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = playerPos.position;

    }

    private void FixedUpdate()
    {
        if (turn)
        {  
            float endOffset = -1;
            if (_facingRight)
            {
                rotateTimer += Time.deltaTime;
                follow.FollowOffset.x = Mathf.Lerp(curOffset, endOffset, (rotateTimer / rotationTime));
            }
            else
            {
                rotateTimer += Time.deltaTime;
                endOffset = 1;
                follow.FollowOffset.x = Mathf.Lerp(curOffset, endOffset, (rotateTimer / rotationTime));
            }
            curOffset = follow.FollowOffset.x;
            if(follow.FollowOffset.x == endOffset) { turn = false; }
        }
    }

    public void OnJump()
    {

    }

    public void CallTurn(bool PfacingRight)
    {
        //_turnCoroutine = StartCoroutine(FlipYLerp());
        turn = true;
        rotateTimer = 0;
        _facingRight = PfacingRight;
        //LeanTween.rotateY(gameObject, EndRotation(), rotationTime).setEaseInOutSine();

    }   
}
