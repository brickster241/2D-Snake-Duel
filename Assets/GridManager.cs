using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpriteInfo {
    public FoodType foodType;
    public Sprite sprite;
}

public class GridManager : MonoBehaviour
{
    public bool isGameOver = false;
    public Dictionary<Vector3Int, bool> isSnakeSegmentOnTile;
    public SpriteInfo[] spriteManager;

    
    public void SetGridValue(Vector3 position, bool value) {
        Vector3Int v3pos = Constants.ConvertToVector3Int(position);
        if (isSnakeSegmentOnTile.ContainsKey(v3pos)) {
            isSnakeSegmentOnTile[v3pos] = value;
        } else {
            isSnakeSegmentOnTile.Add(v3pos, value);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        isSnakeSegmentOnTile = new Dictionary<Vector3Int, bool>();
    }
}