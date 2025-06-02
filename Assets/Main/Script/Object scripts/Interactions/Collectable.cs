using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Collectable : MonoBehaviour
{
    public static event Action AddPage;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
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
