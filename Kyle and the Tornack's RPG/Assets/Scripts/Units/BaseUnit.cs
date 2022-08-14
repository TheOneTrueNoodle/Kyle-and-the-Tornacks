using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUnit : MonoBehaviour
{
    public Faction faction;
    public ScriptableUnit UnitData;

    public Tile OccupiedTile;

    public float BaseVigor, BaseStamina, BaseStrength, BaseSkill, BaseIntelligence, BaseFaith, BaseWillpower;

    public void SetStats()
    {
        BaseVigor = UnitData.BaseVigor;
        BaseStamina = UnitData.BaseStamina;
        BaseStrength = UnitData.BaseStrength;
        BaseSkill = UnitData.BaseSkill;
        BaseIntelligence = UnitData.BaseIntelligence;
        BaseFaith = UnitData.BaseFaith;
        BaseWillpower = UnitData.BaseWillpower;
    }
}
