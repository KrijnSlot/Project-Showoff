using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleDoorInteraction : UseAble
{
    public int leverIndex = 0;
    public DoorCheck door;


    public override void Activate()
    {
        door.LeverFlipped(leverIndex);
    }

}
