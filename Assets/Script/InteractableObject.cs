using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour, IInteractable
{
    [SerializeField]
    GameObject obstacle;

    public void Interact()
    {
        Debug.Log("tag: " + tag);
        switch (tag)
        {
            case ("Interact"): obstacle.GetComponent<HandleInteraction>().InteractHandle(obstacle.tag); break;
            
        }
    }
}
