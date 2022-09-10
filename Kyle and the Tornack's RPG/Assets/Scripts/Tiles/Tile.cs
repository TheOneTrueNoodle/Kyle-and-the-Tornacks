using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Vector2 Pos;

    [SerializeField] private SpriteRenderer Renderer;

    private Color CurrentDefaultColor;
    [SerializeField] private Color GridViewColor;
    [SerializeField] private Color highlightColor;
    public Color HeroHighlightColor;
    public Color EnemyHighlightColor;

    public bool obstacle;

    public bool StartingLocation;

    public BaseUnit OccupiedUnit;
    public bool Walkable => obstacle == false && OccupiedUnit == null;
    public bool MoveSquare;

    [HideInInspector] public bool GameStarted = false;

    private void OnMouseEnter()
    {
        Renderer.enabled = true;

        if(OccupiedUnit != null)
        {
            if(OccupiedUnit.faction == Faction.Hero)
            {
                Renderer.color = HeroHighlightColor;
            }
            else if(OccupiedUnit.faction == Faction.Enemy)
            {
                Renderer.color = EnemyHighlightColor;
            }
        }
        else
        {
            Renderer.color = highlightColor;
        }
    }

    private void OnMouseExit()
    {
        if(MoveSquare != true)
        {
        if (GameManager.Instance.state == GameManager.GameState.SelectUnits)
        {
            Renderer.color = GridViewColor;
        }
        else if (GameManager.Instance.state == GameManager.GameState.SelectStartPositions)
            {
            Renderer.color = GridViewColor;
        }
        else
        {
            Renderer.enabled = false;
        }
    }
    }

    private void OnMouseDown()
    {
        //Choosing Starting Positions
        if (GameManager.Instance.state == GameManager.GameState.SelectStartPositions)
        {
            //Is a unit selected? (Moving selected unit)
            if (BattleSetupManager.Instance.SelectedUnit != null && BattleSetupManager.Instance.SelectedUnit.faction == Faction.Hero && StartingLocation == true)
            {
                //YES
                //If there is a unit on this tile
                if (OccupiedUnit != null)
                {
                    //Swap both heroes...
                    var SelUnit = BattleSetupManager.Instance.SelectedUnit;
                    var OldUnit = OccupiedUnit;

                    OldUnit.transform.position = SelUnit.OccupiedTile.transform.position;
                    SelUnit.OccupiedTile.OccupiedUnit = OldUnit;
                    OldUnit.OccupiedTile = SelUnit.OccupiedTile;

                    SelUnit.transform.position = transform.position;
                    SelUnit.OccupiedTile = this;
                    OccupiedUnit = SelUnit;

                    BattleSetupManager.Instance.UnselectUnit();
                }
                //If there is not another unit on this tile
                else
                {
                    //Move hero to here...
                    var SelUnit = BattleSetupManager.Instance.SelectedUnit;
                    SelUnit.transform.position = transform.position;
                    SelUnit.OccupiedTile.OccupiedUnit = null;
                    SelUnit.OccupiedTile = this;
                    OccupiedUnit = SelUnit;
                    BattleSetupManager.Instance.UnselectUnit();
                }
            }

            else if (OccupiedUnit != null)
            {
                BattleSetupManager.Instance.SetSelectedUnit(OccupiedUnit);
            }
        }

        //Combat Loop Selection
        else if(GameManager.Instance.state == GameManager.GameState.CombatLoop)
        {
            //Checks to see if a unit is currently selected...
            if (BattleManager.Instance.SelectedUnit != null)
            {
                if(OccupiedUnit != null)
                {
                    BattleManager.Instance.SetSelectedUnit(OccupiedUnit);
                }
                else if(BattleManager.Instance.SelectedUnit != BattleManager.Instance.CurrentUnitTurn)
                {
                    BattleManager.Instance.UnselectUnit();
                }
            }
            //No unit currently selected
            else if(OccupiedUnit != null)
            {
                BattleManager.Instance.SetSelectedUnit(OccupiedUnit);
            }
        }
    }

    public void SetUnit(BaseUnit unit)
    {
        if (unit.OccupiedTile != null)
        {
            unit.OccupiedTile.OccupiedUnit = null;
        }
    unit.transform.position = transform.position;
    OccupiedUnit = unit;
    unit.OccupiedTile = this;
    }
}
