using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class InteractableObject : MonoBehaviour, IInteractable
{
    [SerializeField]
    GameObject[] obstacle;

    [SerializeField] private float checkForPlayerRange;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private GameObject popUpSprite;
    private bool playerInRange;
    private bool popUpOn;

    void Start()
    {
        popUpSprite.gameObject.gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
       /* Debug.Log("player is in range " + playerInRange);*/

        playerInRange = Physics2D.OverlapCircle(this.gameObject.transform.position, checkForPlayerRange, playerLayer);
        if (playerInRange)
        {
            popUpOn = true;
            popUpSprite.gameObject.SetActive(true);
        }
        else if (!playerInRange && popUpOn)
        {
            popUpOn = false;
            popUpSprite.gameObject.SetActive(false);
        }
    }

    public void Interact()
    {
        /*Debug.Log("tag: " + tag);*/
        print("interacting");
        switch (gameObject.tag)
        {
            case "Interact":
                Debug.Log("it's colliding");
                GetComponent<HandleDoorInteraction>().InteractDoorHandle();
                break;
            case "MusicStand":
                print("is colliding");
                GetComponent<MusicStand>().Activate();
                break;
            case "ActivateMovingPlatform":
                GetComponent<FlipSwitch>().Flip();
                foreach (GameObject obst in obstacle)
                {
                    obst.GetComponent<MovingPlatformScript>().enabled = !obst.GetComponent<MovingPlatformScript>().enabled;
                }
                break;
            case "ActivateRotatingPlatform":
                GetComponent<FlipSwitch>().Flip();
                foreach (GameObject obst in obstacle)
                {
                    obst.GetComponent<RotatingPlatforms>().Rotate();
                }
                break;

        }
    }
}
