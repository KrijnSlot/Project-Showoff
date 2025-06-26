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
        GetComponentInChildren<CinemachineCamera>().enabled = false;
        GetComponentInChildren<PlayerMovement>().enabled = false;
    }
}
