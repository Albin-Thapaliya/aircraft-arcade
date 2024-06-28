using UnityEngine;
using System.Collections.Generic;

public class AIManager : MonoBehaviour
{
    public List<UnitManager> aiUnits;

    public void ProcessAITurn()
    {
        foreach (var unit in aiUnits)
        {
            unit.Move(ChooseBestMove(unit));
            UnitManager target = FindTarget(unit);
            if (target != null)
                unit.Attack(target);
        }
    }

    Vector3 ChooseBestMove(UnitManager unit)
    {
        return Vector3.zero;
    }

    UnitManager FindTarget(UnitManager unit)
    {
        return null; 
    }
}
