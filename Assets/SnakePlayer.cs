using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakePlayer : MonoBehaviour
{
    Vector2 direction = Vector2.right;
    public GameObject snakeSegmentPrefab;
    [SerializeField] Transform SnakeSegments;
    List<Transform> snakeSegmentPositions;
    
    public GridManager gridManager;
    
    private void Start() {
        snakeSegmentPositions = new List<Transform>();
        snakeSegmentPositions.Add(this.transform);
        gridManager.isSnakeSegmentOnTile.Add(Constants.ConvertToVector3Int(this.transform.position), true);
    }

    private void UpdateSnakeDirection() {
        if (Input.GetKeyDown(KeyCode.UpArrow) && direction != Vector2.down) {
            direction = Vector2.up;
        } else if (Input.GetKeyDown(KeyCode.DownArrow) && direction != Vector2.up) {
            direction = Vector2.down;
        } else if (Input.GetKeyDown(KeyCode.LeftArrow) && direction != Vector2.right) {
            direction = Vector2.left;
        } else if (Input.GetKeyDown(KeyCode.RightArrow) && direction != Vector2.left) {
            direction = Vector2.right;
        }
    }

    private void UpdateSnakeMovementAndColor() {
        int currentSegments = snakeSegmentPositions.Count;
        float alphaDiff = (currentSegments != 1) ? 0.5f /(currentSegments - 1) : 0f;

        // Last Tile SetGridValue to false
        gridManager.SetGridValue(snakeSegmentPositions[currentSegments - 1].position, false);

        for (int index = currentSegments - 1; index > 0; index--) {
            snakeSegmentPositions[index].position = snakeSegmentPositions[index - 1].position;
            SpriteRenderer segmentSr = snakeSegmentPositions[index].gameObject.GetComponent<SpriteRenderer>();
            segmentSr.color = new Color(0, 1, 0, 0.5f + (currentSegments - index) * alphaDiff);
        }
    
        Vector3 position = transform.position;
        position.x = Mathf.Round(position.x + direction.x);
        position.y = Mathf.Round(position.y + direction.y);
        transform.position = new Vector3(position.x, position.y, 0f);

        // Set Head to true
        gridManager.SetGridValue(transform.position, true);
    }

    private void FixedUpdate() {
        UpdateSnakeMovementAndColor();
    } 

    public void AddSnakeSegments(int additionalSegments) {
        for (int i = 0; i < additionalSegments; i++)
            AddSnakeSegment();
    }

    private void AddSnakeSegment() {
        Transform segmentTail = Instantiate(this.snakeSegmentPrefab, SnakeSegments).transform;
        segmentTail.position = snakeSegmentPositions[snakeSegmentPositions.Count - 1].position;

        snakeSegmentPositions.Add(segmentTail);
    }

    private void Update() {
        UpdateSnakeDirection();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag(Constants.WALL)) {
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
    }
}
