using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class Restart_game : MonoBehaviour
{
    private VideoPlayer vid;
    private void Awake()
    {
        vid = gameObject.GetComponent<VideoPlayer>();
    }
    private void OnEnable()
    {
        vid.loopPointReached += OnVideoEnd;
    }

    private void OnDisable()
    {
        vid.loopPointReached -= OnVideoEnd;
    }

    void OnVideoEnd(VideoPlayer play)
    {
        SceneManager.LoadScene("Menu");
    }
}
