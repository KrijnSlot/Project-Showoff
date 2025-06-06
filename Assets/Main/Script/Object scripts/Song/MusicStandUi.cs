using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MusicStandUi : MonoBehaviour
{
    [SerializeField] int pagesGot;
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
    }

    private void OnDisable()
    {
        Collectable.AddPage += addPage;
    }
    private void Update()
    {
        addPage();
        if (gameObject.activeSelf) text.text = pagesGot + "/" + pagesNeeded;
    }

    void addPage()
    {
        pagesGot = musicStand.GetComponent<MusicStand>().pagesGot;
    }
}
