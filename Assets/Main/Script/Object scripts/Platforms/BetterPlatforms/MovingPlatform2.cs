using UnityEngine;

public class MovingPlatform2 : PlatformBase
{
    [SerializeField] Transform platform;
    [SerializeField] Transform startPoint;
    [SerializeField] Transform endPoint;
    [SerializeField] float speed = 1.5f;
    [SerializeField] bool endAtStart;

    bool endReached;
    [SerializeField] bool startMoving;

    int direction = 1;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.Instance;
    }

    private void FixedUpdate()
    {
        if (startMoving)
        {
        if (!endReached)
            Move();
        }
    }

    public override void Activate()
    {
        startMoving = !startMoving;
        print("hekk");
    }


    void Move()
    {
        Vector2 target = CurrentMoveTarget();

        platform.position = Vector2.MoveTowards(platform.position, target, speed * Time.deltaTime);

        float distance = (target - (Vector2)platform.position).magnitude;
        if (distance <= 0.1f)
        {
            if (!endAtStart)
            {
                direction *= -1;
            }
            else endReached = true;

        }
    }

    Vector2 CurrentMoveTarget()
    {
        if (direction == 1)
        {
            return startPoint.position;
        }
        else
        {
            return endPoint.position;
        }
    }


    private void OnDrawGizmos()
    {
        if (platform != null && startPoint != null && endPoint != null)
        {
            Gizmos.DrawLine(platform.transform.position, startPoint.position);
            Gizmos.DrawLine(platform.transform.position, endPoint.position);
        }
    }
}
