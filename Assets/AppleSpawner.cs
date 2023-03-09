using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleSpawner : MonoBehaviour
{
    void Start()
    {
        float position_x = Mathf.Round(Random.Range(-Constants.X_BOUND, Constants.X_BOUND));
        float position_y = Mathf.Round(Random.Range(-Constants.Y_BOUND, Constants.Y_BOUND));
        transform.position = new Vector3(position_x, position_y, 0f);
    }

    void RespawnApple(Dictionary<Vector3Int, bool> dict) {
        float position_x, position_y;
        do
        {
            position_x = Mathf.Round(Random.Range(-Constants.X_BOUND, Constants.X_BOUND));
            position_y = Mathf.Round(Random.Range(-Constants.Y_BOUND, Constants.Y_BOUND));
        } while (dict.GetValueOrDefault(new Vector3Int((int)position_x, (int)position_y), false));
        
        transform.position = new Vector3(position_x, position_y, 0f);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.GetComponent<SnakePlayer>() != null) {
            SnakePlayer snakePlayer = other.gameObject.GetComponent<SnakePlayer>();
            snakePlayer.AddSnakeSegment();
            RespawnApple(snakePlayer.isSnakeSegmentOnTile);
        }
    }
}
