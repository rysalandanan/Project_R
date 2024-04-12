using System.Collections.Generic;
using UnityEngine;

public class AmmoPoolForEnemies : MonoBehaviour
{
    public static AmmoPoolForEnemies Instance;
    [Header("Main Gun Shells")]
    [SerializeField] private List<GameObject> mainGunAmmunition = new List<GameObject>();
    [SerializeField] private int mainGunAmmoCapacity;
    [SerializeField] private GameObject mainGunAmmoPrefab;

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
}
