using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class OnSecondPlayerSpawn : MonoBehaviour
{
    private void OnEnable()
    {
        PlayerJoinScript.secondPlayer += ActivatePlayer;
    }

    private void OnDisable()
    {
        PlayerJoinScript.secondPlayer -= ActivatePlayer;
    }
    
    void ActivatePlayer()
    {
        GetComponentInChildren<Camera>().enabled = true;
        GetComponentInChildren<CinemachineCamera>().enabled = true;
        GetComponentInChildren<PlayerMovement>().enabled = true;
    }
}
