using System.Collections;
using UnityEngine;

public class TankShoot : MonoBehaviour
{
    [SerializeField] private Transform tankBarrel;
    [SerializeField] private float shellVelocity;
    [SerializeField] private float rateOfFire;
    [SerializeField] private GameObject muzzleFlash;
    [SerializeField] private TankShellPool shellPool;

    private bool canFire = true;

    private void Update()
    {
        if(Input.GetMouseButton(0) && canFire)
        {
            SpawnShell();
        }
    }
    private void SpawnShell()
    {
        GameObject tankShell = TankShellPool.Instance.GetPooledObject();
        if(tankShell != null)
        {
            Rigidbody2D tankShell_RB = tankShell.GetComponent<Rigidbody2D>();
            if (tankShell_RB != null)
            {
                //setting the position//
                tankShell.transform.position = tankBarrel.position;
                //setting the shell direction//
                Vector2 barrelDirection = tankBarrel.up;
                float angle = Mathf.Atan2(barrelDirection.y, barrelDirection.x) * Mathf.Rad2Deg - 90f;
                tankShell.transform.rotation = Quaternion.Euler(0f, 0f, angle);
                //activating and shooting the shell//
                tankShell.SetActive(true);
                tankShell_RB.velocity = barrelDirection * shellVelocity;

                StartCoroutine(ShootDelay());
                StartCoroutine(CreateMuzzleFlash());
            }
        }
    }
    private IEnumerator ShootDelay()
    {
        canFire = false;
        yield return new WaitForSeconds(rateOfFire);
        canFire = true;
    }
    private IEnumerator CreateMuzzleFlash()
    {
        muzzleFlash.SetActive(true);
        yield return new WaitForSeconds(0.15f);
        muzzleFlash.SetActive(false);
    }
}
