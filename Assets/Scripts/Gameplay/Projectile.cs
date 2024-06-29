using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Tooltip("Speed of the projectile")]
    public float speed = 100f;
    
    [Tooltip("Damage dealt by the projectile")]
    public float damage = 10f;
    
    [Tooltip("Lifetime of the projectile in seconds")]
    public float lifetime = 5f;
    
    [Tooltip("Direction of the projectile movement relative to the local forward vector")]
    public Vector3 direction = Vector3.forward;

    private float spawnTime;

    private void Start()
    {
        spawnTime = Time.time;
    }

    private void Update()
    {
        transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);
        
        if (Time.time > spawnTime + lifetime)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Plane target = collision.gameObject.GetComponent<Plane>();
        if (target)
        {
            target.TakeDamage(damage);
        }
        
        Destroy(gameObject);
    }
}
