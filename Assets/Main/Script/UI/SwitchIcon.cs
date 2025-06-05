using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchIcon : MonoBehaviour
{

    [SerializeField] List<Sprite> sprites = new List<Sprite>();
    [SerializeField] Image PowerImage;
    PlayerPowers CurrentPower;
    // Start is called before the first frame update
    void Awake()
    {
        CurrentPower = this.transform.Find("Player1").GetComponent<PlayerPowers>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(CurrentPower.currentPower.ToString());

        if (CurrentPower.currentPower.ToString() == "gravityManip")
        {
            PowerImage.sprite = sprites[0];
        }

        if (CurrentPower.currentPower.ToString() == "timeManip")
        {
            PowerImage.sprite = sprites[1];
        }

        if (CurrentPower.currentPower.ToString() == "sizeManip")
        {
            PowerImage.sprite = sprites[2];
        }

        if (CurrentPower.currentPower.ToString() == "astralProject")
        {
            PowerImage.sprite = sprites[3];
        }

        if (CurrentPower.currentPower.ToString() == "realityManip")
        {
            PowerImage.sprite = sprites[4];
        }
        
        if (CurrentPower.currentPower.ToString() == "song")
            PowerImage.sprite = sprites[5];
    }
}
