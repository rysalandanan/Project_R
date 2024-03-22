using System.Collections.Generic;
using UnityEngine;

public class TankShellPool : MonoBehaviour
{
    public static TankShellPool Instance;
    [SerializeField] private List<GameObject> tankShells = new List<GameObject>();
    [SerializeField] private int poolCapacity = 30;

    [SerializeField] private GameObject tankShellPrefab;
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
        for (int i = 0; i < poolCapacity; i++)
        {
            GameObject obj = Instantiate(tankShellPrefab, this.transform);
            obj.SetActive(false);
            tankShells.Add(obj);
        }
    }
    public GameObject GetPooledObject()
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
}
