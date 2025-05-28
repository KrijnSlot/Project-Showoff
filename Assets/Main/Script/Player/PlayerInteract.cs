using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerInteract : MonoBehaviour
{
    private bool eButtonPressed = false;
    private bool isInteracting = false;

    public PlayerInput playerInput;

    public void Interact(InputAction.CallbackContext context)
    {
        UnityEngine.Debug.Log("interact");
        if (context.performed)
        {
            eButtonPressed = true;
        }
    }

    private void FixedUpdate()
    {
        if (eButtonPressed)
        {
            StartCoroutine(InteractTime(.2f));
            eButtonPressed = false;
        }
    }

    private IEnumerator InteractTime(float time)
    {
        GetComponent<CircleCollider2D>().enabled = true;
        isInteracting = true;
        /*Debug.Log("circle collider enabled");*/
        
        yield return new WaitForSeconds(time);

        GetComponent<CircleCollider2D>().enabled = false;
        isInteracting = false;
        /*Debug.Log("circle collider disabled");*/
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isInteracting)
            return;
        var interactable = other.GetComponent<IInteractable>();
        UnityEngine.Debug.Log(other.tag);
        UnityEngine.Debug.Log(interactable);
        if (interactable != null)
        {
            UnityEngine.Debug.Log("colission");
            interactable.Interact();
        }
    }
}
