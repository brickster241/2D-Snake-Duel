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

    // Update is called once per frame
    void Update()
    {
        UpdateScore(PlayerType.PLAYER_1);
        if (AudioManager.isTwoPlayer)
            UpdateScore(PlayerType.PLAYER_2);
        
        if (Input.GetKeyDown(KeyCode.Space))
            OnPauseButtonClick();
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
        AudioManager.Instance.Play(AudioType.BUTTON_CLICK);
        TextMeshProUGUI pauseText = PauseButton.GetComponentInChildren<TextMeshProUGUI>();
        Image img = PauseButton.GetComponent<Image>();

        pauseText.text = "PAUSED";
        img.color = Color.yellow;
        Time.timeScale = 0f;

        PauseMenuUI.SetActive(true);
    }

    public void ResumePlay() {
        AudioManager.Instance.Play(AudioType.BUTTON_CLICK);
        TextMeshProUGUI pauseText = PauseButton.GetComponentInChildren<TextMeshProUGUI>();
        Image img = PauseButton.GetComponent<Image>();
        img.color = Color.cyan;
        pauseText.text = "PAUSE";
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
    }

    public void RestartLevel() {
        AudioManager.Instance.Play(AudioType.BUTTON_CLICK);
        int currentSceneBuildIndex = SceneManager.GetActiveScene().buildIndex;
        Time.timeScale = 1f;
        SceneManager.LoadScene(currentSceneBuildIndex);
    }

    public void LoadMainMenu() {
        AudioManager.Instance.Play(AudioType.BUTTON_CLICK);
        AudioManager.Instance.Stop(AudioType.LEVEL);
        AudioManager.Instance.Play(AudioType.MAIN_MENU);
        Time.timeScale = 1f;
        SceneManager.LoadScene(Constants.MAIN_MENU_BUILD_INDEX);
    }
}
