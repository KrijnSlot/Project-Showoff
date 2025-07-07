using UnityEngine;

namespace Script
{
    public class LevelFinish : MonoBehaviour
    {
        [SerializeField] private FinishManager finishManager;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag("Player1") || !collision.CompareTag("Player2")) return;

            if (collision.CompareTag("Player1"))
            {
                finishManager.player1Finished = true;
            }
            else if (collision.CompareTag("Player2"))
            {
                finishManager.player2Finished = true;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (!collision.CompareTag("Player1") || !collision.CompareTag("Player2")) return;

            if (collision.CompareTag("Player1"))
            {
                finishManager.player1Finished = false;
            }
            else if (collision.CompareTag("Player2"))
            {
                finishManager.player2Finished = false;
            }
        }

    }
}
