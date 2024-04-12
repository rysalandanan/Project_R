using UnityEngine;

public class PlayerTankMovement : MonoBehaviour
{
    [Header("Tank Movement Attributes")]
    [SerializeField] private float acceleration;
    [SerializeField] private float maxSpeed;
    [SerializeField] private GameObject hullTank;
    [SerializeField] private float hullTraverseSpeed;

    [Header("Tank Movement Path")]
    [SerializeField] private GameObject pathNode; //only one can exist at a time
    private Rigidbody2D rb;
    private Transform targetNode;
    private Vector2 direction;
    private bool isTraversing;
    private PlayerTankStatus playerTankStatus;
    private bool hasPath = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerTankStatus = GetComponent<PlayerTankStatus>();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(pathNode.activeInHierarchy)
            {
                pathNode.SetActive(false);
            }
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pathNode.transform.position = mousePosition;
            pathNode.SetActive(true);
            hasPath = true;
        }
        if(hasPath && playerTankStatus.CanMove())
        {
            MoveToNextNode();
        }
    }

    private void MoveToNextNode()
    {
        GetTargetNode();
        FindDirection();
        if (!isTraversing)
        {
            Move();
        }
        SpeedLimit();
    }
    private void GetTargetNode()
    {
        targetNode = pathNode.transform;
    }
    private void FindDirection()
    {
        direction = (targetNode.position - transform.position).normalized;
        direction.Normalize();
        FaceForward();
    }
    private void FaceForward()
    {
        if(hasPath)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            Quaternion targetRotation = Quaternion.Euler(0f, 0f, angle);
            float step = hullTraverseSpeed * Time.deltaTime;
            hullTank.transform.rotation = Quaternion.RotateTowards(hullTank.transform.rotation, targetRotation, step);

            if (hullTank.transform.rotation != targetRotation)
            {
                isTraversing = true;
            }
            else
            {
                isTraversing = false;
            }
        }
    }
    private void Move()
    {
        Vector2 accelerationVector = acceleration * Time.deltaTime * direction;
        rb.AddForce(accelerationVector);
    }
    private void SpeedLimit()
    {
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }
}

