using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour, IInteractable
{
    [SerializeField]
    GameObject obstacle;

    public void Interact()
    {
        obstacle.SetActive(false);
    }
}
