using UnityEngine;

public class MovingPlatformScript : MonoBehaviour
{
    public Vector2 beginPos;
    public float moveDistance = 2.5f;
    public float speed = 2f;
    public int direction = 1;

    public bool UpNDown = true;
    public bool SideToSide = false;

    private GameManager gameManager;

    // Track previous values
    private bool lastUpNDown;
    private bool lastSideToSide;

    void Start()
    {
        gameManager = GameManager.Instance;
        beginPos = transform.position;
        gameManager = GameManager.Instance;
        // Initialize last values
        lastUpNDown = UpNDown;
        lastSideToSide = SideToSide;
    }

    void Update()
    {
        if (UpNDown)
        {
            Ver();
        }
        else if (SideToSide)
        {
            Hor();
        }
    }

    private void Ver()
    {
        transform.Translate(gameManager.timeScale * (Vector2.up * direction * speed * Time.deltaTime));

        if (transform.position.y >= beginPos.y + moveDistance)
            direction = -1;
        else if (transform.position.y <= beginPos.y - moveDistance)
            direction = 1;
    }

    private void Hor()
    {
        transform.Translate(gameManager.timeScale * (Vector2.right * direction * speed * Time.deltaTime));

        if (transform.position.x >= beginPos.x + moveDistance)
            direction = -1;
        else if (transform.position.x <= beginPos.x - moveDistance)
            direction = 1;
    }

    void OnValidate()
    {
        // If UpNDown was just changed to true, turn off SideToSide
        if (UpNDown != lastUpNDown && UpNDown)
        {
            SideToSide = false;
        }
        // If SideToSide was just changed to true, turn off UpNDown
        else if (SideToSide != lastSideToSide && SideToSide)
        {
            UpNDown = false;
        }

        // Save the current state for the next comparison
        lastUpNDown = UpNDown;
        lastSideToSide = SideToSide;
    }
}
