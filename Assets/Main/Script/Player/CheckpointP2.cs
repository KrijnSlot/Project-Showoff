using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointP2 : MonoBehaviour
{
    public bool checkpointActivated;

    [SerializeField] public GameObject respawnPoint;
    private GameObject player;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
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
