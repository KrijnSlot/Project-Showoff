using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class SetBoundaryOnTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player1") || collision.gameObject.CompareTag("Player2"))
        {
            CinemachineConfiner2D confiner = collision.transform.parent.GetComponentInChildren<CinemachineConfiner2D>();
            confiner.BoundingShape2D = GetComponent<BoxCollider2D>();
        }
    }
}
