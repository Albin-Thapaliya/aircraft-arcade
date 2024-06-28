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
    [SerializeField] private TextMeshProUGUI weaponText = null;
    [SerializeField] private TextMeshProUGUI powerUpText = null;

    private void Update()
    {
        if (flightController == null)
            return;

        UpdateFlightData();
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

        if (weaponText != null)
        {
            string weaponType = flightController.GetCurrentWeapon();
            weaponText.text = $"Weapon: {weaponType}";
        }

        if (powerUpText != null)
        {
            string powerUpStatus = flightController.GetPowerUpStatus();
            powerUpText.text = $"PowerUp: {powerUpStatus}";
        }
    }
}