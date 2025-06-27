using System.ComponentModel;
using UnityEngine;

public class InteractableObject : MonoBehaviour, IInteractable
{


    [SerializeField] private float checkForPlayerRange;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private GameObject popUpSprite;
    [SerializeField] private GameObject nextPopUpSprite;
    private bool playerInRange;
    private bool popUpOn;

    Animator leverAnim;
    MusicStand musicStand;

    private void Awake()
    {
        leverAnim = this.GetComponent<Animator>();
        musicStand = this.GetComponent<MusicStand>();
    }
    void Start()
    {
        popUpSprite.gameObject.gameObject.SetActive(false);
        nextPopUpSprite.gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        PopUpBehavior();
    }

    void PopUpBehavior()
    {
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

        if (musicStand.gotAllPages)
        {
            popUpOn = false;
            popUpSprite.gameObject.SetActive(false);
            nextPopUpSprite.gameObject.SetActive(true);
        }
    }

    public void Interact()
    {
        /*Debug.Log("tag: " + tag);*/
        print("interacting");
        switch (gameObject.tag)
        {
            case "Lever":
                Debug.Log("it's colliding");
                if (leverAnim.GetBool("isOpened") == false)
                    leverAnim.SetBool("isOpened", true);
                else
                    leverAnim.SetBool("isOpened", false);

                GetComponent<UseAble>().Activate();
                break;
            case ("MusicStand"):
                GetComponent<UseAble>().Activate();
                break;
        }
    }
}
