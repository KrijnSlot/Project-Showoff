using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class PowerSwap : MonoBehaviour
{

    /*[SerializeField]
    enum Powers
    {
        gravityManip,
        timeManip,
        sizeManip,
        astralProject,
        realityManip,
        song
    };*/


    [SerializeField] PlayerPowers.Powers setPower;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player1") || collision.gameObject.CompareTag("Player2"))
        {
            PlayerPowers power = collision.GetComponent<PlayerPowers>();

            //playerUI.Add(target);

            power.currentPower = setPower;
            

        }
    }
}

