using System.Collections;
using UnityEngine;

public class TankTargeting : MonoBehaviour
{
    [Header("Main Gun Attributes")]
    [SerializeField] private Transform mainGunBarrel;
    [SerializeField] private float mainGunReloadSpeed;
    [SerializeField] private float shellVelocity;

    [Header("Turret Attributes")]
    [SerializeField] private float turretTraverseSpeed;
    [SerializeField] private GameObject turret;
    [SerializeField] private Transform forwardDirection;
    [SerializeField] private Transform targetPlayer;
    private bool isTargetNear;
    private bool canFireMainGun = true;
    private float fireDelay = 1.5f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isTargetNear = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isTargetNear = false;
        }
    }
    void Update()
    {
        if (isTargetNear)
        {
            FaceGunAtTarget();
        }
        else
        {
            FaceGunForward();
        }
        if(IsGunFacingPlayer() && canFireMainGun)
        {
            StartCoroutine(FireGun());
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
        Vector2 direction = forwardDirection.position - turret.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, angle);
        float step = turretTraverseSpeed * Time.deltaTime;
        turret.transform.rotation = Quaternion.RotateTowards(turret.transform.rotation, targetRotation, step);
    }
    private bool IsGunFacingPlayer()
    {
        Vector2 directionToPlayer = targetPlayer.position - turret.transform.position;
        float angleDifference = Vector2.Angle(turret.transform.up, directionToPlayer);
        float thresholdAngle = 10f; 
        return angleDifference <= thresholdAngle;
    }
    private IEnumerator FireGun()
    {
        GameObject tankShell = AmmoPool.Instance.MainGunPooledObjects();
        if (tankShell != null)
        {
            Rigidbody2D tankShell_RB = tankShell.GetComponent<Rigidbody2D>();
            if (tankShell_RB != null)
            {
                canFireMainGun = false;
                Vector2 barrelDirection = mainGunBarrel.up;
                //setting the position//
                tankShell.transform.position = mainGunBarrel.position;

                //setting the shell direction//
                float angle = Mathf.Atan2(barrelDirection.y, barrelDirection.x) * Mathf.Rad2Deg - 90f;
                tankShell.transform.rotation = Quaternion.Euler(0f, 0f, angle);
                yield return new WaitForSeconds(fireDelay);
                //mainGunSFX.Play();
                tankShell.SetActive(true);
                tankShell_RB.velocity = barrelDirection * shellVelocity;
                StartCoroutine(MainGunReload());
            }
        }
    }
    private IEnumerator MainGunReload()
    {
        //canFireMainGun = false;
        yield return new WaitForSeconds(mainGunReloadSpeed);
        canFireMainGun = true;
    }
    public bool IsTargetNear()
    {
        return isTargetNear;
    }
}
