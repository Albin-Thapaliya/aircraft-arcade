using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Plane : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private FlightController controller;
    public static Transform playerPlaneTransform;

    [Header("Physics")]
    public float thrust = 100f;
    public Vector3 turnTorque = new Vector3(90f, 25f, 45f);
    public float forceMult = 1000f;

    [Header("Autopilot")]
    public float sensitivity = 5f;
    public float aggressiveTurnAngle = 10f;

    [Header("Input")]
    [Range(-1f, 1f)] public float pitch = 0f;
    [Range(-1f, 1f)] public float yaw = 0f;
    [Range(-1f, 1f)] public float roll = 0f;

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
    public float fireRate = 1f;
    public bool useSmartTargeting = true;

    private Rigidbody rb;
    private bool isInvincible = false;
    private float originalSpeed;
    private float powerUpEndTime;
    private float nextFireTime = 0f;
    private float turnSpeed;
    private bool isAI;
    private bool pitchOverride;
    private bool rollOverride;
    private float autoYaw;
    private float autoPitch;
    private float autoRoll;
    private bool isTurbulent;
    private Vector3 baseSpeed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        originalSpeed = thrust;
        currentHealth = maxHealth;

        if (!isAI && controller == null)
            Debug.LogError($"{name}: Plane - Missing reference to FlightController!");
    }

    private void Update()
    {
        if (!isAI)
            HandleManualControl();
        else
            HandleAIControl();

        ConsumeFuel();
        HandleShooting();
        CheckPowerUpStatus();
        RegenerateHealth();
        HandleFlight();
    }

    private void HandleManualControl()
    {
        float keyboardRoll = Input.GetAxis("Horizontal");
        float keyboardPitch = Input.GetAxis("Vertical");

        if (Mathf.Abs(keyboardRoll) > 0.25f)
            rollOverride = true;

        if (Mathf.Abs(keyboardPitch) > 0.25f)
        {
            pitchOverride = true;
            rollOverride = true;
        }

        if (controller != null)
            RunAutopilot(controller.MouseAimPos, out autoYaw, out autoPitch, out autoRoll);

        yaw = autoYaw;
        pitch = pitchOverride ? keyboardPitch : autoPitch;
        roll = rollOverride ? keyboardRoll : autoRoll;
    }

    private Vector3 FindTargetPosition()
    {
        return playerPlaneTransform ? playerPlaneTransform.position : transform.position + transform.forward * 1000;
    }

    public void SetTurbulence(bool status)
    {
        isTurbulent = status;
    }

    private void HandleFlight()
    {
        Vector3 force = Vector3.zero;
        if (isTurbulent)
        {
            force += new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), Random.Range(-5f, 5f));
        }
        rb.AddForce(force);
    }

    private void HandleAIControl()
    {
        Vector3 targetPosition = FindTargetPosition();
        RunAutopilot(targetPosition, out autoYaw, out autoPitch, out autoRoll);

        pitch = autoPitch;
        yaw = autoYaw;
        roll = autoRoll;
    }

    private void RegenerateHealth()
    {
        if (currentHealth < maxHealth * 0.5f)
        {
            currentHealth += healthRegenRate * Time.deltaTime;
            currentHealth = Mathf.Min(currentHealth, maxHealth);
        }
    }

    private void ConsumeFuel()
    {
        if (fuel > 0)
        {
            fuel -= fuelConsumptionRate * Time.deltaTime;
            fuel = Mathf.Clamp(fuel, 0f, 100f);
        }
        else
        {
            thrust = 0f;
        }
    }

    private void CheckPowerUpStatus()
    {
        if (Time.time > powerUpEndTime)
            DeactivatePowerUp();
    }

    public void ActivatePowerUp(PowerUp.PowerUpType powerUpType, float duration)
    {
        powerUpEndTime = Time.time + duration;
        switch (powerUpType)
        {
            case PowerUp.PowerUpType.SpeedBoost:
                thrust *= 2;
                break;
            case PowerUp.PowerUpType.Invincibility:
                isInvincible = true;
                break;
            case PowerUp.PowerUpType.HealthRestore:
                currentHealth = Mathf.Min(currentHealth + 50f, maxHealth);
                break;
        }
    }

    private void DeactivatePowerUp()
    {
        thrust = originalSpeed;
        isInvincible = false;
    }

    public void TakeDamage(float amount)
    {
        if (isInvincible) return;
        currentHealth -= amount;
        if (currentHealth <= 0)
            DestroyPlane();
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
        if (projectilePrefab)
            Instantiate(projectilePrefab, transform.position + transform.forward, transform.rotation);
        if (laserParticlePrefab)
            Instantiate(laserParticlePrefab, transform.position + transform.forward, transform.rotation);
    }

    public void HandleShooting(Vector3 targetDirection)
    {
        if (Time.time >= nextFireTime)
        {
            if (useSmartTargeting)
            {
                Quaternion lookRotation = Quaternion.LookRotation(targetDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, turnSpeed * Time.deltaTime);
            }
            Shoot();
            nextFireTime = Time.time + 1f / fireRate;
        }
    }

    private void DestroyPlane()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        TakeDamage(10f);
    }

    private void RunAutopilot(Vector3 flyTarget, out float yaw, out float pitch, out float roll)
    {
        var localFlyTarget = transform.InverseTransformPoint(flyTarget).normalized * sensitivity;
        var angleOffTarget = Vector3.Angle(transform.forward, flyTarget - transform.position);

        yaw = Mathf.Clamp(localFlyTarget.x, -1f, 1f);
        pitch = -Mathf.Clamp(localFlyTarget.y, -1f, 1f);

        var aggressiveRoll = Mathf.Clamp(localFlyTarget.x, -1f, 1f);
        var wingsLevelRoll = transform.right.y;

        var wingsLevelInfluence = Mathf.InverseLerp(0f, aggressiveTurnAngle, angleOffTarget);
        roll = Mathf.Lerp(wingsLevelRoll, aggressiveRoll, wingsLevelInfluence);
    }

    private void FixedUpdate()
    {
        rb.AddRelativeForce(Vector3.forward * thrust * forceMult, ForceMode.Force);
        rb.AddRelativeTorque(new Vector3(turnTorque.x * pitch, turnTorque.y * yaw, -turnTorque.z * roll) * forceMult, ForceMode.Force);
    }

    internal float GetFuel()
    {
        throw new System.NotImplementedException();
    }

    internal float GetHealth()
    {
        throw new System.NotImplementedException();
    }
}