﻿using UnityEngine;
using TMPro;

public class Hud : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private FlightController flightController = null;

    [Header("HUD Elements")]
    [SerializeField] private RectTransform boresight = null;
    [SerializeField] private RectTransform mousePos = null;
    [SerializeField] private TextMeshProUGUI speedText = null;
    [SerializeField] private TextMeshProUGUI altitudeText = null;
    [SerializeField] private TextMeshProUGUI fuelText = null;
    [SerializeField] private TextMeshProUGUI healthText = null;

    private void Update()
    {
        if (flightController == null)
            return;

        UpdateGraphics();
        UpdateFlightData();
    }

    private void UpdateGraphics()
    {
        if (boresight != null)
        {
            boresight.position = Camera.main.WorldToScreenPoint(flightController.BoresightPos);
            boresight.gameObject.SetActive(boresight.position.z > 1f);
        }

        if (mousePos != null)
        {
            mousePos.position = Camera.main.WorldToScreenPoint(flightController.MouseAimPos);
            mousePos.gameObject.SetActive(mousePos.position.z > 1f);
        }
    }

    private void UpdateFlightData()
    {
        if (speedText != null)
        {
            float speed = flightController.GetSpeed();
            speedText.text = $"Speed: {speed:F2} m/s";
        }

        if (altitudeText != null)
        {
            float altitude = flightController.GetAltitude();
            altitudeText.text = $"Altitude: {altitude:F2} m";
        }

        if (fuelText != null)
        {
            float fuel = flightController.GetFuel();
            fuelText.text = $"Fuel: {fuel:F2}%";
        }

        if (healthText != null)
        {
            float health = flightController.GetHealth();
            healthText.text = $"Health: {health:F2}%";
        }
    }

    public void SetReferenceFlightController(FlightController controller)
    {
        flightController = controller;
    }

    public void ClearReferenceFlightController()
    {
        flightController = null;
    }

    public void SetBoresightActive(bool active)
    {
        if (boresight != null)
            boresight.gameObject.SetActive(active);
    }

    public void SetMousePosActive(bool active)
    {
        if (mousePos != null)
            mousePos.gameObject.SetActive(active);
    }

    public void SetSpeedTextActive(bool active)
    {
        if (speedText != null)
            speedText.gameObject.SetActive(active);
    }

    public void SetAltitudeTextActive(bool active)
    {
        if (altitudeText != null)
            altitudeText.gameObject.SetActive(active);
    }

    public void SetFuelTextActive(bool active)
    {
        if (fuelText != null)
            fuelText.gameObject.SetActive(active);
    }

    public void SetHealthTextActive(bool active)
    {
        if (healthText != null)
            healthText.gameObject.SetActive(active);
    }

    public void SetBoresightPosition(Vector3 position)
    {
        if (boresight != null)
            boresight.position = position;
    }

    public void SetMousePosPosition(Vector3 position)
    {
        if (mousePos != null)
            mousePos.position = position;
    }

    public void SetSpeedText(string text)
    {
        if (speedText != null)
            speedText.text = text;
    }

    public void SetAltitudeText(string text)
    {
        if (altitudeText != null)
            altitudeText.text = text;
    }
}