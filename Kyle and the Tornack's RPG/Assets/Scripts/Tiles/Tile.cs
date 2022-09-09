using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
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
        if(GameManager.Instance.state == GameManager.GameState.SelectUnits)
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
                //NO AND THERE IS A UNIT ON THIS TILE
                if (OccupiedUnit.faction == Faction.Hero)
                {
                    //Unit on this tile is a hero
                    BattleSetupManager.Instance.SetSelectedUnit((BaseUnit)OccupiedUnit);
                }
                else if (OccupiedUnit.faction == Faction.Enemy)
                {
                    //Unit on this tile is an enemy
                    BattleSetupManager.Instance.SetSelectedUnit(OccupiedUnit);
                }
            }
        }

        //Player Turn Movement
        else if(GameManager.Instance.state == GameManager.GameState.CombatLoop)
        {
            //Checks to see if a unit is currently selected...
            if (BattleManager.Instance.SelectedUnit != null && BattleSetupManager.Instance.SelectedUnit)
            {

            }
            //No unit currently selected
            else
            {
                //If Occupied Unit is equal to a hero
                if(OccupiedUnit != null && OccupiedUnit.faction == Faction.Hero)
                {
                    BattleManager.Instance.SetSelectedUnit(OccupiedUnit);
                }
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
