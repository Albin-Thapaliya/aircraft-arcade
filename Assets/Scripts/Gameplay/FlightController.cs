﻿using UnityEngine;

public class FlightController : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField]
    [Tooltip("Hud to spawn to display and operate the crosshairs for the controller")]
    private Hud hudPrefab = null;

    [Header("Components")]
    [SerializeField]
    [Tooltip("Transform of the aircraft the rig follows and references")]
    private Transform aircraft = null;
    [SerializeField]
    [Tooltip("Transform of the object the mouse rotates to generate MouseAim position")]
    private Transform mouseAim = null;
    [SerializeField]
    [Tooltip("Transform of the object on the rig which the camera is attached to")]
    private Transform cameraRig = null;
    [SerializeField]
    [Tooltip("Transform of the camera itself")]
    private Transform cam = null;

    [Header("Options")]
    [SerializeField]
    [Tooltip("Follow aircraft using fixed update loop")]
    private bool useFixed = true;

    [SerializeField]
    [Tooltip("How quickly the camera tracks the mouse aim point.")]
    private float camSmoothSpeed = 5f;

    [SerializeField]
    [Tooltip("Mouse sensitivity for the mouse flight target")]
    private float mouseSensitivity = 3f;

    [SerializeField]
    [Tooltip("How far the boresight and mouse flight are from the aircraft")]
    private float aimDistance = 500f;

    private Rigidbody rb;
    private Plane plane;

    public Vector3 BoresightPos
    {
        get
        {
            return aircraft == null
                ? transform.forward * aimDistance
                : (aircraft.transform.forward * aimDistance) + aircraft.transform.position;
        }
    }

    public Vector3 MouseAimPos
    {
        get
        {
            return mouseAim == null
                ? transform.forward * aimDistance
                : (mouseAim.transform.forward * aimDistance) + mouseAim.transform.position;
        }
    }

    private void Awake()
    {
        ValidateComponents();
        Cursor.lockState = CursorLockMode.Locked;

        if (hudPrefab != null)
        {
            var hudGameObject = Instantiate(hudPrefab);
            var hud = hudGameObject.GetComponent<Hud>();
            hud.SetReferenceFlightController(this);
        }
        else
            Debug.LogError($"{name}: FlightController - No HUD prefab assigned!");

        rb = GetComponent<Rigidbody>();
        if (rb == null)
            Debug.LogError($"{name}: FlightController - Missing Rigidbody component!");

        if (aircraft != null)
        {
            plane = aircraft.GetComponent<Plane>();
            if (plane == null)
                Debug.LogError($"{name}: FlightController - No Plane script found on aircraft!");
        }

        transform.parent = null;
    }

    private void ValidateComponents()
    {
        if (aircraft == null)
            Debug.LogError($"{name}: FlightController - No aircraft transform assigned!");
        if (mouseAim == null)
            Debug.LogError($"{name}: FlightController - No mouse aim transform assigned!");
        if (cameraRig == null)
            Debug.LogError($"{name}: FlightController - No camera rig transform assigned!");
        if (cam == null)
            Debug.LogError($"{name}: FlightController - No camera transform assigned!");
    }

    private void Update()
    {
        if (!useFixed)
            UpdateCameraPos();

        RotateRig();
    }

    private void FixedUpdate()
    {
        if (useFixed)
            UpdateCameraPos();
    }

    private void RotateRig()
    {
        if (mouseAim == null || cam == null || cameraRig == null)
            return;

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = -Input.GetAxis("Mouse Y") * mouseSensitivity;

        mouseAim.Rotate(cam.right, mouseY, Space.World);
        mouseAim.Rotate(cam.up, mouseX, Space.World);

        Vector3 upVec = (Mathf.Abs(mouseAim.forward.y) > 0.9f) ? cameraRig.up : Vector3.up;

        cameraRig.rotation = Damp(cameraRig.rotation, Quaternion.LookRotation(MouseAimPos - cameraRig.position, upVec), camSmoothSpeed, Time.deltaTime);
    }

    private void UpdateCameraPos()
    {
        if (aircraft != null)
            transform.position = aircraft.position;
    }

    private Quaternion Damp(Quaternion a, Quaternion b, float lambda, float dt)
    {
        return Quaternion.Slerp(a, b, 1 - Mathf.Exp(-lambda * dt));
    }

    public float GetSpeed()
    {
        return rb.velocity.magnitude;
    }

    public float GetAltitude()
    {
        return transform.position.y;
    }

    public float GetFuel()
    {
        if (plane != null)
        {
            return plane.GetFuel();
        }
        return 0f;
    }

    public float GetHealth()
    {
        if (plane != null)
        {
            return plane.GetHealth();
        }
        return 0f;
    }

    public void SetAircraft(Transform newAircraft)
    {
        aircraft = newAircraft;
    }

    public void SetMouseAim(Transform newMouseAim)
    {
        mouseAim = newMouseAim;
    }

    public void SetCameraRig(Transform newCameraRig)
    {
        cameraRig = newCameraRig;
    }

    public void SetCam(Transform newCam)
    {
        cam = newCam;
    }

    public void SetUseFixed(bool newUseFixed)
    {
        useFixed = newUseFixed;
    }

    public void SetCamSmoothSpeed(float newCamSmoothSpeed)
    {
        camSmoothSpeed = newCamSmoothSpeed;
    }

    public void SetMouseSensitivity(float newMouseSensitivity)
    {
        mouseSensitivity = newMouseSensitivity;
    }

    public void SetAimDistance(float newAimDistance)
    {
        aimDistance = newAimDistance;
    }

    public Transform GetAircraft()
    {
        return aircraft;
    }

    public Transform GetMouseAim()
    {
        return mouseAim;
    }

    public Transform GetCameraRig()
    {
        return cameraRig;
    }

    public Transform GetCam()
    {
        return cam;
    }
}