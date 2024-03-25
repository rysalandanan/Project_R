using UnityEngine;

public class TankTurretTraverse : MonoBehaviour
{
    [Header("Tank Turret Attributes")]
    [SerializeField] private float turretTraverseSpeed;
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject turret;
    private Vector2 mousePos;
    private TankStatus tankstatus;

    private void Start()
    {
        tankstatus = GetComponent<TankStatus>();
    }
    private void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }
    private void FixedUpdate()
    {
        if (tankstatus.IsGunnerAlive())
        {
            TurretRotation();
        } 
    }
    private void TurretRotation()
    {
        Vector2 turretDirection = mousePos - (Vector2)turret.transform.position;
        float angle = Mathf.Atan2(turretDirection.y, turretDirection.x) * Mathf.Rad2Deg - 90f;
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, angle);
        float speed = turretTraverseSpeed * Time.deltaTime;
        turret.transform.rotation = Quaternion.RotateTowards(turret.transform.rotation, targetRotation, speed);
    }

}
