using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Plane : MonoBehaviour
{
    public float speed = 50f;
    public float fireRate = 1f;

    [Header("Components")]
    [SerializeField] private FlightController controller = null;

    [Header("Physics")]
    public float thrust = 100f;
    public Vector3 turnTorque = new Vector3(90f, 25f, 45f);
    public float forceMult = 1000f;

    [Header("Fuel System")]
    public float fuel = 100f;
    public float fuelConsumptionRate = 0.1f;

    [Header("Health System")]
    public float maxHealth = 100f;
    public float healthRegenRate = 0.05f;
    private float currentHealth;

    [Header("Weapons")]
    public GameObject projectilePrefab;
    public GameObject laserParticlePrefab;
    private float nextFireTime = 0f;
    private string currentWeapon = "Standard";
    private string powerUpStatus = "None";

    private Rigidbody rb;
    private bool isInvincible = false;
    private float originalSpeed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        originalSpeed = thrust;
        currentHealth = maxHealth;
    }

    private void Update()
    {
        ConsumeFuel();
        HandleShooting();
    }

    private void HandleShooting()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + 1f / fireRate;
        }
    }

    private void Shoot()
    {
        if (projectilePrefab != null)
        {
            Instantiate(projectilePrefab, transform.position + transform.forward, transform.rotation);
        }
    }

    public float GetFuel()
    {
        return fuel;
    }

    public float GetHealth()
    {
        return currentHealth;
    }

    public string GetCurrentWeapon()
    {
        return currentWeapon;
    }

    public void SetCurrentWeapon(string weaponType)
    {
        currentWeapon = weaponType;
    }

    public string GetPowerUpStatus()
    {
        return powerUpStatus;
    }

    public void SetPowerUpStatus(string status)
    {
        powerUpStatus = status;
    }

    private void ConsumeFuel()
    {
        if (fuel > 0)
        {
            fuel -= fuelConsumptionRate * Time.deltaTime;
            fuel = Mathf.Clamp(fuel, 0, 100);
        }
        else
        {
            thrust = 0;
        }
    }
}