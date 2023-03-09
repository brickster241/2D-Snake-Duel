using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class AppleSpawner : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    BoxCollider2D bc2d;
    [SerializeField] GridManager gridManager;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        bc2d = GetComponent<BoxCollider2D>();
        StartCoroutine(FoodSpawner());
    }

    void RespawnApple() {
        float position_x, position_y;
        do
        {
            position_x = Mathf.Round(Random.Range(-Constants.X_BOUND, Constants.X_BOUND));
            position_y = Mathf.Round(Random.Range(Constants.Y_BOUND_BOTTOM, Constants.Y_BOUND_TOP));
        } while (gridManager.isSnakeSegmentOnTile.GetValueOrDefault(new Vector3Int((int)position_x, (int)position_y), false));
        transform.position = new Vector3(position_x, position_y, 0f);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.GetComponent<SnakePlayer>() != null) {
            SnakePlayer snakePlayer = other.gameObject.GetComponent<SnakePlayer>();
            StopAllCoroutines();
            snakePlayer.AddSnakeSegments(1);
            StartCoroutine(FoodSpawner());
        }
    }

    IEnumerator FoodSpawner() {
        while (!gridManager.isGameOver) {
            // Update Sprite Color
            Color srColor = spriteRenderer.color;
            spriteRenderer.color = new Color(srColor.r, srColor.g, srColor.b, 0f);
            bc2d.enabled = false;
            yield return new WaitForSeconds(Constants.FOOD_DISABLED_INTERVAL);
            spriteRenderer.color = new Color(srColor.r, srColor.g, srColor.b, 1f);
            bc2d.enabled = true;
            RespawnApple();
            yield return new WaitForSeconds(Constants.FOOD_SPAWN_INTERVAL);
        }
    }
}
