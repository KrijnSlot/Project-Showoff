using UnityEngine;

public class BreakablePlatform : UseAble
{
    [SerializeField] float breakTime;
    float breakTimer;
    bool breaking;
    bool broken;

    [SerializeField] float respawnTime;
    public float respawnTimer;

    BoxCollider2D col;
    SpriteRenderer rend;
    private void Start()
    {
        breakTimer = breakTime;
        respawnTimer = respawnTime;
        col = GetComponent<BoxCollider2D>();
        rend = GetComponent<SpriteRenderer>();
    }
    public override void Activate()
    {
        print("On");
        breaking = true;
    }

    private void FixedUpdate()
    {
        if (breaking)
        {
            breakTimer -= Time.deltaTime;
            if (breakTimer <= 0)
            {
                col.enabled = false;
                rend.enabled = false;
                breaking = false;
                broken = true;
                breakTimer = breakTime;
            }

            print(breakTimer);
        }
        if (broken)
        {
            respawnTimer -= Time.deltaTime;
            if (respawnTimer <= 0)
            {
                col.enabled = true;
                rend.enabled = true;
                respawnTimer = respawnTime;
                broken = false;
            }
        }
    }

    public override void DeActivate()
    {
        print("Deactivated");
        breakTimer = breakTime;
        breaking = false;
    }
}
