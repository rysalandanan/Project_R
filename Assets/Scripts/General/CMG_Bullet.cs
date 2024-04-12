using System.Collections;
using UnityEngine;

public class CMG_Bullet : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(BulletDeactivate());
    }
    private IEnumerator BulletDeactivate()
    {
        yield return new WaitForSeconds(1);
        this.gameObject.SetActive(false);
    }
}
