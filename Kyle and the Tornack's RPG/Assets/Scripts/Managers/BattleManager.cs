using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance;

    public List<BaseUnit> AllUnits;

    private bool UnitsFound;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        CreateInitiativeOrder();
    }

    public void CreateInitiativeOrder()
    {
        //Sorts the list by speed value
        AllUnits.Sort((u1, u2) => u2.speed.CompareTo(u1.speed));
    }
}
