using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJoinScript : MonoBehaviour
{
    int index = 0;
    [SerializeField] List<Transform> spawn = new List<Transform>();
    [SerializeField] List<GameObject> prefab = new List<GameObject>();
    PlayerInputManager playerInputManager;

    private void Awake()
    {
        playerInputManager = GetComponent<PlayerInputManager>();
        if (playerInputManager.playerCount == 0)
        {
            playerInputManager.playerPrefab = prefab[0];
        }
        else
        {
            playerInputManager.playerPrefab = prefab[1];
        }
    }

    public void SwitchNextSpawnCharacter(PlayerInput input)
    {
        if (playerInputManager.playerCount == 0)
        {
            playerInputManager.playerPrefab = prefab[0];
        }
        else
        {
            playerInputManager.playerPrefab = prefab[1];
        }
    }
}
