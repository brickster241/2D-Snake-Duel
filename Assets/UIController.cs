using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIController : MonoBehaviour
{
    public GameObject ShieldField;
    public GameObject MultiplierField;
    public GameObject SpeedField;
    [SerializeField] TextMeshProUGUI ScoreField;
    [SerializeField] GameObject PauseMenuUI;
    public GameObject PauseButton;
    public int currentScore = 0;
    public int increment = 1;
    
    void Start()
    {
        currentScore = 0;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateScore();
    }

    private void UpdateScore() {
        ScoreField.text = "SCORE : " + currentScore;
    }

    public void IncrementScore() {
        currentScore += increment;
    }

    public void OnPauseButtonClick() {
        TextMeshProUGUI pauseText = PauseButton.GetComponentInChildren<TextMeshProUGUI>();
        Image img = PauseButton.GetComponent<Image>();

        pauseText.text = "PAUSED";
        img.color = Color.yellow;
        Time.timeScale = 0f;

        PauseMenuUI.SetActive(true);
    }

    public void ResumePlay() {
        TextMeshProUGUI pauseText = PauseButton.GetComponentInChildren<TextMeshProUGUI>();
        Image img = PauseButton.GetComponent<Image>();
        
        img.color = Color.cyan;
        pauseText.text = "PAUSE";
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
    }

    public void RestartLevel() {
        int currentSceneBuildIndex = SceneManager.GetActiveScene().buildIndex;
        Time.timeScale = 1f;
        SceneManager.LoadScene(currentSceneBuildIndex);
    }

    public void LoadMainMenu() {
        Time.timeScale = 1f;
        SceneManager.LoadScene(Constants.MAIN_MENU_BUILD_INDEX);
    }
}
