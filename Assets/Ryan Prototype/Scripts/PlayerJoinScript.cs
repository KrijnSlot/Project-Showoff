using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJoinScript : MonoBehaviour
{
    [SerializeField] int index = 0;
    [SerializeField] List<Transform> spawn = new List<Transform>();
    [SerializeField] List<GameObject> prefab = new List<GameObject>();
    PlayerInputManager playerInputManager;
    PlayerPowers power;
    enum Powers
    {
        gravityManip,
        timeManip,
        sizeManip,
        astralProject
    };

    [SerializeField] Powers player1Power;
    [SerializeField] Powers player2Power;

    private void Awake()
    {
        //this.gameObject.transform.parent = spawn

        playerInputManager = GetComponent<PlayerInputManager>();


        if (playerInputManager.playerCount == 0)
        {
            index += 1;
            playerInputManager.playerPrefab = prefab[0];
            power = prefab[0].GetComponentInChildren<PlayerPowers>();
            prefab[0].GetComponentInChildren<PlayerMovement>().spawnPoint = spawn[0];

            switch (player1Power)
            {
                case Powers.gravityManip: power.currentPower = PlayerPowers.Powers.gravityManip; break;
                case Powers.timeManip: power.currentPower = PlayerPowers.Powers.timeManip; break;
                case Powers.sizeManip: power.currentPower = PlayerPowers.Powers.sizeManip; break;
                case Powers.astralProject: power.currentPower = PlayerPowers.Powers.astralProject; break;
            }
        }
        else
        {
            playerInputManager.playerPrefab = prefab[1];
            power = prefab[1].GetComponentInChildren<PlayerPowers>();
            prefab[1].GetComponentInChildren<PlayerMovement>().spawnPoint = spawn[1];
            switch (player2Power)
            {
                case Powers.gravityManip: power.currentPower = PlayerPowers.Powers.gravityManip; break;
                case Powers.timeManip: power.currentPower = PlayerPowers.Powers.timeManip; break;
                case Powers.sizeManip: power.currentPower = PlayerPowers.Powers.sizeManip; break;
                case Powers.astralProject: power.currentPower = PlayerPowers.Powers.astralProject; break;
            }
        }
    }

    public void SwitchNextSpawnCharacter(PlayerInput input)
    {
        if (playerInputManager.playerCount == 0)
        {
            
            playerInputManager.playerPrefab = prefab[0];
            power = prefab[0].GetComponentInChildren<PlayerPowers>();
            prefab[0].GetComponentInChildren<PlayerMovement>().spawnPoint = spawn[0];
            switch (player1Power)
            {
                case Powers.gravityManip: power.currentPower = PlayerPowers.Powers.gravityManip; break;
                case Powers.timeManip: power.currentPower = PlayerPowers.Powers.timeManip; break;
                case Powers.sizeManip: power.currentPower = PlayerPowers.Powers.sizeManip; break;
                case Powers.astralProject: power.currentPower = PlayerPowers.Powers.astralProject; break;
            }
        }
        else
        {
            playerInputManager.playerPrefab = prefab[1];
            power = prefab[1].GetComponentInChildren<PlayerPowers>();
            prefab[1].GetComponentInChildren<PlayerMovement>().spawnPoint = spawn[1];
            switch (player2Power)
            {
                case Powers.gravityManip: power.currentPower = PlayerPowers.Powers.gravityManip; break;
                case Powers.timeManip: power.currentPower = PlayerPowers.Powers.timeManip; break;
                case Powers.sizeManip: power.currentPower = PlayerPowers.Powers.sizeManip; break;
                case Powers.astralProject: power.currentPower = PlayerPowers.Powers.astralProject; break;
            }
        }
    }
}
