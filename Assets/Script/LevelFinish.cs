using UnityEngine;

namespace Script
{
    public class LevelFinish : MonoBehaviour
    {
        [SerializeField] private LevelEndUI levelEndUI;
        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.CompareTag("Player1") || collider.CompareTag("Player2"))
            {
                Debug.Log("Level Finished");
                ShowLevelEndUI();
            }
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
