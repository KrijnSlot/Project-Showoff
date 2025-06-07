using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointP2 : MonoBehaviour
{
    public bool checkpointActivated;

    [SerializeField] public GameObject respawnPoint;
    private GameObject player;

    private void FixedUpdate()
    {
        if (player != null)
        {
            player = GameObject.FindGameObjectWithTag("Player2");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player2"))
        {
            checkpointActivated = true;
            Die.SetLastCheckpointP2(this);
        }
    }
}
