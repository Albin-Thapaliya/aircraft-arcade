using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public int health = 100;
    public int moveRange = 5;
    public int attackRange = 3;
    public int attackDamage = 20;

    public bool Move(Vector3 position)
    {
        return true;
    }

    public void Attack(UnitManager target)
    {
        target.TakeDamage(attackDamage);
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
            Die();
    }

    void Die()
    {
        gameObject.SetActive(false);
    }
}
