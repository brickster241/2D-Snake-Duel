using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LobbyController : MonoBehaviour
{
    [SerializeField] GameObject MainMenuUI;
    [SerializeField] GameObject GameTypeUI;
    [SerializeField] TextMeshProUGUI highScoreText;

    public void StartGame() {
        AudioManager.Instance.Play(AudioType.BUTTON_CLICK);
        MainMenuUI.SetActive(false);
        GameTypeUI.SetActive(true);
    }

    public void BackToStart() {
        AudioManager.Instance.Play(AudioType.BUTTON_CLICK);
        GameTypeUI.SetActive(false);
        MainMenuUI.SetActive(true);
    }

    public void QuitApplication() {
        AudioManager.Instance.Play(AudioType.BUTTON_CLICK);
        Application.Quit();
    }

    public void StartSinglePlayer() {
        AudioManager.Instance.Play(AudioType.BUTTON_CLICK);
        AudioManager.Instance.Stop(AudioType.MAIN_MENU);
        AudioManager.Instance.Play(AudioType.LEVEL);
        SceneManager.LoadScene(Constants.SINGLE_PLAYER_BUILD_INDEX);
    }

    public void StartTwoPlayer() {
        AudioManager.Instance.Play(AudioType.BUTTON_CLICK);
        AudioManager.Instance.Stop(AudioType.MAIN_MENU);
        AudioManager.Instance.Play(AudioType.LEVEL);
        SceneManager.LoadScene(Constants.TWO_PLAYER_BUILD_INDEX);
    }

    private void Update() {
        highScoreText.text = "HIGH SCORE : " + PlayerPrefs.GetInt(Constants.HIGH_SCORE, 0);
    }
}
