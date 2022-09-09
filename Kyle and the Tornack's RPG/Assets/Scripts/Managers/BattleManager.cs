using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance;

    public List<BaseUnit> AllUnits;
    public BaseUnit CurrentUnitTurn;

    [SerializeField] private GameObject IconTemplate;
    [SerializeField] private GameObject InitiativeOrderUI;
    
    //Unit Selection
    public BaseUnit SelectedUnit;
    public Color SelectedColor = new Color(88, 153, 255, 255);
    public Color UnitMovedColor = new Color(103, 103, 103, 255);
    public Color DefaultColor = new Color(255, 255, 255, 255);

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
    }

    public void CurrentTurn(BaseUnit Unit)
    {
        CurrentUnitTurn = Unit;

        //Run player turn
        if(Unit.faction == Faction.Hero)
        {
            CurrentUnitTurn.CurrentTurn = true;
            //We need to do a few things
            //#1 Display Current unit Turn, and the next turns in the initiative somewhere on the screen DONE
            //#2 When the unit is selected, shows a few ui buttons for movement, attacking or special moves.
            //#3 Allow the player to actually move the unit and make it take an action.
            //#4 After taking an action, the turn ends and the next turn begins, with this Current turn function looping and changing the initiative order.
        }
        //Run Enemy AI turn
        else if (Unit.faction == Faction.Enemy)
        {

        }

    }

    public void NextTurn(BaseUnit PrevUnit)
    {
        PrevUnit.InitIcon.CurrentActiveTurn();
        PrevUnit.CurrentTurn = false;
        AllUnits.RemoveAt(0);
        AllUnits.Add(PrevUnit);
        AllUnits[0].InitIcon.CurrentActiveTurn();
        CurrentTurn(AllUnits[0]);
    }
    public void SetSelectedUnit(BaseUnit unit)
    {
        if (SelectedUnit != null)
        {
            UnselectUnit();
        }
        SelectedUnit = unit;
        SelectedUnit.gameObject.GetComponent<SpriteRenderer>().color = SelectedColor;

        if(SelectedUnit == CurrentUnitTurn)
        {
            //Do Turn shit 
        }
    }
    public void UnselectUnit()
    {
        SelectedUnit.gameObject.GetComponent<SpriteRenderer>().color = DefaultColor;
        SelectedUnit = null;
    }

    public void CreateInitiativeOrder()
    {
        //Sorts the list by speed value
        AllUnits.Sort((u1, u2) => u2.Speed.CompareTo(u1.Speed));
        InitiativeOrderUI.SetActive(true);

        for(int i = 0; i < AllUnits.Count; i++)
        {
            GameObject InitIcon = Instantiate(IconTemplate);
            InitIcon.SetActive(true);

            AllUnits[i].InitIcon = InitIcon.GetComponent<InitiativeDisplay>();
            InitIcon.name = AllUnits[i].name + " Initiative Icon";
            InitIcon.GetComponent<InitiativeDisplay>().SetIcon(AllUnits[i].UnitData.CharPortrait, AllUnits[i]);
            InitIcon.transform.SetParent(IconTemplate.transform.parent, false);
        }

        AllUnits[0].InitIcon.CurrentActiveTurn();
        CurrentUnitTurn = AllUnits[0];
    }
}
