using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    public GameObject ShieldField;
    public GameObject MultiplierField;
    public GameObject SpeedField;
    public TextMeshProUGUI ScoreField;
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
}
