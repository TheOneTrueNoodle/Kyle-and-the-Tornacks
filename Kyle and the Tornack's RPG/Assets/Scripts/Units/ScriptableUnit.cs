using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Unit", menuName = "ScriptableUnit")]
public class ScriptableUnit : ScriptableObject
{
    public Faction Faction;
    public BaseUnit UnitPrefab;

    public bool SkullToken;
    public bool DeadorInjured;
    public string CharName;
    public Sprite CharPortrait;

    public float BaseVigor, BaseStamina, BasePower, BaseSkill, BaseArcane, BaseWill;
}

public enum Faction
{
    Hero = 0,
    Enemy = 1
}
