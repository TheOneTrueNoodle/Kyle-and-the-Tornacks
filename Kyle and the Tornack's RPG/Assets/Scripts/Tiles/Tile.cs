using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private SpriteRenderer Renderer;
    [SerializeField] private Color highlightColor;

    public bool obstacle;

    public bool StartingLocation;
    [SerializeField] private Color StartingLocationColor;

    public BaseUnit OccupiedUnit;
    public bool Walkable => obstacle == false && OccupiedUnit == null;

    private void Start()
    {
        Renderer.enabled = false;
    }

    private void OnMouseEnter()
    {
        Renderer.enabled = true;
        Renderer.color = highlightColor;
    }

    private void OnMouseExit()
    {
        Renderer.enabled = false;
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
