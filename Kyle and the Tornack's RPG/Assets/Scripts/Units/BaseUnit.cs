using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUnit : MonoBehaviour
{
    public Faction faction;
    public ScriptableUnit UnitData;

    public Tile OccupiedTile;

    public float Vigor, Stamina, Strength, Skill, Intelligence, Faith, Willpower;
    public int speed;

    public void SetStats()
    {
        Vigor = UnitData.BaseVigor;
        Stamina = UnitData.BaseStamina;
        Strength = UnitData.BaseStrength;
        Skill = UnitData.BaseSkill;
        Intelligence = UnitData.BaseIntelligence;
        Faith = UnitData.BaseFaith;
        Willpower = UnitData.BaseWillpower;
    }
}
