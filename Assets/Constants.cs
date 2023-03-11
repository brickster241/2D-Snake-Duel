using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SceneType {
    MAIN_MENU,
    ONE_PLAYER,
    TWO_PLAYER
}

public enum FoodType {
    MASS_GAINER,
    NORMAL,
    MASS_BURNER,
    SHIELD,
    MULTIPLIER,
    SPEED
}

public class Constants
{
    public const string WALL = "Wall";
    public const string LEFT_WALL = "Left";
    public const string RIGHT_WALL = "Right";
    public const string TOP_WALL = "Top";
    public const string BOTTOM_WALL = "Bottom";
    public const string FOOD = "Food";
    public const string SNAKE_SEGMENT = "SnakeSegment";
    public const string HIGH_SCORE = "HIGHSCORE";
    public const int X_BOUND = 24;
    public const int Y_BOUND_TOP = 10;
    public const int Y_BOUND_BOTTOM = -12;
    public const float FOOD_SPAWN_INTERVAL = 10f;
    public const float FOOD_DISABLED_INTERVAL = 3f;
    public const int SEGMENTS_MASS_GAINER = 4;
    public const int SEGMENTS_MASS_BURNER = 6;
    public const float POWER_UP_INTERVAL = 10f;
    public const int MULTIPLIER_INCREMENT = 5;
    public static readonly string[] SpriteText = {"M+", "", "M-", "S", "X", "++"};
    public static Color COLOR_FOOD_ENABLED = new Color(1, 0, 0, 1);
    public static Color COLOR_FOOD_DISABLED = new Color(1, 0, 0, 0);
    public const float TIME_FIXED_DELTA_SPEED = 0.05f;
    public const float TIME_FIXED_DELTA_NORMAL = 0.08f;
    public const int MAIN_MENU_BUILD_INDEX = 0;
    public const int SINGLE_PLAYER_BUILD_INDEX = 1;
    public const int TWO_PLAYER_BUILD_INDEX = 2;
    public const float SCENE_LOADING_WAIT_TIME = 0.038f;
    public const float SLOWED_DELAY_TIME_SCALE = 0.01f;

    public static Vector3Int ConvertToVector3Int(Vector3 position) {
        return new Vector3Int((int)Mathf.Round(position.x), (int)Mathf.Round(position.y), 0);
    }
}
