using System.Collections.Generic;
using UnityEngine;

public class TankShellPool : MonoBehaviour
{
    public static TankShellPool Instance;
    [Header("Main Gun Shells")]
    [SerializeField] private List<GameObject> tankShells = new List<GameObject>();
    [SerializeField] private int mainShellCapacity;
    [SerializeField] private GameObject tankShellPrefab;

    [Header("Coaxial MG Bullets")]
    [SerializeField] private List<GameObject> cgBullets = new List<GameObject>();
    [SerializeField] private int cgBulletCapacity;
    [SerializeField] private GameObject cgBulletPrefab;
    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    // Update is called once per frame
    private void Start()
    {
        for (int i = 0; i < mainShellCapacity; i++)
        {
            GameObject mainGunShell = Instantiate(tankShellPrefab, this.transform);
            mainGunShell.SetActive(false);
            tankShells.Add(mainGunShell);
        }
        for (int i = 0;i < cgBulletCapacity; i++)
        {
            GameObject coaxialGunBullet = Instantiate(cgBulletPrefab, this.transform);
            coaxialGunBullet.SetActive(false);
            cgBullets.Add(coaxialGunBullet);
        }
    }
    public GameObject mainGunPooledObjects()
    {
        for (int i = 0; i < tankShells.Count; i++)
        {
            if (!tankShells[i].activeInHierarchy)
            {
                return tankShells[i];
            }
        }
        return null;
    }
    public GameObject coaxialGunPooledObjects()
    {
        for (int i = 0; i < cgBullets.Count; i++)
        {
            if (!cgBullets[i].activeInHierarchy)
            {
                return cgBullets[i];
            }
        }
        return null;
    }
}
