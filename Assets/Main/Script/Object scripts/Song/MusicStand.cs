using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicStand : MonoBehaviour
{
    [SerializeField] List<GameObject> noteBlocks = new List<GameObject>();
    [SerializeField] Dictionary<GameObject, SpriteRenderer> blocksVisuals = new Dictionary<GameObject, SpriteRenderer>();
    [SerializeField] Dictionary<GameObject, BoxCollider2D> blocksCollider = new Dictionary<GameObject, BoxCollider2D>();

    float despawnTimer;
    [SerializeField] float despawnTime;

    public int pagesNeeded;
    public int pagesGot;

    bool isOn;

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
            blocksVisuals[block].enabled = false;
            blocksCollider[block].enabled = false;
        }
        isOn = false;
    }

    void addPage()
    {
        pagesGot += 1;
    }
    public void Activate()
    {
        if (pagesGot >= pagesNeeded)
        {
            print("song");
            despawnTimer = despawnTime;
            isOn = true;
            foreach (var block in noteBlocks)
            {
                blocksVisuals[block].enabled = true;
                blocksCollider[block].enabled = true;
            }
        }
    }

    void Update()
    {
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
                    blocksVisuals[block].enabled = false;
                    blocksCollider[block].enabled = false;
                }
                isOn = false;
            }
        }
    }
}
