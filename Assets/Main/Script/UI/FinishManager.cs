using System.Collections;
using System.Collections.Generic;
using Script;
using UnityEngine;

public class FinishManager : MonoBehaviour
{
    [SerializeField] private LevelEndUI levelEndUI;

    public bool player1Finished;
    public bool player2Finished;

    private void Update()
    {
        if(player1Finished && player2Finished)
        {
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
