using UnityEngine;

namespace Script
{
    public class LevelFinish : MonoBehaviour
    {
        [SerializeField] private LevelEndUI levelEndUI;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag("Player1") && !collision.CompareTag("Player2")) return;
            Debug.Log("Level Finished");
            ShowLevelEndUI();
        }
        
        private void ShowLevelEndUI()
        {
            if (levelEndUI != null)
            {
                levelEndUI.gameObject.SetActive(true);
            }
        }
    }
}
