using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakePlayer : MonoBehaviour
{
    Vector2 direction = Vector2.right;
    public GameObject snakeSegmentPrefab;
    [SerializeField] Transform SnakeSegments;
    List<Transform> snakeSegmentPositions;

    private void Start() {
        snakeSegmentPositions = new List<Transform>();
        snakeSegmentPositions.Add(this.transform);
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

    private void UpdateSnakeMovement() {

        for (int index = snakeSegmentPositions.Count - 1; index > 0; index--)
            snakeSegmentPositions[index].position = snakeSegmentPositions[index - 1].position;

        Vector3 position = transform.position;
        position.x = Mathf.Round(position.x + direction.x);
        position.y = Mathf.Round(position.y + direction.y);
        transform.position = new Vector3(position.x, position.y, 0f);
    }

    private void FixedUpdate() {
        UpdateSnakeMovement();
    } 

    public void AddSnakeSegment() {
        Transform segmentTail = Instantiate(this.snakeSegmentPrefab, SnakeSegments).transform;
        segmentTail.position = snakeSegmentPositions[snakeSegmentPositions.Count - 1].position;

        snakeSegmentPositions.Add(segmentTail);
    }

    private void Update() {
        UpdateSnakeDirection();
    }
}
