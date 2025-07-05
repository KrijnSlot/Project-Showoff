using System.ComponentModel;
using UnityEngine;

public class InteractableObject : MonoBehaviour, IInteractable
{
    [SerializeField] private float checkForPlayerRange;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private GameObject popUpSprite;
    [SerializeField] private GameObject nextPopUpSprite;
    [SerializeField] private AudioClip leverSound;

    private bool playerInRange;
    private bool popUpOn;

    private Animator leverAnim;
    private MusicStand musicStand;

    private void Awake()
    {
        leverAnim = GetComponent<Animator>();
        musicStand = GetComponent<MusicStand>();
    }

    private void Start()
    {
        popUpSprite.gameObject.gameObject.SetActive(false);
        if (nextPopUpSprite != null)
        {
        nextPopUpSprite.gameObject.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        PopUpBehavior();
    }

    void PopUpBehavior()
    {
        playerInRange = Physics2D.OverlapCircle(transform.position, checkForPlayerRange, playerLayer);

        if (playerInRange)
        {
            popUpOn = true;
            popUpSprite.SetActive(true);
        }
        else if (!playerInRange && popUpOn)
        {
            popUpOn = false;
            popUpSprite.SetActive(false);
        }

        if (musicStand != null && musicStand.gotAllPages && playerInRange)
        {
            popUpOn = false;
            popUpSprite.gameObject.SetActive(false);
            if(nextPopUpSprite != null)
            nextPopUpSprite.gameObject.SetActive(true);
        }
        else
        {
            if (nextPopUpSprite != null)
                nextPopUpSprite.gameObject.SetActive(false);
        }
    }

    public void Interact()
    {
        print("interacting");
        switch (gameObject.tag)
        {
            case "Lever":
                Debug.Log("it's colliding");
                bool isOpened = leverAnim.GetBool("isOpened");
                leverAnim.SetBool("isOpened", !isOpened);

                PlayLeverSound(); // play sound when lever is toggled
                GetComponent<UseAble>().Activate();
                break;

            case "MusicStand":
                GetComponent<UseAble>().Activate();
                break;
        }
    }

    public void PlayLeverSound()
    {
        if (leverSound != null)
        {
            AudioSource.PlayClipAtPoint(leverSound, transform.position);
        }
    }
}
