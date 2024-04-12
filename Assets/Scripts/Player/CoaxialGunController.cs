/*
using System.Collections;
using UnityEngine;

public class CoaxialGunController : MonoBehaviour
{
    temporary deactivated

    
    [Header("Coaxial Machine Gun Attributes")]
    [SerializeField] private Transform coaxialGunBarrel;
    [SerializeField] private float bulletVelocity;
    [SerializeField] private float coaxialGunReloadSpeed;
    [SerializeField] private GameObject coaxialGunMuzzleFlash;

    [SerializeField] private float coaxialGunBulletSpread;
    [SerializeField] private float coaxialGunRateOfFire;
    [SerializeField] private AudioSource coaxialGunSFX;
    private bool canFireCMG = true;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && canFireCMG)
        {
            FireCoaxialGun();
        }
    }
    private void FireCoaxialGun()
    {
        GameObject tankCMG = AmmoPool.Instance.CoaxialGunPooledObjects();
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

                coaxialGunSFX.Play();
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
*/
