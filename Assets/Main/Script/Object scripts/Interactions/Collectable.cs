using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Collectable : MonoBehaviour
{
    public static event Action AddPage;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player1") || other.CompareTag("Player2"))
        {
            if(this.tag == "MusicSheet")
            {
                AddPage?.Invoke();
            }
            Debug.Log(other.gameObject.name);
            Destroy(this.gameObject);
        }
    }

}
