using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public enum PowerUpType { SpeedBoost, Invincibility, HealthRestore }
    public PowerUpType powerUpType;
    public float duration = 5f;

    private void OnTriggerEnter(Collider other)
    {
        Plane plane = other.GetComponent<Plane>();
        if (plane != null)
        {
            plane.ActivatePowerUp(powerUpType, duration);
            Destroy(gameObject);

            if (powerUpType == PowerUpType.SpeedBoost)
                Debug.Log("Speed Boost Power Up Collected!");
            else if (powerUpType == PowerUpType.Invincibility)
                Debug.Log("Invincibility Power Up Collected!");
            else if (powerUpType == PowerUpType.HealthRestore)
                Debug.Log("Health Restore Power Up Collected!");

            Debug.Log("Power Up Collected!");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 1f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 1f);
    }
}