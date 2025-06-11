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
    [SerializeField] GameObject SplitscreenDevision;
    [SerializeField] GameObject spawnButton;
    PlayerPowers power;

    

    [SerializeField] PlayerPowers.Powers player1Power;
    [SerializeField] PlayerPowers.Powers player2Power;

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
            SplitscreenDevision.SetActive(true);
            spawnButton.SetActive(false);
        }
        else
        {
            SplitscreenDevision.SetActive(false);
            spawnButton.SetActive(true);
        }

    }
}
