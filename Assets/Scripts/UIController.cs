using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIController : MonoBehaviour
{
    public GameObject Player1ShieldField;
    public GameObject Player1MultiplierField;
    public GameObject Player1SpeedField;
    [SerializeField] TextMeshProUGUI Player1ScoreField;
    public GameObject Player2ShieldField;
    public GameObject Player2MultiplierField;
    public GameObject Player2SpeedField;
    [SerializeField] TextMeshProUGUI Player2ScoreField;
    [SerializeField] GameObject PauseMenuUI;
    [SerializeField] GameObject GameOverUI;
    [SerializeField] TextMeshProUGUI GameOverMainTextHeading;
    [SerializeField] TextMeshProUGUI GameOverUIScoreField;
    [SerializeField] GridManager gridManager;
    public GameObject PauseButton;
    public int Player1CurrentScore = 0;
    public int Player2CurrentScore = 0;
    public int Player1Increment = 1;
    public int Player2Increment = 1;
    
    void Start()
    {
        Player1CurrentScore = 0;
        Player2CurrentScore = 0;
    }

    void Update()
    {
        UpdateScore(PlayerType.PLAYER_1);
        if (GameplayManager.Instance.isTwoPlayer)
            UpdateScore(PlayerType.PLAYER_2);
        if (Input.GetKeyDown(KeyCode.Space))
            OnPauseButtonClick();
    }

    public void DisplayGameOverUI(bool isHeadOnCollision, PlayerType playerType) {
        GameOverUI.SetActive(true);
        if (!GameplayManager.Instance.isTwoPlayer) {
            // Single Player
            GameOverUIScoreField.text = "YOUR SCORE : " + Player1CurrentScore;
            PlayerPrefs.SetInt(Constants.HIGH_SCORE, Mathf.Max(Player1CurrentScore, PlayerPrefs.GetInt(Constants.HIGH_SCORE, 0)));
        } else if (isHeadOnCollision) {
            GameOverUIScoreField.text = (Player1CurrentScore > Player2CurrentScore) ? "GREEN HAS HIGHER SCORE THAN BLUE. GREEN WINS !" : "BLUE HAS HIGHER SCORE THAN GREEN. BLUE WINS !";
            if (Player1CurrentScore == Player2CurrentScore)
                GameOverUIScoreField.text = "GREEN AND BLUE HAVE SAME SCORE. IT'S A DRAW !";
        } else {
            GameOverUIScoreField.text = (playerType == PlayerType.PLAYER_1) ? "GREEN COLLIDED. BLUE WINS !" : "BLUE COLLIDED. GREEN WINS !"; 
        }
        Time.timeScale = 0f;
    }

    private void UpdateScore(PlayerType playerType) {
        switch (playerType)
        {
            case PlayerType.PLAYER_1:
                Player1ScoreField.text = "SCORE : " + Player1CurrentScore;
                break;
            case PlayerType.PLAYER_2:
                Player2ScoreField.text = "SCORE : " + Player2CurrentScore;
                break;
        }
    }

    public void IncrementScore(PlayerType playerType) {
        switch (playerType)
        {
            case PlayerType.PLAYER_1:
                Player1CurrentScore += Player1Increment;
                break;
            case PlayerType.PLAYER_2:
                Player2CurrentScore += Player2Increment;
                break;
        }
    }

    public void OnPauseButtonClick() {
        GameplayManager.Instance.PlayAudio(AudioType.BUTTON_CLICK);
        TextMeshProUGUI pauseText = PauseButton.GetComponentInChildren<TextMeshProUGUI>();
        Image img = PauseButton.GetComponent<Image>();

        pauseText.text = "PAUSED";
        img.color = Color.yellow;
        Time.timeScale = 0f;

        PauseMenuUI.SetActive(true);
    }

    public void ResumePlay() {
        GameplayManager.Instance.PlayAudio(AudioType.BUTTON_CLICK);
        TextMeshProUGUI pauseText = PauseButton.GetComponentInChildren<TextMeshProUGUI>();
        Image img = PauseButton.GetComponent<Image>();
        img.color = Color.cyan;
        pauseText.text = "PAUSE";
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
    }

    public void RestartLevel() {
        GameplayManager.Instance.PlayAudio(AudioType.BUTTON_CLICK);
        int currentSceneBuildIndex = SceneManager.GetActiveScene().buildIndex;
        Time.timeScale = 1f;
        SceneManager.LoadScene(currentSceneBuildIndex);
    }

    public void LoadMainMenu() {
        GameplayManager.Instance.PlayAudio(AudioType.BUTTON_CLICK);
        GameplayManager.Instance.StopAudio(AudioType.LEVEL);
        GameplayManager.Instance.PlayAudio(AudioType.MAIN_MENU);
        Time.timeScale = 1f;
        SceneManager.LoadScene(Constants.MAIN_MENU_BUILD_INDEX);
    }
}
