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
    public const string FOOD = "Food";
    public const int X_BOUND = 24;
    public const int Y_BOUND_TOP = 10;
    public const int Y_BOUND_BOTTOM = -12;
    public const float FOOD_SPAWN_INTERVAL = 10f;
    public const float FOOD_DISABLED_INTERVAL = 3f;
    public const int SEGMENTS_MASS_GAINER = 4;
    public const int SEGMENTS_MASS_BURNER = 6;
    public const float POWER_UP_INTERVAL = 10f;

    public static Vector3Int ConvertToVector3Int(Vector3 position) {
        return new Vector3Int((int)Mathf.Round(position.x), (int)Mathf.Round(position.y), 0);
    }
}
