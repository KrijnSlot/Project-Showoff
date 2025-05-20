using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Die : MonoBehaviour
{
private void OnTriggerEnter2D(Collider2D collision)
{
    Debug.Log("GWW");
    if (!collision.CompareTag("Player1") && !collision.CompareTag("Player2")) return;
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
}
}
