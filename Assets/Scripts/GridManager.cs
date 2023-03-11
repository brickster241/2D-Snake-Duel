using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GridManager : MonoBehaviour
{
    public bool isGameOver = false;
    public Dictionary<Vector3Int, bool> isSnakeSegmentOnTile;
    
    public void SetGridValue(Vector3 position, bool value) {
        Vector3Int v3pos = Constants.ConvertToVector3Int(position);
        if (isSnakeSegmentOnTile.ContainsKey(v3pos)) {
            isSnakeSegmentOnTile[v3pos] = value;
        } else {
            isSnakeSegmentOnTile.Add(v3pos, value);
        }
    }

    void Start()
    {
        isGameOver = false;
        GameplayManager.Instance.isTwoPlayer = (SceneManager.GetActiveScene().buildIndex == (int)SceneType.TWO_PLAYER);
        isSnakeSegmentOnTile = new Dictionary<Vector3Int, bool>();
    }
}
