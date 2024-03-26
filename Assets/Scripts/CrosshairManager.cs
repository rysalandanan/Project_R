using UnityEngine;

public class CrosshairManager : MonoBehaviour
{
    private void Update()
    {
        Vector2 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        this.transform.position = mouseWorldPosition;
    }
}
