using UnityEngine;

public class Die : MonoBehaviour
{
    public static Checkpoint lastCheckpoint;

    public static void SetLastCheckpoint(Checkpoint newCheckpoint)
    {
        lastCheckpoint = newCheckpoint;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player1") && !collision.CompareTag("Player2")) return;

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
            Debug.LogWarning("No checkpoint set — can't respawn.");
        }
    }
}
