using UnityEngine;

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
        [SerializeField] [Range(-1f, 1f)] private float pitch = 0f;
        [SerializeField] [Range(-1f, 1f)] private float yaw = 0f;
        [SerializeField] [Range(-1f, 1f)] private float roll = 0f;

        public float Pitch { set { pitch = Mathf.Clamp(value, -1f, 1f); } get { return pitch; } }
        public float Yaw { set { yaw = Mathf.Clamp(value, -1f, 1f); } get { return yaw; } }
        public float Roll { set { roll = Mathf.Clamp(value, -1f, 1f); } get { return pitch; } }

        private Vector3 flyTarget = Vector3.zero;

        private Rigidbody rigid;

        private bool rollOverride = false;
        private bool pitchOverride = false;

        private void Awake()
        {
            rigid = GetComponent<Rigidbody>();
        }

        private void Update()
        {

            rollOverride = false;
            pitchOverride = false;

            float keyboardRoll = Input.GetAxis("Horizontal");
            if (Mathf.Abs(keyboardRoll) > .25f)
            {
                rollOverride = true;
            }

            float keyboardPitch = Input.GetAxis("Vertical");
            if (Mathf.Abs(keyboardPitch) > .25f)
            {
                pitchOverride = true;
                rollOverride = true;
            }

            float autoYaw, autoPitch, autoRoll = 0f;
            RunAutopilot(out autoYaw, out autoPitch, out autoRoll);

            yaw = autoYaw;
            pitch = (pitchOverride) ? keyboardPitch : autoPitch;
            roll = (rollOverride) ? keyboardRoll : autoRoll;
        }

        private void RunAutopilot(out float yaw, out float pitch, out float roll)
        {
            flyTarget = controller.MouseAimPos;
            var localFlyTarget = transform.InverseTransformPoint(flyTarget).normalized * sensitivity;
            var angleOffTarget = Vector3.Angle(transform.forward, flyTarget - transform.position);

            yaw = Mathf.Clamp(localFlyTarget.x, -1f, 1f);
            pitch = -Mathf.Clamp(localFlyTarget.y, -1f, 1f);

            var agressiveRoll = Mathf.Clamp(localFlyTarget.x, -1f, 1f);

            var wingsLevelRoll = transform.right.y;

            var wingsLevelInfluence = Mathf.InverseLerp(0f, aggressiveTurnAngle, angleOffTarget);
            roll = Mathf.Lerp(wingsLevelRoll, agressiveRoll, wingsLevelInfluence);
        }

        private void FixedUpdate()
        {
            rigid.AddRelativeForce(Vector3.forward * thrust * forceMult, ForceMode.Force);
            rigid.AddRelativeTorque(new Vector3(turnTorque.x * pitch, turnTorque.y * yaw, -turnTorque.z * roll) * forceMult, ForceMode.Force);
        }
    }