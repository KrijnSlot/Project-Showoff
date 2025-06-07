using UnityEngine;

public class Die : MonoBehaviour
{
    public static Checkpoint lastCheckpoint;
    public static CheckpointP2 lastCheckpointP2;

    public static void SetLastCheckpoint(Checkpoint newCheckpoint)
    {
        lastCheckpoint = newCheckpoint;
    }

    public static void SetLastCheckpointP2(CheckpointP2 newCheckpointP2)
    {
        lastCheckpointP2 = newCheckpointP2;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player1") && !collision.CompareTag("Player2")) return;

        if (collision.CompareTag("Player1"))
        {
        if (lastCheckpoint != null && lastCheckpoint.respawnPoint != null)
        {
            collision.transform.position = lastCheckpoint.respawnPoint.transform.position;

            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = Vector2.zero;
                rb.angularVelocity = 0f;
            }
        }
        else
        {
            Debug.LogWarning("No checkpoint set for P1 — can't respawn.");
        }
        }

        if (collision.CompareTag("Player2"))
        {
            if (lastCheckpointP2 != null && lastCheckpointP2.respawnPoint != null)
            {
                collision.transform.position = lastCheckpointP2.respawnPoint.transform.position;

                Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.velocity = Vector2.zero;
                    rb.angularVelocity = 0f;
                }
            }
            else
            {
                Debug.LogWarning("No checkpoint set for P2 — can't respawn.");
            }
        }
    }
}
