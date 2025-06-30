using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicStand : UseAble
{
    [SerializeField] List<GameObject> noteBlocks = new List<GameObject>();
    [SerializeField] Dictionary<GameObject, SpriteRenderer> blocksVisuals = new Dictionary<GameObject, SpriteRenderer>();
    [SerializeField] Dictionary<GameObject, BoxCollider2D> blocksCollider = new Dictionary<GameObject, BoxCollider2D>();

    [SerializeField] GameObject standWithBook;
    [SerializeField] GameObject standWithoutBook;

    float despawnTimer;
    [SerializeField] float despawnTime;

    public int pagesNeeded;
    public int pagesGot;

    bool isOn;
    [HideInInspector] public bool gotAllPages;

    private void OnEnable()
    {
        Collectable.AddPage += addPage;
    }

    private void OnDisable()
    {
        Collectable.AddPage -= addPage;
    }

    private void Start()
    {
        foreach (var block in noteBlocks)
        {
            blocksVisuals.Add(block, block.GetComponent<SpriteRenderer>());
            blocksCollider.Add(block, block.GetComponent<BoxCollider2D>());
        }
        foreach (var block in noteBlocks)
        {
            block.SetActive(false);
        }
        isOn = false;
    }

   public void addPage()
    {
        pagesGot += 1;
    }
    public override void Activate()
    {
        if (pagesGot >= pagesNeeded)
        {
            print("song");
            standWithoutBook.SetActive(false);
            standWithBook.SetActive(true);
            despawnTimer = despawnTime;
            isOn = true;
            foreach (var block in noteBlocks)
            {
                block.SetActive(true);
            }
        }
    }

    void Update()
    {
        CheckForPages();
        if (isOn)
        {
            if (despawnTimer > 0)
            {
                despawnTimer -= Time.deltaTime;
            }
            else
            {
                foreach (var block in noteBlocks)
                {
                    block.SetActive(false);
                }
                isOn = false;
            }
        }
    }

    void CheckForPages()
    {
        if(pagesGot >= pagesNeeded)
        {
            gotAllPages = true;
        }
    }
}
