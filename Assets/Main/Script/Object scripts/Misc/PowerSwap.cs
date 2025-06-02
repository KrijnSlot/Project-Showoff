using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerPowers;

public class PowerSwap : MonoBehaviour
{
    [SerializeField]
    enum Powers
    {
        gravityManip,
        timeManip,
        sizeManip,
        astralProject,
        realityManip,
        song
    };

    [SerializeField] Powers setPower;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("player1") || collision.gameObject.CompareTag("player2"))
        {
            PlayerPowers power = collision.GetComponent<PlayerPowers>();
            switch (setPower)
            {
                case Powers.gravityManip: power.currentPower = PlayerPowers.Powers.gravityManip; break;
                case Powers.timeManip: power.currentPower = PlayerPowers.Powers.timeManip; break;
                case Powers.sizeManip: power.currentPower = PlayerPowers.Powers.sizeManip; break;
                case Powers.astralProject: power.currentPower = PlayerPowers.Powers.astralProject; break;
                case Powers.realityManip: power.currentPower = PlayerPowers.Powers.realityManip; break;
                case Powers.song: power.currentPower = PlayerPowers.Powers.song; break;
            }
        }
    }
}
