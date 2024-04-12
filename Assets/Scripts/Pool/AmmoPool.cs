using System.Collections.Generic;
using UnityEngine;

public class AmmoPool : MonoBehaviour
{
    public static AmmoPool Instance;
    [Header("Main Gun Shells")]
    [SerializeField] private List<GameObject> mainGunAmmunition = new List<GameObject>();
    [SerializeField] private int mainGunAmmoCapacity;
    [SerializeField] private GameObject mainGunAmmoPrefab;

    /*
    temporary deactivated
    [Header("Coaxial MG Bullets")]
    [SerializeField] private List<GameObject> coaxialGunAmunition = new List<GameObject>();
    [SerializeField] private int coaxialGunAmmoCapacity;
    [SerializeField] private GameObject coaxialGunAmmoPrefab;
    */
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    private void Start()
    {
        SpawnMainGunAmmo();
        //SpawnCoaxialGunAmmo();
    }

    private void SpawnMainGunAmmo()
    {
        for (int i = 0; i < mainGunAmmoCapacity; i++)
        {
            GameObject mainGunShell = Instantiate(mainGunAmmoPrefab, this.transform);
            mainGunShell.SetActive(false);
            mainGunAmmunition.Add(mainGunShell);
        }
    }
    /*
    private void SpawnCoaxialGunAmmo()
    {
        for (int i = 0; i < coaxialGunAmmoCapacity; i++)
        {
            GameObject coaxialGunBullet = Instantiate(coaxialGunAmmoPrefab, this.transform);
            coaxialGunBullet.SetActive(false);
            coaxialGunAmunition.Add(coaxialGunBullet);
        }
    }
    */
    public GameObject MainGunPooledObjects()
    {
        for (int i = 0; i < mainGunAmmunition.Count; i++)
        {
            if (!mainGunAmmunition[i].activeInHierarchy)
            {
                return mainGunAmmunition[i];
            }
        }
        return null;
    }
    /*
    public GameObject CoaxialGunPooledObjects()
    {
        for (int i = 0; i < coaxialGunAmunition.Count; i++)
        {
            if (!coaxialGunAmunition[i].activeInHierarchy)
            {
                return coaxialGunAmunition[i];
            }
        }
        return null;
    }
    */
}
