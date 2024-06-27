using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public enum PowerUpType { SpeedBoost, Invincibility, HealthRestore, FuelRestore }
    public PowerUpType powerUpType;
    public float duration = 5f;

    private void OnTriggerEnter(Collider other)
    {
        Plane plane = other.GetComponent<Plane>();
        if (plane != null)
        {
            plane.ActivatePowerUp(powerUpType, duration);
            Destroy(gameObject);
        }
    }
}