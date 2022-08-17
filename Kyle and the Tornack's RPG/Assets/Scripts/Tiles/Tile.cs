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
            //The player has already selected another unit
            if (BattleSetupManager.Instance.SelectedUnit != null && BattleSetupManager.Instance.SelectedUnit.faction == Faction.Hero && StartingLocation == true)
            {
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

            //The Player has not selected another unit

            //The unit on this tile is a Hero...
            else if (OccupiedUnit != null && OccupiedUnit.faction == Faction.Hero)
            {
                BattleSetupManager.Instance.SetSelectedUnit((BaseUnit)OccupiedUnit);
            }

            //The unit on this tile is an Enemy...
            else if (OccupiedUnit != null && OccupiedUnit.faction == Faction.Enemy)
            {
                BattleSetupManager.Instance.SetSelectedUnit((BaseUnit)OccupiedUnit);
            }
        }

        //Player Turn Movement
        else if(GameManager.Instance.state == GameManager.GameState.CombatLoop)
        {
            if (BattleSetupManager.Instance.SelectedUnit != null && BattleSetupManager.Instance.SelectedUnit.faction == Faction.Hero && StartingLocation == true)
            {

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
