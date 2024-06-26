using UnityEngine;

public class AIPlane : MonoBehaviour
{
    [Header("Waypoints")]
    [SerializeField] private Transform[] waypoints;
    private int currentWaypointIndex = 0;

    [Header("Flight Parameters")]
    [Tooltip("Speed of the AI plane")] public float speed = 50f;
    [Tooltip("Turning speed of the AI plane")] public float turnSpeed = 2f;
    [Tooltip("Target for the AI plane to attack")] public Transform target;
    [Tooltip("Avoidance radius for obstacles")] public float avoidanceRadius = 10f;

    private Plane plane;

    private void Awake()
    {
        plane = GetComponent<Plane>();
        if (plane == null)
            Debug.LogError($"{name}: AIPlane - Missing Plane component!");
    }

    private void Update()
    {
        if (waypoints.Length == 0) return;

        Transform targetWaypoint = waypoints[currentWaypointIndex];
        Vector3 direction = (targetWaypoint.position - transform.position).normalized;
        float distance = Vector3.Distance(transform.position, targetWaypoint.position);

        AvoidObstacles(ref direction);

        transform.position += direction * speed * Time.deltaTime;

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);

        if (distance < 10f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }

        if (target != null)
        {
            Vector3 targetDirection = (target.position - transform.position).normalized;
            plane.HandleShooting(targetDirection);
        }
    }

    private void AvoidObstacles(ref Vector3 direction)
    {
        RaycastHit hit;
        if (Physics.SphereCast(transform.position, avoidanceRadius, transform.forward, out hit, avoidanceRadius))
        {
            direction += hit.normal * avoidanceRadius;
        }
    }
}