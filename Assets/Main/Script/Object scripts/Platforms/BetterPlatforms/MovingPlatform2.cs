using UnityEngine;

public class MovingPlatform2 : PlatformBase
{
    [SerializeField] Transform platform;
    [SerializeField] Transform startPoint;
    [SerializeField] Transform endPoint;
    [SerializeField] float speed = 1.5f;
    [SerializeField] bool endAtStart;
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] bool startMoving;

    bool endReached;
   
    int direction = 1;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.Instance;
        MakeLine();
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

    void MakeLine()
    {
        Vector3 lineStartPoint = startPoint.position;
        Vector3 lineEndPoint = endPoint.position;
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, lineStartPoint);
        lineRenderer.SetPosition(1, lineEndPoint);
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
