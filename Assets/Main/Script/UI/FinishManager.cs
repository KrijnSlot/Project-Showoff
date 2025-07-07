using System.Collections;
using System.Collections.Generic;
using Script;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishManager : MonoBehaviour
{
    [SerializeField] private LevelEndUI levelEndUI;

    public bool player1Finished;
    public bool player2Finished;

    private void Update()
    {
        if(player1Finished && player2Finished)
        {
            SceneManager.LoadScene("FinalScene");
        }
    }
}
