using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Plane : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private FlightController controller = null;

    [Header("Physics")]
    [Tooltip("Force to push plane forwards with")] public float thrust = 100f;
    [Tooltip("Pitch, Yaw, Roll")] public Vector3 turnTorque = new Vector3(90f, 25f, 45f);
    [Tooltip("Multiplier for all forces")] public float forceMult = 1000f;

    [Header("Autopilot")]
    [Tooltip("Sensitivity for autopilot flight.")] public float sensitivity = 5f;
    [Tooltip("Angle at which airplane banks fully into target.")] public float aggressiveTurnAngle = 10f;

    [Header("Input")]
    [SerializeField][Range(-1f, 1f)] private float pitch = 0f;
    [SerializeField][Range(-1f, 1f)] private float yaw = 0f;
    [SerializeField][Range(-1f, 1f)] private float roll = 0f;

    [Header("Fuel System")]
    [Tooltip("Initial fuel amount in percentage")] public float fuel = 100f;
    [Tooltip("Fuel consumption rate per second")] public float fuelConsumptionRate = 0.1f;

    [Header("Weapons")]
    [Tooltip("Projectile prefab to shoot")] public GameObject projectilePrefab;
    [Tooltip("Particle system prefab for laser effect")] public GameObject laserParticlePrefab;
    [Tooltip("Fire rate in shots per second")] public float fireRate = 1f;

    private Rigidbody rb;
    private bool rollOverride = false;
    private bool pitchOverride = false;
    private float nextFireTime = 0f;
    private float autoYaw;
    private float autoPitch;
    private float autoRoll;


    public float Pitch
    {
        get { return pitch; }
        set { pitch = Mathf.Clamp(value, -1f, 1f); }
    }
    public float Yaw
    {
        get { return yaw; }
        set { yaw = Mathf.Clamp(value, -1f, 1f); }
    }
    public float Roll
    {
        get { return roll; }
        set { roll = Mathf.Clamp(value, -1f, 1f); }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        if (controller == null)
            Debug.LogError($"{name}: Plane - Missing reference to FlightController!");
    }

    private void Update()
    {
        rollOverride = false;
        pitchOverride = false;

        float keyboardRoll = Input.GetAxis("Horizontal");
        if (Mathf.Abs(keyboardRoll) > 0.25f)
            rollOverride = true;

        float keyboardPitch = Input.GetAxis("Vertical");
        if (Mathf.Abs(keyboardPitch) > 0.25f)
        {
            pitchOverride = true;
            rollOverride = true;
        }

        if (controller != null)
            RunAutopilot(controller.MouseAimPos, out float autoYaw, out float autoPitch, out float autoRoll);

        yaw = autoYaw;
        pitch = pitchOverride ? keyboardPitch : autoPitch;
        roll = rollOverride ? keyboardRoll : autoRoll;

        ConsumeFuel();
        HandleShooting();
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

    public float GetFuel()
    {
        return fuel;
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
        if (laserParticlePrefab != null)
        {
            Instantiate(laserParticlePrefab, transform.position + transform.forward, transform.rotation);
        }
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
}