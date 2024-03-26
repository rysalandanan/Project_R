using UnityEngine;

public class EnemyTargeting : MonoBehaviour
{
    [SerializeField] private Transform targetPlayer;
    [SerializeField] private float turretTraverseSpeed;
    [SerializeField] private GameObject turret;
    private bool isPlayerNear;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerNear = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerNear = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerNear)
        {
            FaceGunAtTarget();
        }
        else
        {
            FaceGunForward();
        }
    }
    private void FaceGunAtTarget()
    {
        Vector2 direction = targetPlayer.position - turret.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, angle);
        float step = turretTraverseSpeed * Time.deltaTime;
        turret.transform.rotation = Quaternion.RotateTowards(turret.transform.rotation, targetRotation, step);
    }
    private void FaceGunForward()
    {
       
    }
    public bool IsPlayerNear()
    {
        return isPlayerNear;
    }
}
