using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCollision : MonoBehaviour
{
    [SerializeField] Transform platform;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player1") || other.gameObject.CompareTag("Player2"))
        {
            other.gameObject.transform.parent.transform.parent = platform;
            Debug.Log("ge");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player1") || other.gameObject.CompareTag("Player2"))
        {
            other.gameObject.transform.parent.transform.parent = null;
        }
    }
}
