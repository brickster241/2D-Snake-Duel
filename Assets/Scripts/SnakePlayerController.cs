using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SnakePlayerController : MonoBehaviour
{
    [SerializeField] PlayerType playerType;
    [SerializeField] Transform SnakeSegments;
    [SerializeField] UIController uIController;
    Vector2 playerDirection;
    List<Transform> snakeSegmentPositions;
    Coroutine speed = null;
    Coroutine multiplier = null;
    Coroutine shield = null;
    public GridManager gridManager;
    [SerializeField] GameObject snakeSegmentPrefab;
    private bool isShieldActive = false;
    private SpriteRenderer spriteRenderer;

    private void Awake() {
        playerDirection = (playerType == PlayerType.PLAYER_1) ? Vector2.right : Vector2.left;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.color = (playerType == PlayerType.PLAYER_1) ? Color.green : Color.cyan; 
    }

    private void Start() {
        isShieldActive = false;
        snakeSegmentPositions = new List<Transform>();
        snakeSegmentPositions.Add(this.transform);
        gridManager.isSnakeSegmentOnTile.Add(Constants.ConvertToVector3Int(this.transform.position), true);
    }

    private void UpdatePlayerMovementAndColor() {
        int currentSegments = snakeSegmentPositions.Count;
        float alphaDiff = (currentSegments != 1) ? 0.5f /(currentSegments - 1) : 0f;

        // Last Tile SetGridValue to false
        gridManager.SetGridValue(snakeSegmentPositions[currentSegments - 1].position, false);

        for (int index = currentSegments - 1; index > 0; index--) {
            snakeSegmentPositions[index].position = snakeSegmentPositions[index - 1].position;
            SpriteRenderer segmentSr = snakeSegmentPositions[index].gameObject.GetComponent<SpriteRenderer>();
            segmentSr.color = new Color(segmentSr.color.r, segmentSr.color.g, segmentSr.color.b, 0.5f + (currentSegments - index) * alphaDiff);
        }
    
        Vector3 position = transform.position;
        position.x = Mathf.Round(position.x + playerDirection.x);
        position.y = Mathf.Round(position.y + playerDirection.y);
        transform.position = new Vector3(position.x, position.y, 0f);

        // Set Head to true
        gridManager.SetGridValue(transform.position, true);
    }

     private void FixedUpdate() {
        UpdatePlayerMovementAndColor();
    } 

    // additionalSegments will change based on MASS_GAINER
    public void AddSnakeSegments(FoodType foodType) {
        int additionalSegments = (foodType == FoodType.MASS_GAINER) ? Constants.SEGMENTS_MASS_GAINER : 1;
        for (int i = 0; i < additionalSegments; i++)
            AddSnakeSegment();
    }

    // decreaseSegments will change based on MASS_BURNER
    public void RemoveSnakeSegments() {
        int startingIndex = Mathf.Max(snakeSegmentPositions.Count - Constants.SEGMENTS_MASS_BURNER, 1);
        int initialLength = snakeSegmentPositions.Count;
        for (int index = initialLength - 1; index > startingIndex; index--)
        {
            Transform currTransform = snakeSegmentPositions[index];
            gridManager.SetGridValue(currTransform.position, false);
            snakeSegmentPositions.Remove(currTransform);
            Destroy(currTransform.gameObject);
        }
    }

    private void AddSnakeSegment() {
        Transform segmentTail = Instantiate(this.snakeSegmentPrefab, SnakeSegments).transform;
        segmentTail.position = snakeSegmentPositions[snakeSegmentPositions.Count - 1].position;
        gridManager.SetGridValue(segmentTail.position, true);
        snakeSegmentPositions.Add(segmentTail);
    }

    private void Update() {
        UpdatePlayerDirection();
    }

    private void UpdatePlayerDirection() {
        switch (playerType)
        {
            case PlayerType.PLAYER_1:
                if (Input.GetKeyDown(KeyCode.UpArrow) && playerDirection != Vector2.down) {
                    playerDirection = Vector2.up;
                } else if (Input.GetKeyDown(KeyCode.DownArrow) && playerDirection != Vector2.up) {
                    playerDirection = Vector2.down;
                } else if (Input.GetKeyDown(KeyCode.LeftArrow) && playerDirection != Vector2.right) {
                    playerDirection = Vector2.left;
                } else if (Input.GetKeyDown(KeyCode.RightArrow) && playerDirection != Vector2.left) {
                    playerDirection = Vector2.right;
                }
                break;
            case PlayerType.PLAYER_2:
                if (Input.GetKeyDown(KeyCode.W) && playerDirection != Vector2.down) {
                    playerDirection = Vector2.up;
                } else if (Input.GetKeyDown(KeyCode.S) && playerDirection != Vector2.up) {
                    playerDirection = Vector2.down;
                } else if (Input.GetKeyDown(KeyCode.A) && playerDirection != Vector2.right) {
                    playerDirection = Vector2.left;
                } else if (Input.GetKeyDown(KeyCode.D) && playerDirection != Vector2.left) {
                    playerDirection = Vector2.right;
                }
                break;
        }
    }
    
    private void WallCollisionHandler(Collider2D other) {
        string name = other.gameObject.name;
        Vector3 currPos = transform.position;
        switch (name)
        {
            case Constants.LEFT_WALL:
                transform.position = new Vector3(Constants.X_BOUND, currPos.y, currPos.z);
                break;
            case Constants.RIGHT_WALL:
                transform.position = new Vector3(-Constants.X_BOUND, currPos.y, currPos.z);
                break;
            case Constants.TOP_WALL:
                transform.position = new Vector3(currPos.x, Constants.Y_BOUND_BOTTOM, currPos.z);
                break;
            case Constants.BOTTOM_WALL:
                transform.position = new Vector3(currPos.x, Constants.Y_BOUND_TOP, currPos.z);
                break;
        }
    }

    private void FoodCollisionHandler(Collider2D other) {
        FoodSpawner spawnedFood = other.gameObject.GetComponent<FoodSpawner>();
        uIController.IncrementScore(playerType);
        switch (spawnedFood.foodType)
        {
            case FoodType.SPEED:
                if (speed != null)
                    StopCoroutine(speed);
                GameplayManager.Instance.PlayAudio(AudioType.POWER_PICKUP);
                GameObject speedField = (playerType == PlayerType.PLAYER_1) ? uIController.Player1SpeedField : uIController.Player2SpeedField;
                speed = StartCoroutine(EnablePowerUp(speedField, FoodType.SPEED));
                break;
            case FoodType.MULTIPLIER:
                if (multiplier != null)
                    StopCoroutine(multiplier);
                GameplayManager.Instance.PlayAudio(AudioType.POWER_PICKUP);
                GameObject multiPlierField = (playerType == PlayerType.PLAYER_1) ? uIController.Player1MultiplierField : uIController.Player2MultiplierField;
                multiplier = StartCoroutine(EnablePowerUp(multiPlierField, FoodType.MULTIPLIER));
                break;
            case FoodType.SHIELD:
                if (shield != null) 
                    StopCoroutine(shield);
                GameplayManager.Instance.PlayAudio(AudioType.POWER_PICKUP);
                GameObject shieldField = (playerType == PlayerType.PLAYER_1) ? uIController.Player1ShieldField : uIController.Player2ShieldField;
                shield = StartCoroutine(EnablePowerUp(shieldField, FoodType.SHIELD));
                break;
            default:
                GameplayManager.Instance.PlayAudio(AudioType.FOOD_PICKUP);
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag(Constants.WALL)) {
            WallCollisionHandler(other);
        } else if (other.gameObject.CompareTag(Constants.FOOD)) {
            FoodCollisionHandler(other);
        } else if (other.gameObject.CompareTag(Constants.SNAKE_SEGMENT)) {
            SegmentCollisionHandler(other);
        }
    }

    private void SegmentCollisionHandler(Collider2D other) {
        float spriteBlue = other.gameObject.GetComponent<SpriteRenderer>().color.b;
        if (spriteRenderer.color.b != spriteBlue) {
            // Collided with Another Snake
            gridManager.isGameOver = true;
            if (other.gameObject.GetComponent<SnakePlayerController>() != null) {
                int Player1Score = uIController.Player1CurrentScore;
                int player2Score = uIController.Player2CurrentScore;
                PlayerType winPlayer = (Player1Score >= player2Score) ? PlayerType.PLAYER_1 : PlayerType.PLAYER_2;
                uIController.DisplayGameOverUI(true, winPlayer);   
            } else {
                uIController.DisplayGameOverUI(false, playerType);
            }
        } else if (!isShieldActive) {
            gridManager.isGameOver = true;
            uIController.DisplayGameOverUI(false, playerType);
        }
    }

    IEnumerator EnablePowerUp(GameObject obj, FoodType foodType) {
        Image fieldImg = obj.GetComponent<Image>();
        TextMeshProUGUI textField = obj.GetComponentInChildren<TextMeshProUGUI>();
        fieldImg.color = (playerType == PlayerType.PLAYER_1) ? Color.green : Color.cyan;
        textField.color = Color.black;
        switch (foodType)
        {
            case FoodType.SPEED:
                Time.fixedDeltaTime = Constants.TIME_FIXED_DELTA_SPEED;
                yield return new WaitForSeconds(Constants.POWER_UP_INTERVAL);
                Time.fixedDeltaTime = Constants.TIME_FIXED_DELTA_NORMAL;        
                break;
            case FoodType.MULTIPLIER:
                if (playerType == PlayerType.PLAYER_1) {
                    uIController.Player1Increment = Constants.MULTIPLIER_INCREMENT;
                    yield return new WaitForSeconds(Constants.POWER_UP_INTERVAL);
                    uIController.Player1Increment = 1;
                } else {
                    uIController.Player2Increment = Constants.MULTIPLIER_INCREMENT;
                    yield return new WaitForSeconds(Constants.POWER_UP_INTERVAL);
                    uIController.Player2Increment = 1;
                }
                break;
            case FoodType.SHIELD:
                isShieldActive = true;
                yield return new WaitForSeconds(Constants.POWER_UP_INTERVAL);
                isShieldActive = false;
                break;
            default:
                break;
        }
        fieldImg.color = Color.black;
        textField.color = Color.white;
        
    }

}
