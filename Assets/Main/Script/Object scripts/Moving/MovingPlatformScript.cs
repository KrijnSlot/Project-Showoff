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

    Rigidbody rb;

    void Start()
    {
        rb=GetComponent<Rigidbody>();

        gameManager = GameManager.Instance;
        beginPos = transform.position;
        gameManager = GameManager.Instance;
        // Initialize last values
        lastUpNDown = UpNDown;
        lastSideToSide = SideToSide;
    }

    void FixedUpdate()
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
        if (rb != null)
        {
            rb.MovePosition(transform.position + gameManager.timeScale * (Vector3.up * direction * speed * Time.fixedDeltaTime));
        }
        else
        {
            // TODO: Use rigidBody.movePosition
            transform.Translate(gameManager.timeScale * (Vector2.up * direction * speed * Time.fixedDeltaTime));
        }
        if (transform.position.y >= beginPos.y + moveDistance)
            direction = -1;
        else if (transform.position.y <= beginPos.y - moveDistance)
            direction = 1;
    }

    private void Hor()
    {
        if (rb != null)
        {
            rb.MovePosition(transform.position + gameManager.timeScale * (Vector3.right * direction * speed * Time.fixedDeltaTime));
        }
        else
        {
            transform.Translate(gameManager.timeScale * (Vector2.right * direction * speed * Time.fixedDeltaTime));
        }
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
