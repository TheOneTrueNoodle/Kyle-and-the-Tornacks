using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private SpriteRenderer Renderer;
    [SerializeField] private Color highlightColor;

    [SerializeField] private bool obstacle;

    [SerializeField] private bool StartingLocation;
    [SerializeField] private Color StartingLocationColor;

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
}
