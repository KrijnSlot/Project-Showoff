using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerInteract : MonoBehaviour
{
    private bool eButtonPressed = false;
    private bool isInteracting = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) eButtonPressed = true;
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
        Debug.Log("circle collider enabled");
        yield return new WaitForSeconds(time);
        GetComponent<CircleCollider2D>().enabled = false;
        isInteracting = false;
        Debug.Log("circle collider disabled");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isInteracting)
            return;

        var interactable = other.GetComponent<IInteractable>();
        if (interactable != null)
        {
            interactable.Interact();
        }
    }
}
