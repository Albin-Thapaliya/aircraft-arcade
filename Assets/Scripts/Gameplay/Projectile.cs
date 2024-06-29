using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Tooltip("Speed of the projectile")] public float speed = 100f;
    [Tooltip("Damage dealt by the projectile")] public float damage = 10f;

    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Plane target = collision.gameObject.GetComponent<Plane>();
        if (target != null)
        {
            target.TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}