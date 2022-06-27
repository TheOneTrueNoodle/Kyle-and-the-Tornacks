using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private SpriteRenderer Renderer;
    [SerializeField] private Color GridViewColor;
    [SerializeField] private Color highlightColor;

    public bool obstacle;

    public bool StartingLocation;
    [SerializeField] private Color StartingLocationColor;

    public BaseUnit OccupiedUnit;
    public bool Walkable => obstacle == false && OccupiedUnit == null;

    [HideInInspector] public bool GameStarted = false;

    private void OnMouseEnter()
    {
        Renderer.enabled = true;
        Renderer.color = highlightColor;
    }

    private void OnMouseExit()
    {
        if(GameStarted == false)
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
        if(GameManager.Instance.state == GameManager.GameState.SelectStartPositions)
        {
            if (OccupiedUnit != null)
            {
                if (BattleSetupManager.Instance.SelectedUnit != null && BattleSetupManager.Instance.SelectedUnit.faction == Faction.Hero && StartingLocation == true)
                {
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

                        BattleSetupManager.Instance.SelectedUnit = null;
                    }
                    else
                    {
                        //Move hero to here...
                        var SelUnit = BattleSetupManager.Instance.SelectedUnit;
                        SelUnit.transform.position = transform.position;
                        SelUnit.OccupiedTile.OccupiedUnit = null;
                        SelUnit.OccupiedTile = this;
                        OccupiedUnit = SelUnit;
                        BattleSetupManager.Instance.SelectedUnit = null;
                    }
                }
                else if (OccupiedUnit.faction == Faction.Hero)
                {
                    BattleSetupManager.Instance.SetSelectedUnit((BaseUnit)OccupiedUnit);
                }
                else if (OccupiedUnit.faction == Faction.Enemy)
                {
                    BattleSetupManager.Instance.SetSelectedUnit((BaseUnit)OccupiedUnit);
                }
            }
            else
            {
                if (BattleSetupManager.Instance.SelectedUnit != null && BattleSetupManager.Instance.SelectedUnit.faction == Faction.Hero && StartingLocation == true)
                {
                    //Move hero to here...
                    var SelUnit = BattleSetupManager.Instance.SelectedUnit;
                    SelUnit.transform.position = transform.position;
                    SelUnit.OccupiedTile.OccupiedUnit = null;
                    SelUnit.OccupiedTile = this;
                    OccupiedUnit = SelUnit;
                    BattleSetupManager.Instance.SelectedUnit = null;
                }
            }
        }

        else if(GameManager.Instance.state == GameManager.GameState.PlayerTurn)
        {

        }
        else if(GameManager.Instance.state == GameManager.GameState.EnemyTurn)
        {

        }
    }

    public void SetUnit(BaseUnit unit)
    {
        if(unit.OccupiedTile != null)
        {
            unit.OccupiedTile.OccupiedUnit = null;
        }
        unit.transform.position = transform.position;
        OccupiedUnit = unit;
        unit.OccupiedTile = this;
    }
}
