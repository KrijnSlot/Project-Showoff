using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TutorialPopup : MonoBehaviour
{
    [SerializeField] GameObject tutorialImage;
    [SerializeField] GameObject tutorialIndicator;
    SpriteRenderer SpriteRenderer;
    SpriteRenderer tutorialIndicatorSprite;

    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer = tutorialImage.GetComponent<SpriteRenderer>();
        tutorialIndicatorSprite = tutorialIndicator.GetComponent<SpriteRenderer>();
        SpriteRenderer.enabled = false;
    }

    private void OnTriggerEnter2D()
    {
        SpriteRenderer.enabled = true;
        tutorialIndicatorSprite.enabled = false;
        Debug.Log("ello");
    }

    private void OnTriggerExit2D()
    {
        SpriteRenderer.enabled = false;
        tutorialIndicatorSprite.enabled = true;
    }
}

