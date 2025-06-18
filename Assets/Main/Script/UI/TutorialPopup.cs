using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TutorialPopup : MonoBehaviour
{
    [SerializeField] GameObject tutorialImage;
    SpriteRenderer SpriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer = tutorialImage.GetComponent<SpriteRenderer>();
        SpriteRenderer.enabled = false;
    }

    private void OnTriggerEnter2D()
    {
        SpriteRenderer.enabled = true;
        Debug.Log("ello");
    }

    private void OnTriggerExit2D()
    {
        SpriteRenderer.enabled = false;
    }
}

