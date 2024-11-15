using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 2f;
    public float moveDistance = 5f;
    public int hp = 6;

    private Vector3 startPosition;
    private Vector3 targetPosition;
    private bool movingForward = true;
    private bool movingBack = false;

    void Start()
    {
        startPosition = transform.position;
        targetPosition = startPosition + new Vector3(moveDistance, 0, 0);
    }

    void Update()
    {
        Move();
        if(hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    void Move()
    {
        if (movingForward)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            if (transform.position == targetPosition)
            {
                movingForward = false;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, startPosition, speed * Time.deltaTime);
            if (transform.position == startPosition)
            {
                movingForward = true;
                movingBack = false;
            }
        }

        if(movingBack)
        {
            transform.position = Vector3.MoveTowards(transform.position, -targetPosition, speed * Time.deltaTime);
            if(transform.position == -startPosition)
            {
                movingBack = false;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, -startPosition, speed * Time.deltaTime);
            if (transform.position == -startPosition)
            {
                movingForward = false;
                movingBack = true;
            }
        }
    }
}

