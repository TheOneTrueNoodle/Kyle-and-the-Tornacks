using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private SpriteRenderer Renderer;
    [SerializeField] private Color highlightColor;

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
