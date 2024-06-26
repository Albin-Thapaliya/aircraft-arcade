using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Transform aircraft = null;
    [SerializeField] private Transform mouseAim = null;
    [SerializeField] private Transform cameraRig = null;
    [SerializeField] private Transform cam = null;

    [Header("Options")]
    [SerializeField] [Tooltip("Follow aircraft using fixed update loop")]
    private bool useFixed = true;

    [SerializeField] [Tooltip("How quickly the camera tracks the mouse aim point.")]
    private float camSmoothSpeed = 5f;

    [SerializeField] [Tooltip("Mouse sensitivity for the mouse flight target")]
    private float mouseSensitivity = 3f;

    [SerializeField] [Tooltip("How far the boresight and mouse flight are from the aircraft")]
    private float aimDistance = 500f;

    public Vector3 BoresightPos
    {
        get { return (aircraft.transform.forward * aimDistance) + aircraft.transform.position; }
    }

    public Vector3 MouseAimPos
    {
        get { return (mouseAim.transform.forward * aimDistance) + mouseAim.transform.position; }
    }

    private void Update()
    {
        if (useFixed == false)
            UpdateCameraPos();

        RotateRig();
    }

    private void FixedUpdate()
    {
        if (useFixed == true)
            UpdateCameraPos();
    }

    private void RotateRig()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = -Input.GetAxis("Mouse Y") * mouseSensitivity;

        mouseAim.Rotate(cam.right, mouseY, Space.World);
        mouseAim.Rotate(cam.up, mouseX, Space.World);

        Vector3 upVec = (Mathf.Abs(mouseAim.forward.y) > 0.9f) ? cameraRig.up : Vector3.up;

        cameraRig.rotation = Damp(cameraRig.rotation, Quaternion.LookRotation(MouseAimPos - cameraRig.position, upVec), camSmoothSpeed, Time.deltaTime);
    }

    private void UpdateCameraPos()
    {
        transform.position = aircraft.position;
    }

    private Quaternion Damp(Quaternion a, Quaternion b, float lambda, float dt)
    {
        return Quaternion.Slerp(a, b, 1 - Mathf.Exp(-lambda * dt));
    }
}