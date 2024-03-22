using System.Collections;
using UnityEngine;

public class Shell : MonoBehaviour
{
    public string hitTag;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        this.gameObject.SetActive(false);
        hitTag = collision.gameObject.tag;
        
    }
    private void OnEnable()
    {
        //if missed
        StartCoroutine(ShellDeactivate());
    }
    private IEnumerator ShellDeactivate()
    {
        yield return new WaitForSeconds(3);
        this.gameObject.SetActive(false);
    }
}
