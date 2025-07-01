using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointP2 : MonoBehaviour
{
    public bool checkpointActivated;

    [SerializeField] public GameObject respawnPoint;
    private GameObject player;
    private Animator animator;
    private ParticleSystem pSystem;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        pSystem = GetComponent<ParticleSystem>();
    }

    private void FixedUpdate()
    {
        if (player != null)
        {
            player = GameObject.FindGameObjectWithTag("Player2");
        }
        DeativatedOldCheckpoint();

        Debug.Log(animator.gameObject.name + " status");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player2"))
        {
            pSystem.Play();
            animator.SetBool("Active", true);
            checkpointActivated = true;
            Die.SetLastCheckpointP2(this);
        }
    }
    void DeativatedOldCheckpoint()
    {
        if (Die.GetLastCheckpointP2() != this)
        {
            animator.SetBool("Active", false);
            checkpointActivated = false;
        }
    }
}
