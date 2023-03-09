using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField] GameObject ShieldField;
    [SerializeField] GameObject MultiplierField;
    [SerializeField] GameObject SpeedField;
    [SerializeField] TextMeshProUGUI ScoreField;
    [SerializeField] GameObject PauseButton;

    public int currentScore = 0;
    
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
        currentScore += 1;
    }
}
