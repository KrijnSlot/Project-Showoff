using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public bool checkpointActivated;

    [SerializeField] public GameObject respawnPoint;
    private GameObject player;
    private Animator animator;
    private ParticleSystem pSystem;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        pSystem = GetComponentInChildren<ParticleSystem>();
    }

    private void FixedUpdate()
    {
        if (player != null)
        {
            player = GameObject.FindGameObjectWithTag("Player1");
        }
        DeativatedOldCheckpoint();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player1") && !checkpointActivated)
        {
            pSystem.Play();
            animator.SetBool("Active", true);
            checkpointActivated = true;
            Die.SetLastCheckpoint(this);

            
        }
    }

    void DeativatedOldCheckpoint()
    {
        if (Die.GetLastCheckpoint() != this)
        {
            animator.SetBool("Active", false);
            checkpointActivated = false;
        }
    }
}
