using UnityEngine;

public class SpectatorController : MonoBehaviour
{
    public float speed = 5.0f;
    public float mouseSensitivity = 2.0f;
    public bool isSpectating = false;

    private Camera spectatorCamera;

    void Start()
    {
        spectatorCamera = Camera.main;
    }

    void Update()
    {
        if (!isSpectating) return;

        HandleMovement();
        HandleCameraLook();
        HandleInput();
    }

    void HandleMovement()
    {
        float x = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        float z = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        float y = 0;

        if (Input.GetKey(KeyCode.E))
        {
            y = speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.Q))
        {
            y = -speed * Time.deltaTime;
        }

        Vector3 move = transform.right * x + transform.up * y + transform.forward * z;
        transform.Translate(move, Space.World);
    }

    void HandleCameraLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = -Input.GetAxis("Mouse Y") * mouseSensitivity;

        Vector3 newRotation = transform.eulerAngles;
        newRotation.x += mouseY;
        newRotation.y += mouseX;
        transform.eulerAngles = newRotation;
    }

    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            ToggleSpectatorMode();
        }
    }

    public void EnableSpectatorMode()
    {
        isSpectating = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        UIManager.ShowSpectatorMode(true);
    }

    public void DisableSpectatorMode()
    {
        isSpectating = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        UIManager.ShowSpectatorMode(false);
    }

    private void ToggleSpectatorMode()
    {
        if (isSpectating)
        {
            DisableSpectatorMode();
        }
        else
        {
            EnableSpectatorMode();
        }
    }
}
public static class UIManager
{
    public static void ShowSpectatorMode(bool show)
    {
        Debug.Log("Spectator Mode: " + (show ? "Enabled" : "Disabled"));
    }
}