using UnityEngine;

public class SpectatorController : MonoBehaviour
{
    public float speed = 5.0f;
    public bool isSpectating = false;

    void Update()
    {
        if (!isSpectating) return;

        float x = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        float z = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        transform.Translate(x, 0, z);
    }

    public void EnableSpectatorMode()
    {
        isSpectating = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
