public class BattleSystem : MonoBehaviour
{
    public void StartBattle(UnitManager attacker, UnitManager defender)
    {
        if (IsInRange(attacker, defender))
        {
            defender.TakeDamage(attacker.attackDamage);
        }
    }

    bool IsInRange(UnitManager attacker, UnitManager defender)
    {
        return Vector3.Distance(attacker.transform.position, defender.transform.position) <= attacker.attackRange;
    }
}
