using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace Script
{
    public class LevelEndUI : MonoBehaviour
    {
        public void ResetLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
