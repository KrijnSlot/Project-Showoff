using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class LockYCam : MonoBehaviour
{
    [SerializeField] CameraFollowObj followObj = null;
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
    }

}
