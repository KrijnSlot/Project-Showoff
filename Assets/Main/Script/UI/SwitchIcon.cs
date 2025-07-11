using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchIcon : MonoBehaviour
{

    [SerializeField] List<Sprite> sprites = new List<Sprite>();
    [SerializeField] List<Sprite> buttons = new List<Sprite>();
    [SerializeField] Image PowerImage;
    [SerializeField] Image buttonImage;

    PlayerPowers CurrentPower;
    // Start is called before the first frame update
    void Awake()
    {
        CurrentPower = transform.GetChild(0).GetComponent<PlayerPowers>(); ;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(CurrentPower.currentPower.ToString());

        switch (CurrentPower.currentPower)
        {
            case (PlayerPowers.Powers.gravityManip):
                PowerImage.sprite = sprites[0];
                buttonImage.sprite = buttons[0];
                break;
            case (PlayerPowers.Powers.timeManip):
                PowerImage.sprite = sprites[1];
                buttonImage.sprite = buttons[0];
                break;
            case (PlayerPowers.Powers.sizeManip):
                PowerImage.sprite = sprites[2];
                buttonImage.sprite = buttons[0];
                break;
            case (PlayerPowers.Powers.astralProject):
                PowerImage.sprite = sprites[3];
                buttonImage.sprite = buttons[0];
                break;
            case (PlayerPowers.Powers.realityManip):
                PowerImage.sprite = sprites[4];
                buttonImage.sprite = buttons[0];
                break;
            case (PlayerPowers.Powers.song):
                PowerImage.sprite = sprites[5];
                buttonImage.sprite = buttons[1];
                break;


        }

        //if (CurrentPower.currentPower.ToString() == "gravityManip")
        //{
        //    PowerImage.sprite = sprites[0];
        //    buttonImage.sprite = buttons[0];
        //}

        //if (CurrentPower.currentPower.ToString() == "timeManip")
        //{
        //    PowerImage.sprite = sprites[1];
        //    buttonImage.sprite = buttons[0];
        //}

        //if (CurrentPower.currentPower.ToString() == "sizeManip")
        //{
        //    PowerImage.sprite = sprites[2];
        //    buttonImage.sprite = buttons[0];
        //}

        //if (CurrentPower.currentPower.ToString() == "astralProject")
        //{
        //    PowerImage.sprite = sprites[3];
        //    buttonImage.sprite = buttons[0];
        //}

        //if (CurrentPower.currentPower.ToString() == "realityManip")
        //{
        //    PowerImage.sprite = sprites[4];
        //    buttonImage.sprite = buttons[0];
        //}
        
        //if (CurrentPower.currentPower.ToString() == "song")
        //{
        //    PowerImage.sprite = sprites[5];
        //    buttonImage.sprite = buttons[1];
        //}
    }
}
