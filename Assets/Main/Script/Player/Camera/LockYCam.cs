using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class LockYCam : MonoBehaviour
{
    CinemachineConfiner2D confine;
    GameManager gameManager;
    [SerializeField] int playerNumb;

    private void Awake()
    {
            confine = gameObject.GetComponent<CinemachineConfiner2D>();
            gameManager = GameManager.Instance;
        if (gameManager.cameraBounds.Length > 0)
        {
            confine.BoundingShape2D = gameManager.cameraBounds[playerNumb - 1].GetComponent<BoxCollider2D>();
        }
    }

    /*[SerializeField] CameraFollowObj followObj = null;
    [SerializeField] float offset = 2f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player1") || collision.CompareTag("Player2"))
        {
            followObj = collision.transform.parent.GetComponentInChildren<CameraFollowObj>();
            if (followObj) followObj.Lock(CameraFollowObj.CameraLockStates.yLock, transform.position.y + offset);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)

    {
        if (followObj) followObj.Lock(CameraFollowObj.CameraLockStates.free, 0);
    }*/



}
