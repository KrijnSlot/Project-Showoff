using System;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJoinScript : MonoBehaviour
{
    [SerializeField] int index = 0;
    [SerializeField] List<Transform> spawn = new List<Transform>();
    [SerializeField] List<GameObject> prefab = new List<GameObject>();
    PlayerInputManager playerInputManager;
    [SerializeField] GameObject SplitscreenDevision;
    [SerializeField] GameObject spawnButton;
    [SerializeField] GameObject p1SpawnButton;
    PlayerPowers power;
    //[SerializeField] Animator anim;

    

    [SerializeField] PlayerPowers.Powers player1Power;
    [SerializeField] PlayerPowers.Powers player2Power;

    public static event Action secondPlayer;

    private void Awake()
    {
        //this.gameObject.transform.parent = spawn
        //anim.SetBool("start", true);
        playerInputManager = GetComponent<PlayerInputManager>();

        if (playerInputManager.playerCount == 0)
        {
            index += 1;
            playerInputManager.playerPrefab = prefab[0];
            power = prefab[0].GetComponentInChildren<PlayerPowers>();
            prefab[0].GetComponentInChildren<PlayerMovement>().spawnPoint = spawn[0];
            prefab[0].GetComponentInChildren<CinemachineCamera>().enabled = false;
            prefab[0].GetComponentInChildren<PlayerMovement>().enabled = false;

            power.currentPower = player1Power;
        }
        else
        {
            playerInputManager.playerPrefab = prefab[1];
            power = prefab[1].GetComponentInChildren<PlayerPowers>();
            prefab[1].GetComponentInChildren<PlayerMovement>().spawnPoint = spawn[1];
            power.currentPower = player2Power;

        }
    }

    public void SwitchNextSpawnCharacter(PlayerInput input)
    {
        if (playerInputManager.playerCount == 0)
        {

            playerInputManager.playerPrefab = prefab[0];
            power = prefab[0].GetComponentInChildren<PlayerPowers>();
            prefab[0].GetComponentInChildren<PlayerMovement>().spawnPoint = spawn[0];
            prefab[0].GetComponentInChildren<CinemachineCamera>().enabled = false;
            prefab[0].GetComponentInChildren<PlayerMovement>().enabled = false;

            power.currentPower = player1Power;

        }
        else
        {
            playerInputManager.playerPrefab = prefab[1];
            power = prefab[1].GetComponentInChildren<PlayerPowers>();
            prefab[1].GetComponentInChildren<PlayerMovement>().spawnPoint = spawn[1];

            power.currentPower = player2Power;

        }
        if (playerInputManager.playerCount > 1)
        {
            secondPlayer?.Invoke();
            SplitscreenDevision.SetActive(true);
            spawnButton.SetActive(false);
        }
        else if (playerInputManager.playerCount > 0)
        {
            p1SpawnButton.SetActive(false);
        }
        else
        {

            SplitscreenDevision.SetActive(false);
            spawnButton.SetActive(true);
        }

    }
}
