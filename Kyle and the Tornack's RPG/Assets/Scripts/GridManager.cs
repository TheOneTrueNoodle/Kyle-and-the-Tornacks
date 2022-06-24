using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int width, height;

    [SerializeField] private Tile tilePrefab;
    private Tile[] tileGrid;

    public Dictionary<Vector2, Tile> Tiles;

    private void Start()
    {
        FindGrid();
        //GenerateGrid();
    }

    void FindGrid()
    {
        tileGrid = GetComponentsInChildren<Tile>();
        Tiles = new Dictionary<Vector2, Tile>();

        foreach (Tile tile in tileGrid)
        {
            Tiles[new Vector2(tile.transform.position.x, tile.transform.position.y)] = tile;
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

    void GenerateGrid()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var spawnedTile = Instantiate(tilePrefab, new Vector3(gameObject.transform.position.x + x, gameObject.transform.position.y + y), Quaternion.identity);
                spawnedTile.name = $"Tile {x} {y}";
                spawnedTile.transform.parent = gameObject.transform;
            }
        }
    }
}
