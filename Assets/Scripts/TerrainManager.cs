using UnityEngine;

public class TerrainManager : MonoBehaviour
{
    public TerrainTile[,] map;

    public void InitializeTerrain(int width, int height)
    {
        map = new TerrainTile[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                map[x, y] = new TerrainTile();
                map[x, y].Initialize(x, y, Random.Range(0, 3));
            }
        }
    }

    public float GetMovementCost(int x, int y)
    {
        return map[x, y].movementCost;
    }

    public float GetDefensiveBonus(int x, int y)
    {
        return map[x, y].defensiveBonus;
    }
}

public class TerrainTile
{
    public int x;
    public int y;
    public int type;
    public float movementCost;
    public float defensiveBonus;

    public void Initialize(int x, int y, int type)
    {
        this.x = x;
        this.y = y;
        this.type = type;
        switch (type)
        {
            case 0:
                movementCost = 1.0f;
                defensiveBonus = 0.0f;
                break;
            case 1:
                movementCost = 1.5f;
                defensiveBonus = 0.2f;
                break;
            case 2:
                movementCost = 2.0f;
                defensiveBonus = 0.3f;
                break;
        }
    }
}