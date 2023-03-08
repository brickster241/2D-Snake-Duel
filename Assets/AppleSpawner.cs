using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleSpawner : MonoBehaviour
{
    int X_BOUND = 23;
    int Y_BOUND = 11;
    void Start()
    {
        RespawnApple();
    }

    void RespawnApple() {
        float position_x = Mathf.Round(Random.Range(-X_BOUND, X_BOUND));
        float position_y = Mathf.Round(Random.Range(-Y_BOUND, Y_BOUND));
        transform.position = new Vector3(position_x, position_y, 0f);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.GetComponent<SnakePlayer>() != null) {
            SnakePlayer snakePlayer = other.gameObject.GetComponent<SnakePlayer>();
            snakePlayer.AddSnakeSegment();
            RespawnApple();
        }
    }
}
