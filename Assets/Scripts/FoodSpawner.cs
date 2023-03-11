using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FoodSpawner : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    BoxCollider2D bc2d;
    [SerializeField] GridManager gridManager;
    [HideInInspector] public FoodType foodType;
    TextMeshPro spriteText;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        bc2d = GetComponent<BoxCollider2D>();
        spriteText = GetComponentInChildren<TextMeshPro>();
        foodType = FoodType.NORMAL;
        spriteText.text = Constants.SpriteText[(int)foodType];
        StartCoroutine(SpawnFood());
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
        if (other.gameObject.GetComponent<SnakePlayerController>() != null) {
            SnakePlayerController snakePlayer = other.gameObject.GetComponent<SnakePlayerController>();
            StopAllCoroutines();
            if (foodType != FoodType.MASS_BURNER) {
                snakePlayer.AddSnakeSegments(foodType);
            } else {
                snakePlayer.RemoveSnakeSegments();
            }   
            StartCoroutine(SpawnFood());
        }
    }

    IEnumerator SpawnFood() {
        while (!gridManager.isGameOver) {
            spriteRenderer.color = Constants.COLOR_FOOD_DISABLED;
            bc2d.enabled = false;
            spriteText.enabled = false;
            yield return new WaitForSeconds(Constants.FOOD_DISABLED_INTERVAL);
            SetRandomFoodAndTextType();
            spriteRenderer.color = Constants.COLOR_FOOD_ENABLED;
            bc2d.enabled = true;
            spriteText.enabled = true;
            RespawnApple();
            yield return new WaitForSeconds(Constants.FOOD_SPAWN_INTERVAL);
        }
    }

    void SetRandomFoodAndTextType() {
        float random = UnityEngine.Random.Range(1, 1000);
        if (0 <= random && random < 100) {
            foodType = FoodType.MASS_GAINER;
        } else if (100 <= random && random < 200) {
            foodType = FoodType.MASS_BURNER;
        } else if (200 <= random && random < 300) {
            foodType = FoodType.MULTIPLIER;
        } else if (300 <= random && random < 400) {
            foodType = FoodType.SHIELD;
        } else if (400 <= random && random < 500 && !GameplayManager.Instance.isTwoPlayer) {
            foodType = FoodType.SPEED;
        } else {
            foodType = FoodType.NORMAL;
        }
        spriteText.text = Constants.SpriteText[(int)foodType];
    }
}
