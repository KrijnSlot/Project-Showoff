    using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class DoorCheck : MonoBehaviour, IInteractable 
{
    bool[] leverStates;

    [SerializeField] int leversNeeded;

    private void Awake()
    {
        leverStates = new bool[leversNeeded];
    }

    // Start is called before the first frame update
    public void LeverFlipped(int pLeverIndex)
    {
        Debug.Log("Lever index:" + pLeverIndex);
        leverStates[pLeverIndex] = !leverStates[pLeverIndex];
        OpenDoorWhenSwitched();
    }

    void OpenDoorWhenSwitched()
    {
        if (leverStates.Count (x => x) == leversNeeded)
        { // if all switches are flipped, you can add an animation and disable the collider for player passthrough
            this.gameObject.SetActive(false);
        }
    }

    public void Interact()
    {
        OpenDoorWhenSwitched();
    }
}
