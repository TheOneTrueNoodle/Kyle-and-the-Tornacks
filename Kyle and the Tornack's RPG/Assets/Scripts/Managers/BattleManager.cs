using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance;

    public List<BaseUnit> AllUnits;
    public BaseUnit CurrentUnitTurn;

    [SerializeField] private GameObject IconTemplate;
    [SerializeField] private List<GameObject> InitIcons;
    [SerializeField] private GameObject InitiativeOrderUI;
    
    //Unit Selection
    public BaseUnit SelectedUnit;
    public List<Tile> MovableTiles;

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
        CurrentUnitTurn.RemainingMovement = CurrentUnitTurn.MoveSpeed;

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
        SelectedUnit.UnitSelected();

        if (SelectedUnit == CurrentUnitTurn)
        {
            //Now, lets find every tile within the move distance of the selected unit
            float UnitX = SelectedUnit.OccupiedTile.transform.position.x;
            float UnitY = SelectedUnit.OccupiedTile.transform.position.y;

            for(float x = UnitX; x <= UnitX + SelectedUnit.RemainingMovement; x++)
            {
                for(float y = UnitY; y <= UnitY + SelectedUnit.RemainingMovement; y++)
                {
                    //Check if position is walkable?
                    Debug.Log(x + " " + y);
                    Tile tile = GridManager.Instance.GetTileAtPosition(new Vector2(x, y));
                    if ((tile != null) && tile.Walkable)
                    {
                        //This tile can be moved too..
                        //ADD PATHFINDING TO SEE IF THERE IS A PATH TO TILE
                        //ADD TO SEE IF PATHFINDING IS TOO FAR AWAY.
                        SpriteRenderer renderer = tile.GetComponent<SpriteRenderer>();
                        renderer.color = tile.HeroHighlightColor;
                        renderer.enabled = true;

                        tile.MoveSquare = true;
                        MovableTiles.Add(tile);
                    }
                }
            }

        }
    }
    public void UnselectUnit()
    {
        SelectedUnit.UnitDeselected();

        if(SelectedUnit == CurrentUnitTurn)
        {

        }

        SelectedUnit = null;
    }

    public void CreateInitiativeOrder()
    {
        //Sorts the list by speed value
        AllUnits.Sort((u1, u2) => u2.Init.CompareTo(u1.Init));
        InitiativeOrderUI.SetActive(true);

        for(int i = 0; i < AllUnits.Count; i++)
        {
            GameObject InitIcon = Instantiate(IconTemplate);
            InitIcon.SetActive(true);

            AllUnits[i].InitIcon = InitIcon.GetComponent<InitiativeDisplay>();
            InitIcon.name = AllUnits[i].name + " Initiative Icon";
            InitIcon.GetComponent<InitiativeDisplay>().SetIcon(AllUnits[i].UnitData.CharPortrait, AllUnits[i]);
            InitIcon.transform.SetParent(IconTemplate.transform.parent, false);
            InitIcons.Add(InitIcon);
        }

        AllUnits[0].InitIcon.CurrentActiveTurn();
        CurrentUnitTurn = AllUnits[0];
        CurrentUnitTurn.RemainingMovement = CurrentUnitTurn.MoveSpeed;
    }
}
