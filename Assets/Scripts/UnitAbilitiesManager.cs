using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAbilitiesManager : MonoBehaviour
{
    public List<Ability> abilities = new List<Ability>();

    public void AddAbility(Ability ability)
    {
        abilities.Add(ability);
    }

    public void ActivateAbility(int index, UnitManager targetUnit)
    {
        if (index >= 0 && index < abilities.Count)
        {
            abilities[index].Activate(targetUnit);
        }
    }
}

[System.Serializable]
public class Ability
{
    public string name;
    public int cooldown;
    private int currentCooldown;

    public virtual void Activate(UnitManager target)
    {
        if (currentCooldown == 0)
        {
            Execute(target);
            currentCooldown = cooldown;
        }
    }

    protected virtual void Execute(UnitManager target)
    {
        Debug.Log("Ability not implemented");
    }

    public void CooldownUpdate()
    {
        if (currentCooldown > 0)
            currentCooldown--;
    }
}

public class HealAbility : Ability
{
    public int healAmount;

    public HealAbility(int healAmount)
    {
        name = "Heal";
        cooldown = 3;
        this.healAmount = healAmount;
    }

    protected override void Execute(UnitManager target)
    {
        target.RestoreHealth(healAmount);
    }
}

public class DamageAbility : Ability
{
    public int damage;

    public DamageAbility(int damage)
    {
        name = "Damage";
        cooldown = 2;
        this.damage = damage;
    }

    protected override void Execute(UnitManager target)
    {
        target.TakeDamage(damage);
    }
}
