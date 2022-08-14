using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;
    [SerializeField] private int width, height;

    [SerializeField] private Tile tilePrefab;
    private Tile[] tileGrid;


    public Dictionary<Vector2, Tile> Tiles;

    private void Awake()
    {
        Instance = this;
    }


    public void FindGrid()
    {
        tileGrid = GetComponentsInChildren<Tile>();
        Tiles = new Dictionary<Vector2, Tile>();

        foreach (Tile tile in tileGrid)
        {
            Tiles[new Vector2(tile.transform.position.x, tile.transform.position.y)] = tile;
        }

        GameManager.Instance.ChangeState(GameManager.GameState.SelectUnits);
    }

    public Tile GetTileAtPosition(Vector2 pos)
    {
        if(Tiles.TryGetValue(pos, out var tile))
        {
            return tile;
        }
        return null;
    }

    public Tile GetHeroSpawnTile()
    {
        return Tiles.Where(t => t.Value.StartingLocation && t.Value.Walkable).OrderBy(tag => Random.value).First().Value;
    }

    public void GridView()
    {
        foreach (Tile tile in tileGrid)
        {
            tile.GetComponent<SpriteRenderer>().enabled = false;
        }
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
