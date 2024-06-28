using UnityEngine;

public class ResourceSystem : MonoBehaviour
{
    public int gold;
    public int mana;

    void Start()
    {
        gold = 100;
        mana = 50;
    }

    public void ModifyGold(int amount)
    {
        gold += amount;
    }

    public void ModifyMana(int amount)
    {
        mana += amount;
    }

    public bool CanAfford(int costGold, int costMana)
    {
        return gold >= costGold && mana >= costMana;
    }

    public void SpendResources(int goldCost, int manaCost)
    {
        if (CanAfford(goldCost, manaCost))
        {
            gold -= goldCost;
            mana -= manaCost;
        }
    }
}
