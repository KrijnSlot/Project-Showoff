using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class disableStartCam : MonoBehaviour
{
    private void OnEnable()
    {
        PlayerJoinScript.secondPlayer += DisableCam;
    }

    private void OnDisable()
    {
        PlayerJoinScript.secondPlayer -= DisableCam;
    }

    void DisableCam()
    {
        GetComponent<CinemachineCamera>().enabled = false;
        gameObject.SetActive(false);
    }
}
