using UnityEngine;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour
{
    public int health = 100;
    public int maxHealth = 100;
    public int mana = 50;
    public int maxMana = 50;
    public int experience = 0;
    public int level = 1;

    private const int ExperiencePerLevel = 100;
    private List<Item> inventory = new List<Item>();
    private Dictionary<EquipmentType, Equipment> equipment = new Dictionary<EquipmentType, Equipment>();
    private List<Skill> skills = new List<Skill>();

    void Start()
    {
        AddItem(new Item("Health Potion"));
        AddItem(new Item("Mana Potion"));
        AddSkill(new Skill("Fireball", 10));
    }

    public void AddExperience(int amount)
    {
        experience += amount;
        CheckLevelUp();
    }

    private void CheckLevelUp()
    {
        while (experience >= ExperiencePerLevel * level)
        {
            experience -= ExperiencePerLevel * level;
            level++;
            LevelUp();
        }
    }

    private void LevelUp()
    {
        maxHealth += 20;
        health = maxHealth;
        maxMana += 10;
        mana = maxMana;
        Debug.Log($"Player leveled up to {level}! Health: {maxHealth}, Mana: {maxMana}");
        GiveLevelUpReward();
    }

    private void GiveLevelUpReward()
    {
        AddSkill(new Skill("Heal", level));
    }

    public void AddItem(Item item)
    {
        inventory.Add(item);
        Debug.Log($"Added {item.name} to inventory.");
    }

    public void UseItem(string itemName)
    {
        Item item = inventory.Find(i => i.name == itemName);
        if (item != null)
        {
            ApplyItemEffects(item);
            inventory.Remove(item);
        }
    }

    private void ApplyItemEffects(Item item)
    {
        switch (item.name)
        {
            case "Health Potion":
                health = Mathf.Min(health + 50, maxHealth);
                Debug.Log("Used Health Potion: +50 Health");
                break;
            case "Mana Potion":
                mana = Mathf.Min(mana + 30, maxMana);
                Debug.Log("Used Mana Potion: +30 Mana");
                break;
        }
    }

    public void EquipItem(Equipment equipment)
    {
        if (this.equipment.ContainsKey(equipment.type))
        {
            this.equipment[equipment.type] = equipment;
            Debug.Log($"Equipped {equipment.name}");
        }
    }

    public void AddSkill(Skill skill)
    {
        skills.Add(skill);
        Debug.Log($"Learned new skill: {skill.name}");
    }

    public void UseSkill(string skillName, PlayerManager target)
    {
        Skill skill = skills.Find(s => s.name == skillName);
        if (skill != null && mana >= skill.manaCost)
        {
            mana -= skill.manaCost;
            skill.Activate(target);
        }
    }
}

public class Item
{
    public string name;

    public Item(string name)
    {
        this.name = name;
    }
}

public class Equipment
{
    public string name;
    public EquipmentType type;

    public Equipment(string name, EquipmentType type)
    {
        this.name = name;
        this.type = type;
    }
}

public enum EquipmentType
{
    Weapon,
    Armor,
    Shield,
    Helmet
}

public class Skill
{
    public string name;
    public int manaCost;

    public Skill(string name, int manaCost)
    {
        this.name = name;
        this.manaCost = manaCost;
    }

    public void Activate(PlayerManager target)
    {
        Debug.Log($"Used {name} on {target.gameObject.name}");
    }
}
