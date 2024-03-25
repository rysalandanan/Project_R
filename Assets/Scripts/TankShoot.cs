using System.Collections;
using UnityEngine;

public class TankShoot : MonoBehaviour
{
    [Header("Main Gun Attributes")]
    [SerializeField] private Transform mainGunBarrel;
    [SerializeField] private float shellVelocity;
    [SerializeField] private float mainGunReloadSpeed;
    [SerializeField] private GameObject mainGunMuzzleFlash;
    [SerializeField] private TankShellPool shellPool;
    [SerializeField] private int shellCapacity;
    private int shellFired;
    private bool canFireMainGun = true;

    [Header("Coaxial Machine Gun Attributes")]
    [SerializeField] private Transform coaxialGunBarrel;
    [SerializeField] private float bulletVelocity;
    [SerializeField] private float coaxialGunReloadSpeed;
    [SerializeField] private GameObject coaxialGunMuzzleFlash;
    [SerializeField] private float coaxialGunBulletSpread;
    [SerializeField] private float coaxialGunRateOfFire;
    private bool canFireCMG = true;

    private void Update()
    {
        if(Input.GetMouseButton(0) && canFireMainGun && shellFired < shellCapacity)
        {
            FireMainGun();
        }
        if(Input.GetKey(KeyCode.Space) && canFireCMG)
        {
           FireCoaxialGun();
        }
            
    }
    private void FireMainGun()
    {
        GameObject tankShell = TankShellPool.Instance.mainGunPooledObjects();
        if(tankShell != null)
        {
            Rigidbody2D tankShell_RB = tankShell.GetComponent<Rigidbody2D>();
            if (tankShell_RB != null)
            {
                //setting the position//
                tankShell.transform.position = mainGunBarrel.position;
                //setting the shell direction//
                Vector2 barrelDirection = mainGunBarrel.up;
                float angle = Mathf.Atan2(barrelDirection.y, barrelDirection.x) * Mathf.Rad2Deg - 90f;
                tankShell.transform.rotation = Quaternion.Euler(0f, 0f, angle);
                //activating and shooting the shell//
                tankShell.SetActive(true);
                tankShell_RB.velocity = barrelDirection * shellVelocity;

                shellFired += 1;
                StartCoroutine(MainGunReload());
                StartCoroutine(MainGunMuzzleFlash());
            }
        }
    }
    private IEnumerator MainGunReload()
    {
        canFireMainGun = false;
        yield return new WaitForSeconds(mainGunReloadSpeed);
        canFireMainGun = true;
    }
    private IEnumerator MainGunMuzzleFlash()
    {
        mainGunMuzzleFlash.SetActive(true);
        yield return new WaitForSeconds(0.15f);
        mainGunMuzzleFlash.SetActive(false);
    }
    private void FireCoaxialGun()
    {
        GameObject tankCMG = TankShellPool.Instance.coaxialGunPooledObjects();
        if (tankCMG != null)
        {
            Rigidbody2D tankCMG_RB = tankCMG.GetComponent<Rigidbody2D>();
            if (tankCMG_RB != null)
            {
                Vector2 barrelDirection = coaxialGunBarrel.up;

                //Coaxial Machine Gun bullet Spread
                float spreadAngle = Random.Range(-coaxialGunBulletSpread, coaxialGunBulletSpread);
                Quaternion spreadRotation = Quaternion.Euler(0f, 0f, spreadAngle);
                Vector2 spreadDirection = spreadRotation * barrelDirection;

                tankCMG.transform.position = coaxialGunBarrel.position;
                
                float angle = Mathf.Atan2(spreadDirection.y, spreadDirection.x) * Mathf.Rad2Deg - 90f;
                tankCMG.transform.rotation = Quaternion.Euler(0f, 0f, angle);

                tankCMG.SetActive(true);
                tankCMG_RB.velocity = spreadDirection * bulletVelocity;

                StartCoroutine(CMGFireDelay());
                StartCoroutine(CMGMuzzleFlash());
            }
        }
    }
    private IEnumerator CMGFireDelay()
    {
        canFireCMG = false;
        yield return new WaitForSeconds(coaxialGunRateOfFire);
        canFireCMG = true;
    }
    private IEnumerator CMGMuzzleFlash()
    {
        coaxialGunMuzzleFlash.SetActive(true);
        yield return new WaitForSeconds(0.15f);
        coaxialGunMuzzleFlash.SetActive(false);
    }
}
