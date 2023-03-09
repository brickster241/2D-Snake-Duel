using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants
{
    public const string WALL = "Wall";
    public const string LEFT_WALL = "Left";
    public const string RIGHT_WALL = "Right";
    public const string TOP_WALL = "Top";
    public const string BOTTOM_WALL = "Bottom";
    public const int X_BOUND = 24;
    public const int Y_BOUND_TOP = 10;
    public const int Y_BOUND_BOTTOM = -12;

    public static Vector3Int ConvertToVector3Int(Vector3 position) {
        return new Vector3Int((int)Mathf.Round(position.x), (int)Mathf.Round(position.y), 0);
    }
}
