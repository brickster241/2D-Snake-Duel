using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LobbyController : MonoBehaviour
{
    [SerializeField] GameObject MainMenuUI;
    [SerializeField] GameObject GameTypeUI;
    [SerializeField] GameObject HowToPlayUI;
    [SerializeField] TextMeshProUGUI highScoreText;

    public void StartGame() {
        GameplayManager.Instance.PlayAudio(AudioType.BUTTON_CLICK);
        MainMenuUI.SetActive(false);
        HowToPlayUI.SetActive(false);
        GameTypeUI.SetActive(true);
    }

    public void BackToStart() {
        GameplayManager.Instance.PlayAudio(AudioType.BUTTON_CLICK);
        GameTypeUI.SetActive(false);
        HowToPlayUI.SetActive(false);
        MainMenuUI.SetActive(true);
    }

    public void ViewInstructions() {
        GameplayManager.Instance.PlayAudio(AudioType.BUTTON_CLICK);
        GameTypeUI.SetActive(false);
        MainMenuUI.SetActive(false);
        HowToPlayUI.SetActive(true);
    }

    public void QuitApplication() {
        GameplayManager.Instance.PlayAudio(AudioType.BUTTON_CLICK);
        Application.Quit();
    }

    public void StartSinglePlayer() {
        GameplayManager.Instance.PlayAudio(AudioType.BUTTON_CLICK);
        GameplayManager.Instance.StopAudio(AudioType.MAIN_MENU);
        GameplayManager.Instance.PlayAudio(AudioType.LEVEL);
        SceneManager.LoadScene(Constants.SINGLE_PLAYER_BUILD_INDEX);
    }

    public void StartTwoPlayer() {
        GameplayManager.Instance.PlayAudio(AudioType.BUTTON_CLICK);
        GameplayManager.Instance.StopAudio(AudioType.MAIN_MENU);
        GameplayManager.Instance.PlayAudio(AudioType.LEVEL);
        SceneManager.LoadScene(Constants.TWO_PLAYER_BUILD_INDEX);
    }

    private void Update() {
        highScoreText.text = "HIGH SCORE : " + PlayerPrefs.GetInt(Constants.HIGH_SCORE, 0);
    }
}
