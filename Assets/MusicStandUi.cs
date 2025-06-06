using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MusicStandUi : MonoBehaviour
{
    int pagesGot;
    int pagesNeeded;
    [SerializeField] GameObject musicStand;
    TextMeshPro text;

    private void Start()
    {
        text = GetComponent<TextMeshPro>();
        pagesNeeded = musicStand.GetComponent<MusicStand>().pagesNeeded;
    }

    private void OnEnable()
    {
        Collectable.AddPage += addPage;
        print("enabled");
        
    }

    private void OnDisable()
    {
        Collectable.AddPage += addPage;
    }
    private void Update()
    {
        if(gameObject.activeSelf) text.text = pagesGot + "/" + pagesNeeded;
    }

    void addPage()
    {
        pagesGot += 1;
    }
}
