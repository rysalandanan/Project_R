using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Transform[] nodePoints;
    [SerializeField] private float acceleration;
    [SerializeField] private float maxSpeed;
    [SerializeField] private GameObject hullTank;
    [SerializeField] private float hullTraverseSpeed;
    private int currentNodeIndex = 0;
    private Rigidbody2D rb;
    private Transform targetNode;
    private Vector2 direction;
    private bool isTraversing;
    private EnemyTargeting targeting;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        targeting = GetComponent<EnemyTargeting>();
    }
    private void Update()
    {
        if(!targeting.IsPlayerNear())
        {
            MoveToNextNode();
        }
    }

    private void MoveToNextNode()
    {
        if (nodePoints.Length == 0)
        {
            return;
        }
        GetTargetNode();
        FindDirection();
        if (!isTraversing)
        {
            Move();
        }
        SpeedLimit();
        CheckPath();  
    }
    private void GetTargetNode()
    {
        targetNode = nodePoints[currentNodeIndex];
    }
    private void FindDirection()
    {
        direction = (targetNode.position - transform.position).normalized;
        direction.Normalize();
        FaceForward();
    }
    private void FaceForward()
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f; 
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, angle);
        float step = hullTraverseSpeed * Time.deltaTime;
        hullTank.transform.rotation = Quaternion.RotateTowards(hullTank.transform.rotation, targetRotation, step);

        if(hullTank.transform.rotation != targetRotation)
        {
            isTraversing = true;
        }
        else
        {
            isTraversing = false;
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
    private void CheckPath()
    {
        if (Vector3.Distance(transform.position, targetNode.position) < 0.1f)
        {
            // Move to the next node
            currentNodeIndex = (currentNodeIndex + 1) % nodePoints.Length;
        }
    }
}
