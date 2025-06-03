using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleDoorInteraction : MonoBehaviour
{
    public int leverIndex = 0;
    public DoorCheck door;

    Animator leverAnim;

    private void Awake()
    {
        leverAnim = this.GetComponent<Animator>();
    }

    public void InteractDoorHandle()
    {
        door.LeverFlipped(leverIndex);

        if (leverAnim.GetBool("isOpened") == false)
            leverAnim.SetBool("isOpened", true);
        else
            leverAnim.SetBool("isOpened", false);
    }

}
