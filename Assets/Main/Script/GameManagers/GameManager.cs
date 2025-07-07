using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance { get { return _instance; } }

    public GameObject[] cameraBounds;


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public float timeScale;

    private void Update()
    {
        if (timeScale < 0) timeScale = 0;

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("Menu");
        }
    }
}
