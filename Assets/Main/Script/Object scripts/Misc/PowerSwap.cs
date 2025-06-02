using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class PowerSwap : MonoBehaviour
{

    [SerializeField] List<Image> playerUI = new List<Image>();

    [SerializeField] List<Sprite> sprites = new List<Sprite>();
    

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
        if (collision.gameObject.CompareTag("Player1") || collision.gameObject.CompareTag("Player2"))
        {
            
            PlayerPowers power = collision.GetComponent<PlayerPowers>();
            switch (setPower)
            {
                case Powers.gravityManip: power.currentPower = PlayerPowers.Powers.gravityManip;
                    if (collision.gameObject.CompareTag("Player1")) {playerUI[0].sprite = sprites[0];} 
                    else{playerUI[1].sprite = sprites[0];}
                        break;

                case Powers.timeManip: power.currentPower = PlayerPowers.Powers.timeManip;
                    if (collision.gameObject.CompareTag("Player1")) { playerUI[0].sprite = sprites[1]; }
                    else { playerUI[1].sprite = sprites[1]; }
                    break;

                case Powers.sizeManip: power.currentPower = PlayerPowers.Powers.sizeManip;
                    if (collision.gameObject.CompareTag("Player1")) { playerUI[0].sprite = sprites[2]; }
                    else { playerUI[1].sprite = sprites[2]; }
                    break;

                case Powers.astralProject: power.currentPower = PlayerPowers.Powers.astralProject;
                    if (collision.gameObject.CompareTag("Player1")) { playerUI[0].sprite = sprites[3]; }
                    else { playerUI[1].sprite = sprites[3]; }
                    break;

                case Powers.realityManip: power.currentPower = PlayerPowers.Powers.realityManip;
                    if (collision.gameObject.CompareTag("Player1")) { playerUI[0].sprite = sprites[4]; }
                    else { playerUI[1].sprite = sprites[4]; }
                    break;

                case Powers.song: power.currentPower = PlayerPowers.Powers.song;
                    if (collision.gameObject.CompareTag("Player1")) { playerUI[0].sprite = sprites[5]; }
                    else { playerUI[1].sprite = sprites[5]; }
                    break;
            }
            
        }
    }
}
