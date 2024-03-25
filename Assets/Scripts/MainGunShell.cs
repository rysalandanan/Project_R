using System.Collections;
using UnityEngine;

public class MainGunShell : MonoBehaviour
{
    public string hitTag;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hitTag = collision.gameObject.tag;
    }
    private void OnEnable()
    {
        StartCoroutine(ShellDeactivate());
    }
    private IEnumerator ShellDeactivate()
    {
        yield return new WaitForSeconds(3);
        this.gameObject.SetActive(false);
    }
}
