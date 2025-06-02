using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleDoorInteraction : MonoBehaviour
{
    public int leverIndex = 0;
    public DoorCheck door;

     public void InteractDoorHandle()
    {
        door.LeverFlipped(leverIndex);
    }

}
