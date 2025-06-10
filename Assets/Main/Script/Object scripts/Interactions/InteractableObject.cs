using UnityEngine;

public class InteractableObject : MonoBehaviour, IInteractable
{
    

    [SerializeField] private float checkForPlayerRange;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private GameObject popUpSprite;
    private bool playerInRange;
    private bool popUpOn;
    Animator leverAnim;

    private void Awake()
    {
        leverAnim = this.GetComponent<Animator>();
    }
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
