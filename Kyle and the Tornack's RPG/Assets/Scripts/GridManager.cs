using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int width, height;

    [SerializeField] private Tile tilePrefab;

    [SerializeField] private Transform cam;


    private Dictionary<Vector2, Tile> Tiles;

    private void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        Tiles = new Dictionary<Vector2, Tile>();
        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                var spawnedTile = Instantiate(tilePrefab, new Vector3(gameObject.transform.position.x + x, gameObject.transform.position.y + y), Quaternion.identity);
                spawnedTile.name = $"Tile {x} {y}";
                spawnedTile.transform.parent = gameObject.transform;

                Tiles[new Vector2(x, y)] = spawnedTile;
            }
        }
    }

    public Tile GetTileAtPosition(Vector2 pos)
    {
        if(Tiles.TryGetValue(pos, out var tile))
        {
            return tile;
        }
        return null;
    }
}
