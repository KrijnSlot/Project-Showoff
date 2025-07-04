using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Restart_game : MonoBehaviour
{
    private VideoPlayer vid;
    private void Awake()
    {
        vid = gameObject.GetComponent<VideoPlayer>();


    }
}
