using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class PowerSwap : MonoBehaviour
{
    public static event Action<int> powerSwapped;
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
            if (collision.gameObject.CompareTag("Player1"))
                powerSwapped?.Invoke(1);
            else powerSwapped?.Invoke(2);


                power.currentPower = setPower;
            

        }
    }
}

