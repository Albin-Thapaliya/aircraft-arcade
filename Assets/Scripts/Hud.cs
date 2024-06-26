﻿using UnityEngine;
using UnityEngine.UI;
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
    }

    public void SetReferenceFlightController(FlightController controller)
    {
        flightController = controller;
    }
}