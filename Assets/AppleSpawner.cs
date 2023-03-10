using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleSpawner : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    BoxCollider2D bc2d;
    [SerializeField] GridManager gridManager;
    public FoodType foodType;
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
        SetTransformScale(foodType);
        transform.position = new Vector3(position_x, position_y, 0f);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.GetComponent<SnakePlayer>() != null) {
            SnakePlayer snakePlayer = other.gameObject.GetComponent<SnakePlayer>();
            StopAllCoroutines();
            if (foodType != FoodType.MASS_BURNER) {
                snakePlayer.AddSnakeSegments(foodType);
            } else {
                snakePlayer.RemoveSnakeSegments();
            }   
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
        } else if (100 <= random && random < 200) {
            foodType = FoodType.MASS_BURNER;
        } else if (200 <= random && random < 300) {
            foodType = FoodType.SPEED;
        } else if (300 <= random && random < 400) {
            foodType = FoodType.MULTIPLIER;
        } else if (400 <= random && random < 500) {
            foodType = FoodType.SHIELD;
        } else {
            foodType = FoodType.SHIELD;
        }
        
        Sprite sprite = Array.Find(sprites, item => item.foodType == foodType).sprite;
        return sprite;
    }


    private void SetTransformScale(FoodType foodType) {
        switch (foodType)
        {
            case FoodType.SPEED:
                transform.localScale = new Vector3(0.5f, 0.5f, 1f);
                break;
            case FoodType.SHIELD:
                transform.localScale = new Vector3(0.25f, 0.25f, 1f);
                break;
            default:
                transform.localScale = new Vector3(1f, 1f, 1f);
                break;
        }
    }
}
