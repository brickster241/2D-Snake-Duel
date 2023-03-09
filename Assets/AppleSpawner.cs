using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FoodType {
    MASS_GAINER,
    NORMAL,
    MASS_BURNER,
    SHIELD,
    MULTIPLIER,
    SPEED
}


public class AppleSpawner : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    BoxCollider2D bc2d;
    [SerializeField] GridManager gridManager;
    FoodType foodType;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        bc2d = GetComponent<BoxCollider2D>();
        foodType = FoodType.NORMAL;
        StartCoroutine(FoodSpawner());
    }

    void RespawnApple() {
        float position_x, position_y;
        do
        {
            position_x = Mathf.Round(UnityEngine.Random.Range(-Constants.X_BOUND, Constants.X_BOUND));
            position_y = Mathf.Round(UnityEngine.Random.Range(Constants.Y_BOUND_BOTTOM, Constants.Y_BOUND_TOP));
        } while (gridManager.isSnakeSegmentOnTile.GetValueOrDefault(new Vector3Int((int)position_x, (int)position_y), false));
        transform.position = new Vector3(position_x, position_y, 0f);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.GetComponent<SnakePlayer>() != null) {
            SnakePlayer snakePlayer = other.gameObject.GetComponent<SnakePlayer>();
            StopAllCoroutines();
            snakePlayer.AddSnakeSegments(foodType);
            StartCoroutine(FoodSpawner());
        }
    }

    IEnumerator FoodSpawner() {
        while (!gridManager.isGameOver) {
            // Update Sprite Color
            Color srColor = spriteRenderer.color;
            spriteRenderer.color = new Color(srColor.r, srColor.g, srColor.b, 0f);
            bc2d.enabled = false;
            foodType = FoodType.NORMAL;
            yield return new WaitForSeconds(Constants.FOOD_DISABLED_INTERVAL);

            // Add New Sprite on Probability
            spriteRenderer.sprite = GetRandomizedSprite();
            spriteRenderer.color = new Color(srColor.r, srColor.g, srColor.b, 1f);
            bc2d.enabled = true;
            RespawnApple();
            yield return new WaitForSeconds(Constants.FOOD_SPAWN_INTERVAL);
        }
    }

    Sprite GetRandomizedSprite() {
        SpriteInfo[] sprites = gridManager.spriteManager;
        float random = UnityEngine.Random.Range(1, 1000);
        if (0 <= random && random < 100) {
            foodType = FoodType.MASS_GAINER;
        } else {
            foodType = FoodType.NORMAL;
        }
        
        Sprite sprite = Array.Find(sprites, item => item.foodType == foodType).sprite;
        return sprite;
    }
}
