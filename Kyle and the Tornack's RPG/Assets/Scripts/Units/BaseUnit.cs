using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUnit : MonoBehaviour
{
    [Header("Faction & Data")]
    public Faction faction;
    public ScriptableUnit UnitData;
    [Space]
    public Tile OccupiedTile;
    [Space]
    [Header("Stats")]
    public float Vigor;
    public float Stamina;
    public float Power;
    public float Skill;
    public float Arcane;
    public float Will;
    public int MoveSpeed;
    public int RemainingMovement;
    public int Init;
    [Space]
    [Header("Visuals")]
    public InitiativeDisplay InitIcon;
    public Color DefaultColor, SelectedColor, HighlightedColor;

    //Gameplay Values
    [Space]
    [Header("Gameplay Values")]
    public bool CurrentTurn = false;

    public void SetStats()
    {
        Vigor = UnitData.BaseVigor;
        Stamina = UnitData.BaseStamina;
        Power = UnitData.BasePower;
        Skill = UnitData.BaseSkill;
        Arcane = UnitData.BaseArcane;
        Will = UnitData.BaseWill;

        MoveSpeed = 2 + (int)Skill;
    }

    public void UnitSelected()
    {
        gameObject.GetComponent<SpriteRenderer>().color = SelectedColor;
    }
    public void UnitDeselected()
    {
        gameObject.GetComponent<SpriteRenderer>().color = DefaultColor;
    }
}
