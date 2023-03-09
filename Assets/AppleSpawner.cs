using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleSpawner : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        float position_x = Mathf.Round(Random.Range(-Constants.X_BOUND, Constants.X_BOUND));
        float position_y = Mathf.Round(Random.Range(Constants.Y_BOUND_BOTTOM, Constants.Y_BOUND_TOP));
        transform.position = new Vector3(position_x, position_y, 0f);
    }

    void RespawnApple(Dictionary<Vector3Int, bool> dict) {
        float position_x, position_y;
        do
        {
            position_x = Mathf.Round(Random.Range(-Constants.X_BOUND, Constants.X_BOUND));
            position_y = Mathf.Round(Random.Range(Constants.Y_BOUND_BOTTOM, Constants.Y_BOUND_TOP));
        } while (dict.GetValueOrDefault(new Vector3Int((int)position_x, (int)position_y), false));
        transform.position = new Vector3(position_x, position_y, 0f);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.GetComponent<SnakePlayer>() != null) {
            SnakePlayer snakePlayer = other.gameObject.GetComponent<SnakePlayer>();
            snakePlayer.AddSnakeSegments(1);
            RespawnApple(snakePlayer.gridManager.isSnakeSegmentOnTile);
        }
    }
}
